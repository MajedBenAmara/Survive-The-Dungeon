using UnityEngine;

public class DemonKingAttackState : DemonKingBaseState
{
    public override void EnterStat(DemonKingStateManager bossStateManager)
    {
        bossStateManager.PlayAnimation("Demon_King_Attack");

    }

    public override void ExitStat(DemonKingStateManager bossStateManager)
    {

        bossStateManager.MeleeAttackTimer = Time.time;

    }

    public override void LogicUpdate(DemonKingStateManager bossStateManager)
    {
        if (bossStateManager.GetAnimationCondition("Demon_King_Attack", .9f))
        {
            bossStateManager.CanMeleeAttack = false;
            bossStateManager.ResetStakesCounter();

        }
    }

    public override void PhysicUpdate(DemonKingStateManager bossStateManager)
    {
    }
}
