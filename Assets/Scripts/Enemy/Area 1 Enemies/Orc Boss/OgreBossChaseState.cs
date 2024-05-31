public class OgreBossChaseState : OgreBossBaseState
{
    public override void EnterStat(OgreBossStateManager bossStateManager)
    {
        //Debug.Log("BossChaseState");

        bossStateManager.PlayAnimation("Ogre_Run");
    }

    public override void ExitStat(OgreBossStateManager bossStateManager)
    {
    }

    public override void LogicUpdate(OgreBossStateManager bossStateManager)
    {
    }

    public override void PhysicUpdate(OgreBossStateManager bossStateManager)
    {
        bossStateManager.ChasePlayer();
    }
}
