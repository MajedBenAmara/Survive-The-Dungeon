using UnityEngine;

public class PoisonDemon : Enemy
{

    #region Variables
    [Header("Poison")]
    [SerializeField]
    private PoisonCloud _poisonCloud;
    [SerializeField]
    private float _detonationDistance = 1f;

    #endregion

    #region Unity Func

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        Burn();
        ChasePlayer();
        FacePlayer();
        CreatePoisonCloud();
    }

    #endregion

    #region Built Func

    // Create a poison cloud when the enemy reach a certain distance
    private void CreatePoisonCloud()
    {
        if (CalculateDistanceFromPlayerToEnemy() < _detonationDistance)
        {
            OnEnemeyDestruction();
        }
    }

    protected override void HealthCheck()
    {
        if (Health <= 0)
        {
            OnEnemeyDestruction();
        }
    }

    private void OnEnemeyDestruction()
    {
        if (_doItOnce)
        {
            GameManager.Instance.IncreaseNumberOfKills();
            Instantiate(_deathEffect, transform.position, transform.rotation);
            _doItOnce = false;
        }
        Instantiate(_poisonCloud, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detonationDistance);
    }

    #endregion

}
