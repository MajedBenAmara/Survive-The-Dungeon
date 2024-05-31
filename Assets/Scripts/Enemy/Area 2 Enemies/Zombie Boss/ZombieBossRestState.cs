using UnityEngine;

public class ZombieBossRestState : ZombieBossBaseState
{
    public override void EnterStat(ZombieBossStateManager bossStateManager)
    {
        //Debug.Log("Rest State ");

        bossStateManager.PlayAnimation("Zombie_Boss_Idle");
    }

    public override void ExitStat(ZombieBossStateManager bossStateManager)
    {
        bossStateManager.CanRest = false;
        bossStateManager.RestTimer = Time.time;
    }

    public override void LogicUpdate(ZombieBossStateManager bossStateManager)
    {
    }

    public override void PhysicUpdate(ZombieBossStateManager bossStateManager)
    {
    }
}
