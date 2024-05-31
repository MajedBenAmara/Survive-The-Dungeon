using UnityEngine;

public class SlowArea : MonoBehaviour
{
    #region Variables

    internal float Range;

    [SerializeField]
    private float _slowAmount = .15f;

    [SerializeField]
    private float _slowAreaDamage = 2f;


    private bool _doItOnce = false;
    private CircleCollider2D _circleCollider2D;

    #endregion

    #region Unity Func

    private void Start()
    {
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _circleCollider2D.radius = Range;
    }

    // slow player if he hit the collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (!_doItOnce)
            {
                if (GameManager.Instance.PlayerStats.PlayerMovementSpeed > .05f)
                {
                    collision.gameObject.SendMessage("HandleTackingDamage", _slowAreaDamage);
                    float slowedSpeed = GameManager.Instance.PlayerStats.PlayerMovementSpeed - _slowAmount;
                    GameManager.Instance.PlayerStats.PlayerMovementSpeed = slowedSpeed;
                    GameManager.Instance.PlayerVFXControler.PlaySlowEffect();
                    GameManager.Instance.PlayerFlashEffect.SlowFlash();
                    _doItOnce = true;
                }

            }

        }

    }

    #endregion
}
