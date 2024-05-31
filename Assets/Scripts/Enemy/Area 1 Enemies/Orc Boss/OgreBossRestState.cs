using UnityEngine;

public class OgreBossRestState : OgreBossBaseState
{
    public override void EnterStat(OgreBossStateManager bossStateManager)
    {
        //Debug.Log("BossRestState");
        bossStateManager.PlayAnimation("Ogre_Idle");
    }

    public override void ExitStat(OgreBossStateManager bossStateManager)
    {
        bossStateManager.CanRest = false;
        bossStateManager.RestTimer = Time.time;
    }

    public override void LogicUpdate(OgreBossStateManager bossStateManager)
    {
    }

    public override void PhysicUpdate(OgreBossStateManager bossStateManager)
    {
    }
}
