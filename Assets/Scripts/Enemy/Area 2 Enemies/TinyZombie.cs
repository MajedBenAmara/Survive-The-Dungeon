using System.Collections;
using UnityEngine;

public class TinyZombie : Enemy
{
    #region Variables

    [SerializeField]
    private bool _drawGizmo = false;

    [Header("Crowd Control")]
    [SerializeField]
    private float _explosionTriggerDisntace = .5f;

    [SerializeField]
    private float _explosionRange = .5f;

    [SerializeField]
    private float _slowDuration = .5f;

    [SerializeField]
    private CircleCollider2D _slowAreaCollider;

    [SerializeField]
    private SlowArea slowArea;

    [SerializeField]
    private RangIndicator _rangIndicator;

    private bool _exploded = false;
    private bool _isExploding = false;
    private bool _canExplode = false;
    private float _playerOriginalSpeed;
    private float _effectTime;
    private int _flashNumber = 4;
    private bool _noHealth = false;
    private bool _doItOne = true;

    #endregion

    #region Unity Region

    protected override void Start()
    {
        base.Start();
        _playerOriginalSpeed = GameManager.Instance.PlayerStats.PlayerMovementSpeed;
        _rangIndicator.MaxRange = _explosionRange;
    }
    private void OnEnable()
    {
        slowArea.Range = _explosionRange;
    }

    private void Update()
    {
        ManageFunctions();
    }

    #endregion

    #region Built Func

    private void ManageFunctions()
    {
        //  if the enemy in the phase of exploding we dont call this func:
        if (!_isExploding)
        {
            Burn();
            ChasePlayer();
            FacePlayer();
        }

        // if the enemy exploded we don't call this func, we do this we only call it one time
        if (!_exploded)
        {
            CheckExplosionDistanceTrigger();
        }

        // this func is called when enemy health reach 0
        if (_noHealth)
        {
            ExplosionEffect();
        }
    }

    protected override void HealthCheck()
    {
        // if the enemy is exploding we don't need to check his health
        if (!_isExploding)
        {
            if(Health <= 0)
            {
                _noHealth = true;

            }
        }
        else
        {

            if (_doItOne)
            {
                GameManager.Instance.IncreaseNumberOfKills();
                _doItOne = false;
            }

            Destroy(gameObject);
        }
    }

    // check if the player in the range to trigger the explosion
    private void CheckExplosionDistanceTrigger()
    {
        // if the enemy is in the range to trigger the explosion the enemy will explode
        if (CalculateDistanceFromPlayerToEnemy() <= _explosionTriggerDisntace)
        {
            // this indicate that the enemy can explode even if the player run out of the explosion trigger distance
            _canExplode = true;
        }
        if (_canExplode)
        {
            ExplosionEffect();
        }
    }

    // Create the explosion effect
    private void ExplosionEffect()
    {
        // start indicate the range of the explosion
        _rangIndicator.ActivateOuterRange();
        _rangIndicator.StartCoroutine(_rangIndicator.IndicateRange());
        _isExploding = true;
        // Do the Falsh effect 4 Times then explode
        if(_flashNumber > 0)
        {
            if (Time.time - _effectTime > 2 * _flashEffect.FlashDuration)
            {
                _effectTime = Time.time;
                _flashEffect.NormalFlash();
                _flashNumber--;
            }
        }
        else
        {
            // slow Player
            StartCoroutine(SlowPlayer());
            
            GetComponent<Renderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            Instantiate(_deathEffect, transform.position, transform.rotation);

        }


    }

    private IEnumerator SlowPlayer()
    {

        // activate and desactivate the slow area collider to slow the player
        _slowAreaCollider.enabled = true;
        yield return new WaitForSeconds(.3f);

        _slowAreaCollider.enabled = false;
        yield return new WaitForSeconds(_slowDuration);

        // Give the player his original speed after a duration
        GameManager.Instance.PlayerVFXControler.StopSlowEffect();
        GameManager.Instance.PlayerStats.PlayerMovementSpeed = _playerOriginalSpeed;

        _exploded = true;
        HealthCheck();
        
    }

    private void OnDrawGizmos()
    {
        if (_drawGizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _explosionRange);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _explosionTriggerDisntace);

        }

    }

    #endregion
}



