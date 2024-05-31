using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : Enemy
{
    #region Variables
    protected float fireMoment = 0f;

    [Header("Animation names")]
    [SerializeField]
    protected string _orcIdleAnim;
    [SerializeField]
    protected string _orcRunAnim;
    [SerializeField]
    protected string _weaponIdleAnim;
    [SerializeField]
    protected string _weaponAttackAnim;

    [Header("Distance check")]
    [SerializeField]
    protected float _chaseDistance = 2f;
    [SerializeField]
    protected float _attackDistance = 7f;

    [Header("Attack Stats")]
    [SerializeField]
    protected float _timeBetweenRangedAttacks = 1f;

    [Header("Class")]
    [SerializeField]
    protected string _orcClass;
    #endregion

    #region Built Function
    protected virtual void AttackPlayer()
    {
    }

    protected void CheckDistanceToPlayer()
    {
        // if the player is far from enemy(> _chaseDistance), the enemy will chase him
        if (CalculateDistanceFromPlayerToEnemy() > _chaseDistance)
        {
            _anim.Play(_orcRunAnim);
            ChasePlayer();
        }
        // if player is close enough to be attack (< _attackDistance), the enemy attack him
        if (CalculateDistanceFromPlayerToEnemy() < _attackDistance)
        {
            AttackPlayer();
        }
    }
    #endregion
}