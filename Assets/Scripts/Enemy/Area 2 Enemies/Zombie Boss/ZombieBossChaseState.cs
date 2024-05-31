public class ZombieBossChaseState : ZombieBossBaseState
{
    public override void EnterStat(ZombieBossStateManager bossStateManager)
    {
        //Debug.Log("Chase State");

        bossStateManager.PlayAnimation("Zombie_Boss_Run");
    }

    public override void ExitStat(ZombieBossStateManager bossStateManager)
    {
    }

    public override void LogicUpdate(ZombieBossStateManager bossStateManager)
    {
    }

    public override void PhysicUpdate(ZombieBossStateManager bossStateManager)
    {
        bossStateManager.ChasePlayer();
    }
}
