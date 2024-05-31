public abstract class  OgreBossBaseState 
{
    public abstract void EnterStat(OgreBossStateManager bossStateManager);
    public abstract void LogicUpdate(OgreBossStateManager bossStateManager);
    public abstract void PhysicUpdate(OgreBossStateManager bossStateManager);
    public abstract void ExitStat(OgreBossStateManager bossStateManager);
}
