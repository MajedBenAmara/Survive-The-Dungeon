using UnityEngine;

public class RootArea : RangIndicator
{

    #region Variables

    [Header("Root omponents")]
    [SerializeField]
    private float _rootAreaDamage = 2f;

    [SerializeField]
    private float _rootDuration = 1;

    [SerializeField]
    private float _detectionDuration = .5f;

    [SerializeField]
    private CircleCollider2D _collider;

    [SerializeField]
    private GameObject _parentGameObject;


    private float _time;
    private bool _doItOnce = true;
    private bool _catchPlayer = false;

    #endregion

    #region Unity Func

    private void Start()
    {
        ActivateOuterRange();
    }

    private void OnEnable()
    {
        StartCoroutine(IndicateRange());
    }

    private void Update()
    {
        ActivateRoot();
    }

    #endregion

    #region Built Func

    // Activate the collider
    private void ActivateRoot()
    {
        // when the inner lange hit it's max local scale
        if(_innerRange.localScale.x == 2 * MaxRange)
        {
            // we read the time of collider activation only once
            if (_doItOnce)
            {
                _collider.enabled = true;
                _time = Time.time;
                _doItOnce = false;
            }

            // Desactivaet the collider after the duration is finished
            if (Time.time - _time > _detectionDuration)
            {
                _collider.enabled = false;

                // if the player didn't get rooted we destroy this game object
                if (!_catchPlayer)
                {
                    Destroy(_parentGameObject);

                }
            }

        }
    }

    // Root player if he hit the collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameManager.Instance.PlayerCanMove)
            {
                collision.gameObject.SendMessage("HandleTackingDamage", _rootAreaDamage);
                _catchPlayer = true;
                StartCoroutine(GameManager.Instance.RootPlayer(_parentGameObject, _rootDuration));
            }

        }
    }



    #endregion

}
