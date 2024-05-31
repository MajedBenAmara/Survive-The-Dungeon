using System.Collections;
using UnityEngine;

public class AbsorptionDemon : Enemy
{
    #region Variables

    [Header("Range Indicator")]
    [SerializeField]
    protected float _scalingDuration;
    [SerializeField]
    protected float _scalingSpeed;
    [SerializeField]
    protected Transform _innerRange;
    [SerializeField]
    protected Transform _outerRange;

    [Header("Absorption Components")]

    [SerializeField]
    private float _absorptionDistance = 2f;

    [SerializeField]
    private float _absorptionAmount;

    [SerializeField]
    private float _timeBetweenAbsorption = .5f;

    private float _maxHealth;
    private float _absorptionMoment;

    #endregion

    #region Unity Func

    protected override void Start()
    {
        base.Start();
        _maxHealth = Health;
        _outerRange.localScale = _innerRange.localScale = new Vector2(2 * _absorptionDistance, 2 * _absorptionDistance); 
    }

    private void Update()
    {
        AbsorpeHealth();
        FacePlayer();
        Burn();
    }

    #endregion

    #region Built Func

    private void AbsorpeHealth()
    {
        if(CalculateDistanceFromPlayerToEnemy() < _absorptionDistance)
        {
            if (Time.time - _absorptionMoment > _timeBetweenAbsorption)
            {
                _absorptionMoment = Time.time;
                if(Health <= _maxHealth)
                {
                    Health += _absorptionAmount;
                }
                DealDamage(_absorptionAmount);
                GameManager.Instance.PlayerFlashEffect.DrainFlash();
                GameManager.Instance.PlayerVFXControler.PlayDrainEffect();

            }
            _innerRange.gameObject.SetActive(true);
            _outerRange.gameObject.SetActive(true);
            StartCoroutine(PlayAbsorption());

        }
        else
        {
            GameManager.Instance.PlayerVFXControler.StopDrainEffect();
            _innerRange.gameObject.SetActive(false);
            _outerRange.gameObject.SetActive(false);
            ChasePlayer();
        }
    }

    private IEnumerator PlayAbsorption()
    {
        while(gameObject != null)
        {
            yield return IndicateRange();
        }
    }

    public IEnumerator IndicateRange()
    {
        float t = 0f;
        float rate = (1 / _scalingDuration) * _scalingSpeed;

        while (t < 1f)
        {
            t += Time.deltaTime * rate;
            _innerRange.localScale = Vector2.Lerp(new Vector2(2 * _absorptionDistance, 2 * _absorptionDistance), Vector2.zero, t);
            yield return null;
        }
    }

    protected override void HealthCheck()
    {
        if(Health <= 0)
        {
            GameManager.Instance.PlayerVFXControler.StopDrainEffect();
        }
        base.HealthCheck();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _absorptionDistance);
    }

    #endregion

}
