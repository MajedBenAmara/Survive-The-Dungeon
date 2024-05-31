using UnityEngine;

public class OgreBossDashState : OgreBossBaseState
{
    public override void EnterStat(OgreBossStateManager bossStateManager)
    {
        //Debug.Log("BossDashState");

        bossStateManager.PlayAnimation("Ogre_Dash");
    }

    public override void ExitStat(OgreBossStateManager bossStateManager)
    {
        bossStateManager.CanDash = false;
        bossStateManager.DashTimer = Time.time;
    }

    public override void LogicUpdate(OgreBossStateManager bossStateManager)
    {
        if (bossStateManager.GetAnimationCondition("Ogre_Dash", .6f))
        {
            //Debug.Log("Dash Force");
            bossStateManager.ApplyDash();
        }
    }

    public override void PhysicUpdate(OgreBossStateManager bossStateManager)
    {
    }
}
