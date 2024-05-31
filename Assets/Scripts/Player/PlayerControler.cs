using System.Collections;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    #region Variables

    internal Vector2 Direction;

    [SerializeField]
    private Transform _rotationIndicator;
    [SerializeField]
    private Weapon _weapon;

    internal Rigidbody2D Rb;

    #endregion

    #region Unity Func

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (!GameManager.Instance.PlayerCannotTurn)
        {
            if (_weapon.GetIdleAnimationCondition(.001f))
                FlipPlayerOnMousePosition();
        }

    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.PlayerCannotTurn)
        {
            if (GameManager.Instance.PlayerCanMove)
            {
                MovePlayer();
            }
            
        }
    }

    #endregion

    #region Built Func

    private void MovePlayer()
    { 
        Rb.velocity =  Direction.normalized * GameManager.Instance.PlayerStats.PlayerMovementSpeed;
    }

    public void FlipPlayerOnMousePosition()
    {
        Vector3 mousePosition;
        Vector3 shootDirection;
        float angle;

        // getting the mouse coor in world space coordiante system
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // calculating the direction from the weapon holder to the mouse
        shootDirection = (mousePosition - transform.position).normalized;
        // calculating the produced angle by the shooting direction vector annd transforming into into degree
        angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        // Creating a rotation using the angle that we previously made
        if ((angle <= 90) && (angle >= -90))
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);

        }
        else
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }

    public void ApplyForce(Vector2 forceDirection, float dashPower)
    {
        StartCoroutine(SmallDash(forceDirection, dashPower));
    }

    IEnumerator SmallDash(Vector2 direction, float dashPower)
    {
        Rb.velocity = direction * dashPower;
        yield return new WaitForSeconds(.2f);
        Rb.velocity = Vector2.zero;
    }

    #endregion

}
