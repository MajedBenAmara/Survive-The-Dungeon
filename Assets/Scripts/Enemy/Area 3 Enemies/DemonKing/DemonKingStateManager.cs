using System.Collections;
using TMPro;
using UnityEngine;

public class DemonKingStateManager : BossStateManager
{

    #region Variables

    #region States

    private DemonKingBaseState _currentState;
    private DemonKingAttackState _demonKingMeleeAttackState = new DemonKingAttackState();
    private DemonKingChaseManager _demonKingChaseState = new DemonKingChaseManager();
    private DemonKingPoisonState _demonKingPoisonState = new DemonKingPoisonState();
    private DemonKingRestState _demonKingRestState = new DemonKingRestState();

    #endregion

    #region Poison Cloud

    [Header("Poison Cloud Components")]

    public float TimeBetweenPoisonSpawn = 1f;


    internal float PoisonSpawnTimer;
    internal bool CanSpawnPoison = false;
    internal float PoisonSpawnMoment;


    [SerializeField]
    private float _poisonSpawnCooldown;

    [SerializeField]
    private float _poisonSpawnDistance;

    [SerializeField]
    private DemonKingPoisonCloud _poisonCloud;

    #endregion

    #region Health Absorption

    [Header("Health Absorption Components")]

    [SerializeField]
    protected float _scalingDuration;

    [SerializeField]
    protected float _scalingSpeed;

    [SerializeField]
    protected Transform _innerRange;

    [SerializeField]
    protected Transform _outerRange;

    [SerializeField]
    private float _absorptionDistance = 2f;

    [SerializeField]
    private float _absorptionAmount;

    [SerializeField]
    private float _timeBetweenAbsorption = .5f;

    private float _maxHealth;

    private float _absorptionMoment;

    private bool _playAbsorpe = false;

    #endregion

    #region Stake's Attack

    [Header("Stacke's Attack Components")]

    public TextMeshProUGUI StakesNumberText;


    [SerializeField]
    private float _meleeAttackDamage;

    private int _stakes = 0;

    #endregion

    #endregion

    #region Unity Func

    protected override void Start()
    {
        base.Start();
        _currentState = _demonKingRestState;
        _outerRange.localScale = new Vector2(_absorptionDistance, _absorptionDistance);
        _maxHealth = Health;
    }

    private void Update()
    {
        FacePlayer();

        ManageState();

        CalculateRestTime();
        CalculateMeleeAttackTime();
        CalculateSpawningPoisonTime();

        AbsorpeHealthCheckUp();

        Burn();

        _currentState.LogicUpdate(this);
    }

    private void FixedUpdate()
    {
        _currentState.PhysicUpdate(this);
    }

    #endregion

    #region Built Func

    // Switch for one state to another
    private void SwitchState(DemonKingBaseState newState)
    {
        _currentState.ExitStat(this);
        _currentState = newState;
        _currentState.EnterStat(this);
    }

    // Manage the change between the different states
    protected override void ManageState()
    {
        // Ranged Attack state 

        if (CalculateDistanceFromPlayerToEnemy() <= _poisonSpawnDistance
            && CalculateDistanceFromPlayerToEnemy() > _chaseDistance
               && CanSpawnPoison)
        {
            SwitchState(_demonKingPoisonState);
        }


        // Melee Attack state 
        if ((CalculateDistanceFromPlayerToEnemy() <= _meleeAttackDistance) && CanMeleeAttack)
        {
            SwitchState(_demonKingMeleeAttackState);
        }
        else
        {
            ResetStakesCounter();
        }
        // Rest state
        if (CanRest)
        {
            if (!(CalculateDistanceFromPlayerToEnemy() <= _chaseDistance) ||
                        (!CanMeleeAttack || !CanSpawnPoison))
            {
                SwitchState(_demonKingRestState);
            }
        }

        // Chase state
        if (CalculateDistanceFromPlayerToEnemy() <= _chaseDistance
            && CalculateDistanceFromPlayerToEnemy() > _meleeAttackDistance)
        {
            SwitchState(_demonKingChaseState);
        }
    }

    // Indicate when the poison clouds can be spawned again
    public void CalculateSpawningPoisonTime()
    {
        if (Time.time > _poisonSpawnCooldown + PoisonSpawnTimer)
        {
            CanSpawnPoison = true;
        }
    }

    
    public void SpawnPoisonCloud()
    {
        Instantiate(_poisonCloud, GameManager.Instance.PlayerTransform.position, Quaternion.identity);
    }

    // check when the boss can absorpe the player health
    private void AbsorpeHealthCheckUp()
    {
        if ((CalculateDistanceFromPlayerToEnemy() < _absorptionDistance)
                && (Health < _maxHealth * .3f))
        {

            AbsorpeHealth();
            ShowAbsorptionAnimation();

        }
        else
        {
            HideAbsorptionAnimation();
        }


    }

    // Absorpe Health from player
    private void AbsorpeHealth()
    {

        if (Time.time - _absorptionMoment > _timeBetweenAbsorption)
        {
            _absorptionMoment = Time.time;
            if (Health <= _maxHealth && Health > 0f)
            {
                Health += _absorptionAmount;
            }

            DealDamage(_absorptionAmount);
            GameManager.Instance.PlayerVFXControler.PlayDrainEffect();
            GameManager.Instance.PlayerFlashEffect.DrainFlash();
        }

    }

    // Show the indicator for the rang of health absorption 
    private void ShowAbsorptionAnimation()
    {

        _innerRange.gameObject.SetActive(true);
        _outerRange.gameObject.SetActive(true);
        _playAbsorpe = true;
        StartCoroutine(PlayAbsorption());
        
    }

    // Hide the indicator for the rang of health absorption 
    private void HideAbsorptionAnimation()
    {
        StopCoroutine(PlayAbsorption());
        _innerRange.gameObject.SetActive(false);
        _outerRange.gameObject.SetActive(false);
        GameManager.Instance.PlayerVFXControler.StopDrainEffect();
        _playAbsorpe = false;

    }


    private IEnumerator PlayAbsorption()
    {
        while (_playAbsorpe)
        {
            yield return CreateAbsorptionAnimation();
        }
    }


    public IEnumerator CreateAbsorptionAnimation()
    {
        float t = 0f;
        float rate = (1 / _scalingDuration) * _scalingSpeed;

        while (t < 1f)
        {
            t += Time.deltaTime * rate;
            _innerRange.localScale = Vector2.Lerp(new Vector2(_absorptionDistance ,_absorptionDistance ), Vector2.zero, t);
            yield return null;
        }
    }

    // Every time the boss hits the player we increase the number of stakes
    public void IncreaseHiteStackes()
    {
        _stakes++;
        StakesNumberText.text = _stakes.ToString();
        GameManager.Instance.PlayerStats.SendMessage("HandleTackingDamage", _meleeAttackDamage);

        // when the nb of stakes hit the max amount, the dmg will be increased 
        if (_stakes >= 3)
        {
            _stakes = 0;
            StakesNumberText.text = _stakes.ToString();
            GameManager.Instance.PlayerStats.SendMessage("HandleTackingDamage", 2 * _meleeAttackDamage);
        }
    }

    public void ResetStakesCounter()
    {
        _stakes = 0;
        StakesNumberText.text = _stakes.ToString();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if (DrawGizmo)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _poisonSpawnDistance);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _absorptionDistance);
        }

    }

    #endregion

}
