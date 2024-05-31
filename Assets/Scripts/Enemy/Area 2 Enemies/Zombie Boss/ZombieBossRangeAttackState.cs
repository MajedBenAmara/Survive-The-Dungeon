using UnityEngine;

public class ZombieBossRangeAttackState : ZombieBossBaseState
{
    public override void EnterStat(ZombieBossStateManager bossStateManager)
    {
        //Debug.Log("Range Attack State ");

        bossStateManager.PlayAnimation("Zombie_Boss_Throw_Brain");
    }

    public override void ExitStat(ZombieBossStateManager bossStateManager)
    {
        bossStateManager.RangeAttackTimer = Time.time;
    }

    public override void LogicUpdate(ZombieBossStateManager bossStateManager)
    {
        if (bossStateManager.GetAnimationCondition("Zombie_Boss_Throw_Brain", .9f))
        {
            // instantiate projectile
            if (Time.time - bossStateManager.FireMoment > bossStateManager.TimeBetweenRangedAttacks)
            {
                bossStateManager.CreateProjectil();
                bossStateManager.FireMoment = Time.time;
            }
            bossStateManager.CanRangeAttack = false;
        }
    }

    public override void PhysicUpdate(ZombieBossStateManager bossStateManager)
    {
    }
}
