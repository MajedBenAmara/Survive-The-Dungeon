using UnityEngine;

public class Spear : MonoBehaviour
{
    private Vector3 _mousePosition;
    private Vector3 _shootDirection;
    private float _angle;
    private Quaternion _rotaion;
    private Transform _playerTransform;
    private float _spearDamage;

    private void Start()
    {
        _playerTransform = GetComponentInParent<PlayerControler>().transform;
        _spearDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().PlayerDamage;

    }
    public void GetMouseDirection()
    {
        // getting the mouse coor in world space coordiante system
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // calculating the direction from the weapon holder to the mouse
        _shootDirection = (_mousePosition - transform.position).normalized;
        // calculating the produced angle by the shooting direction vector annd transforming into into degree
        _angle = Mathf.Atan2(_shootDirection.y, _shootDirection.x) * Mathf.Rad2Deg;
        // Creating a rotation using the angle that we previously made
        _rotaion = Quaternion.AngleAxis(_angle, Vector3.forward);
        // We rotate the wheapon holder using that rotation
        FixRotation();
        transform.rotation = _rotaion;
    }

    private void FixRotation()
    {
        if (_playerTransform.localScale.x < 0)
        {
            _rotaion = Quaternion.AngleAxis(_angle + 180f, Vector3.forward);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.SendMessage("HandleTakingDamageFromPlayer", _spearDamage);
        }
    }
}
