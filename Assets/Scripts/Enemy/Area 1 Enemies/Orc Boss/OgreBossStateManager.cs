using System.Collections;
using UnityEngine;

public class OgreBossStateManager : BossStateManager
{
    #region Variables

    [Header("Dash")]
    [SerializeField]
    private float DashCooldown = 1f;
    [SerializeField]
    private float DashForce = 1f;
    [SerializeField]
    private float DashDuration = 1f;
    private bool _dashDurationEnded = false;


    #region Timers
    internal float DashTimer;

    #endregion

    #region States Enablers
    internal bool CanDash = true;

    #endregion

    #region States
    private OgreBossBaseState _currentState;
    private OgreBossRestState _ogreBossRestState = new OgreBossRestState();
    private OgreBossChaseState _ogreBossChaseState = new OgreBossChaseState();
    private OgreBossAttackState _ogreBossAttackState = new OgreBossAttackState();
    private OgreBossDashState _ogreBossDashState = new OgreBossDashState();
    #endregion

    private Rigidbody2D _rb;

    #endregion

    #region Unity Func
    protected override void Start()
    {
        base.Start();
        _currentState = _ogreBossRestState;
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        FacePlayer();

        ManageState();

        CalculateRestTime();
        CalculateMeleeAttackTime();
        CalculateDashTime();

        Burn();
        _currentState.LogicUpdate(this);
    }

    private void FixedUpdate()
    {
        _currentState.PhysicUpdate(this);
    }

    #endregion

    #region Built Func

    // manage when do we switch to specific state
    protected override void ManageState()
    {
        // Dash state 

        if (CalculateDistanceFromPlayerToEnemy() <= _chaseDistance
            && CalculateDistanceFromPlayerToEnemy() > _meleeAttackDistance 
               && CanDash)
        {
            SwitchState(_ogreBossDashState);         
        }


        // Attack state 
        if (CalculateDistanceFromPlayerToEnemy() <= _meleeAttackDistance && CanMeleeAttack)
        {
            SwitchState(_ogreBossAttackState);
        }

        // Rest state
        if(CanRest)
        {
            if ( (_dashDurationEnded || 
                CalculateDistanceFromPlayerToEnemy() > _chaseDistance) && !CanMeleeAttack)
            {
                SwitchState(_ogreBossRestState);
            }
        }

        // Chase state
        if ((!CanDash && !CanMeleeAttack && !CanRest) )
        {
            SwitchState(_ogreBossChaseState);
        }


    }

    // switch to the giving state
    private void SwitchState(OgreBossBaseState currentState)
    {
        _currentState.ExitStat(this);
        _currentState = currentState;
        _currentState.EnterStat(this);
    }


                        ////////////////////////////////////////////
                        //Indicate the end of the various cooldowns.//

    public void CalculateDashTime()
    {
        if (Time.time > DashCooldown + DashTimer)
        {
            CanDash = true;
        }
    }
                        ////////////////////////////////////////////

    // Start the dash coroutine
    public void ApplyDash()
    {
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        // find the direction to the player
        Vector2 forceDirection = (GameManager.Instance.PlayerTransform.position - transform.position).normalized;
        // indicate that we're dashing
        _dashDurationEnded = false;
        // translate the transform in the direction that we found 
        transform.Translate(forceDirection * DashForce);
        // the distance of the dash
        yield return new WaitForSeconds(DashDuration);
        // stop dashing
        _rb.velocity = Vector2.zero;
        // indicate that we're no longer dahsing
        _dashDurationEnded = true;
    }

    #endregion

}

