public abstract class ZombieBossBaseState 
{
    public abstract void EnterStat(ZombieBossStateManager bossStateManager);
    public abstract void LogicUpdate(ZombieBossStateManager bossStateManager);
    public abstract void PhysicUpdate(ZombieBossStateManager bossStateManager);
    public abstract void ExitStat(ZombieBossStateManager bossStateManager);
}
