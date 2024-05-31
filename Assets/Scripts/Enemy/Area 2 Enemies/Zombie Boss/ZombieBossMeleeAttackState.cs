using UnityEngine;

public class ZombieBossMeleeAttackState : ZombieBossBaseState
{
    public override void EnterStat(ZombieBossStateManager bossStateManager)
    {
        //Debug.Log("Melee Attack State ");

        bossStateManager.PlayAnimation("Zombie_Boss_Throw_Punches");

    }

    public override void ExitStat(ZombieBossStateManager bossStateManager)
    {
        bossStateManager.MeleeAttackTimer = Time.time;

    }

    public override void LogicUpdate(ZombieBossStateManager bossStateManager)
    {
        if (bossStateManager.GetAnimationCondition("Zombie_Boss_Throw_Punches", .9f))
        {
            bossStateManager.CanMeleeAttack = false;
        }
    }

    public override void PhysicUpdate(ZombieBossStateManager bossStateManager)
    {
    }
}
