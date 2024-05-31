using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreBossAttackState : OgreBossBaseState
{
    public override void EnterStat(OgreBossStateManager bossStateManager)
    {
        //Debug.Log("BossAttackState");

        bossStateManager.PlayAnimation("Ogre_Attack");
    }

    public override void ExitStat(OgreBossStateManager bossStateManager)
    {
        bossStateManager.MeleeAttackTimer = Time.time;
    }

    public override void LogicUpdate(OgreBossStateManager bossStateManager)
    {
        if(bossStateManager.GetAnimationCondition("Ogre_Attack", .9f))
        {
            bossStateManager.CanMeleeAttack = false;
        }
    }

    public override void PhysicUpdate(OgreBossStateManager bossStateManager)
    {
    }
}
