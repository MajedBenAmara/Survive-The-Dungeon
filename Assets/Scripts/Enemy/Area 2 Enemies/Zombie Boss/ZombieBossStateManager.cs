using UnityEngine;

public class ZombieBossStateManager : BossStateManager
{

    #region Variables

    #region States

    private ZombieBossBaseState _currentState;
    private ZombieBossRangeAttackState _zombieBossRangeAttackState = new ZombieBossRangeAttackState();
    private ZombieBossMeleeAttackState _zombieBossMeleeAttackState = new ZombieBossMeleeAttackState();
    private ZombieBossChaseState _zombieBossChaseState = new ZombieBossChaseState();
    private ZombieBossRestState _zombieBossRestState = new ZombieBossRestState();

    #endregion

    #region Ranged Attacks

    [Header("Ranged Attacks Components")]
    public float TimeBetweenRangedAttacks;
    public float RangeAttackCooldown;

    internal float RangeAttackTimer;
    internal bool CanRangeAttack = false;
    internal float FireMoment;

    [SerializeField]
    private float _rangeAttackDistance;

    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private Projectil _projectil;

    #endregion

    #region Melee Attacks

    [Header("Fist Components")]

    [SerializeField]
    private float _fistDamage;

    [SerializeField]
    private float _fearDuration;

    private float _fearMoment;

    #endregion

    #endregion

    #region Unity Func

    protected override void Start()
    {
        base.Start();
        _currentState = _zombieBossRestState;
        ZombieFist.FearThePlayer += FearPlayer;
    }

    void Update()
    {
        FacePlayer();

        ManageState();

        CalculateRestTime();
        CalculateMeleeAttackTime();
        CalculateRangeAttackTime();
        GetDirectionToPlayer();

        Burn();

        _currentState.LogicUpdate(this);
    }

    private void FixedUpdate()
    {
        _currentState.PhysicUpdate(this);
    }

    #endregion

    #region Built Func

    // switch to the giving state
    private void SwitchState(ZombieBossBaseState currentState)
    {
        _currentState.ExitStat(this);
        _currentState = currentState;
        _currentState.EnterStat(this);
    }

    protected override void ManageState()
    {
        // Ranged Attack state 

        if (CalculateDistanceFromPlayerToEnemy() <= _rangeAttackDistance
            && CalculateDistanceFromPlayerToEnemy() >  _chaseDistance
               && CanRangeAttack)
        {
            SwitchState(_zombieBossRangeAttackState);
        }


        // Melee Attack state 
        if ((CalculateDistanceFromPlayerToEnemy() <= _meleeAttackDistance) && CanMeleeAttack)
        {
            SwitchState(_zombieBossMeleeAttackState);
        }

        // Rest state
        if (CanRest)
        {
            if (!(CalculateDistanceFromPlayerToEnemy() <= _chaseDistance
            && CalculateDistanceFromPlayerToEnemy() > _meleeAttackDistance) || 
                        (!CanMeleeAttack || !CanRangeAttack))
            {
                SwitchState(_zombieBossRestState);
            }
        }

        // Chase state
        if (CalculateDistanceFromPlayerToEnemy() <= _chaseDistance
            && CalculateDistanceFromPlayerToEnemy() > _meleeAttackDistance)
        {
            SwitchState(_zombieBossChaseState);
        }
    }

    // Indicate when can the range attack could be cast again
    private void CalculateRangeAttackTime()
    {
        if (Time.time > RangeAttackCooldown + RangeAttackTimer)
        {
            CanRangeAttack = true;
        }
    }

    public void GetDirectionToPlayer()
    {
        Vector3 shootDirection;
        float angle;
        Quaternion rotaion;
        shootDirection = (GameManager.Instance.PlayerTransform.position - _firePoint.position).normalized;
        angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        rotaion = Quaternion.AngleAxis(angle, Vector3.forward);

        _firePoint.rotation = rotaion;
    }

    public void CreateProjectil()
    {
        Instantiate(_projectil.gameObject, _firePoint.position, _firePoint.rotation);
    }

    // fear the player by inversing the directional input
    private void FearPlayer()
    {
        if(Time.time - _fearMoment > GameManager.Instance.PlayerStats.TimeBetweenTakingDamage)
        {
            _fearMoment = Time.time;
            GameManager.Instance.FearPlayer(_fearDuration);
            DealDamage(_fistDamage);
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if (DrawGizmo)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _rangeAttackDistance);
        }


    }

    #endregion

}
