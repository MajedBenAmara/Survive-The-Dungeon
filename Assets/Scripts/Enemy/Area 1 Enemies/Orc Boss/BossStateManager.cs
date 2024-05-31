using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateManager : Enemy
{
    #region Variables

    [Header("Gizmo")]
    public bool DrawGizmo = true;

    [Header("Cooldowns")]
    [SerializeField]
    protected float RestCooldown = 1f;
    [SerializeField]
    protected float MeleeAttackCooldown = 1f;


    [Header("Attack")]
    public float TimeBetweenMeleeAttacks;
    public float WeaponDamage;

    [Header("Distances")]
    [SerializeField]
    protected float _chaseDistance = 2f;
    [SerializeField]
    protected float _meleeAttackDistance = .5f;

    [Header("Boss Death")]
    [SerializeField]
    protected KeyNotification _keyNotification;
    [SerializeField]
    protected GameObject _potion;

    #region Timers
    internal float RestTimer;
    internal float MeleeAttackTimer;
    #endregion

    #region States Enablers
    internal bool CanRest = true;
    internal bool CanMeleeAttack = true;
    #endregion

    #endregion

    #region Built Func

    // manage when do we switch to specific state
    protected virtual void ManageState()
    {


    }

    // get true or false if the givin animation has finished playing the given percentage of the total play time
    public bool GetAnimationCondition(string animationName, float percentage)
    {
        return _anim.GetCurrentAnimatorStateInfo(0).IsName(animationName) &&
            _anim.GetCurrentAnimatorStateInfo(0).normalizedTime > percentage;
    }

    // play the given animation
    public void PlayAnimation(string animationName)
    {
        _anim.Play(animationName);
    }
    ////////////////////////////////////////////
    //Indicate the end of the various cooldowns.//

    public void CalculateRestTime()
    {
        if (Time.time > RestCooldown + RestTimer)
        {
            CanRest = true;
        }
    }

    public void CalculateMeleeAttackTime()
    {
        if (Time.time > MeleeAttackCooldown + MeleeAttackTimer)
        {
            CanMeleeAttack = true;
        }
    }


    // Check if the health dropped to zero
    protected override void HealthCheck()
    {
        if (Health <= 0)
        {
            GameManager.Instance.PlayerStats.NumberOfKeys++;
            Instantiate(_deathEffect, transform.position, transform.rotation);
            _keyNotification.AppearKeyNotification();
            Instantiate(_potion, transform.position, Quaternion.identity);
            GameManager.Instance.StartCoroutine(GameManager.Instance.ShowEquipmentSelectionUI());
            Destroy(gameObject);
        }
    }

    // Gizmo
    protected virtual void OnDrawGizmos()
    {

        if (DrawGizmo)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _chaseDistance);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _meleeAttackDistance);
        }

    }
    #endregion
}
