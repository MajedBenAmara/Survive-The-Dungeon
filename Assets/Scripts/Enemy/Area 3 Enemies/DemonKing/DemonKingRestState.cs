using UnityEngine;

public class DemonKingRestState : DemonKingBaseState
{
    public override void EnterStat(DemonKingStateManager bossStateManager)
    {
        bossStateManager.PlayAnimation("Demon_King_Idle");
    }

    public override void ExitStat(DemonKingStateManager bossStateManager)
    {
        bossStateManager.CanRest = false;
        bossStateManager.RestTimer = Time.time;
    }

    public override void LogicUpdate(DemonKingStateManager bossStateManager)
    {
    }

    public override void PhysicUpdate(DemonKingStateManager bossStateManager)
    {
    }
}
