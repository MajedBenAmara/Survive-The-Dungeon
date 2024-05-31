public class DemonKingChaseManager : DemonKingBaseState
{
    public override void EnterStat(DemonKingStateManager bossStateManager)
    {
        bossStateManager.PlayAnimation("Demon_King_Run");

    }

    public override void ExitStat(DemonKingStateManager bossStateManager)
    {
    }

    public override void LogicUpdate(DemonKingStateManager bossStateManager)
    {
    }

    public override void PhysicUpdate(DemonKingStateManager bossStateManager)
    {
        bossStateManager.ChasePlayer();

    }
}
