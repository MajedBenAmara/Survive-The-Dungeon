using UnityEngine;

public class Zombie : Enemy
{
    [Header("Root Area")]

    [SerializeField]
    private GameObject _rootArea;

    [SerializeField]
    private float _timeBetweenSpawn;

    private float _spawnMoment;


    [Header("Distance")]

    [SerializeField]
    private float _chaseDistance;

    [SerializeField]
    private float _attackDistance;

    private void Update()
    {
        Burn();
        CheckDistanceToPlayer();
        FacePlayer();
    }

    private void SpawnRootArea()
    {
        if(Time.time - _spawnMoment > _timeBetweenSpawn)
        {
            _spawnMoment = Time.time;
            Instantiate(_rootArea, GameManager.Instance.PlayerTransform.position, Quaternion.identity);
        }
        
    }

    protected void CheckDistanceToPlayer()
    {
        // if the player is far from enemy(> _chaseDistance), the enemy will chase him
        if (CalculateDistanceFromPlayerToEnemy() > _chaseDistance)
        {
            ChasePlayer();
        }
        // if player is close enough to be attack (< _attackDistance), the enemy attack him
        if (CalculateDistanceFromPlayerToEnemy() < _attackDistance)
        {
            SpawnRootArea();
        }
    }
}
