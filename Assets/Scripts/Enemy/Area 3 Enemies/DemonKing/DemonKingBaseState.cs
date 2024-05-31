public abstract class DemonKingBaseState 
{
    public abstract void EnterStat(DemonKingStateManager bossStateManager);
    public abstract void LogicUpdate(DemonKingStateManager bossStateManager);
    public abstract void PhysicUpdate(DemonKingStateManager bossStateManager);
    public abstract void ExitStat(DemonKingStateManager bossStateManager);
}
