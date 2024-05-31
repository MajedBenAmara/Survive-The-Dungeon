using UnityEngine;

public class DemonKingPoisonState : DemonKingBaseState
{
    public override void EnterStat(DemonKingStateManager bossStateManager)
    {
        bossStateManager.PlayAnimation("Demon_King_Spawn_Poison");

    }

    public override void ExitStat(DemonKingStateManager bossStateManager)
    {
        bossStateManager.PoisonSpawnTimer = Time.time;

    }

    public override void LogicUpdate(DemonKingStateManager bossStateManager)
    {
        if (bossStateManager.GetAnimationCondition("Demon_King_Spawn_Poison", .9f))
        {
            // instantiate Poison Cloud
            if (Time.time - bossStateManager.PoisonSpawnMoment > bossStateManager.TimeBetweenPoisonSpawn)
            {
                bossStateManager.SpawnPoisonCloud();
                bossStateManager.PoisonSpawnMoment = Time.time;
            }
            bossStateManager.CanSpawnPoison = false;
        }
    }

    public override void PhysicUpdate(DemonKingStateManager bossStateManager)
    {
    }
}
