using UnityEngine;

public class Pumpkin : Enemy
{
    [Header("Fear")]
    [SerializeField]
    private float _fearDuration = .5f;
    [SerializeField]
    private float _fearDamage = 4f;


    private void Update()
    {
        ChasePlayer();
        Burn();
        FacePlayer();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            GameManager.Instance.FearPlayer(_fearDuration);
            DealDamage(_fearDamage);
            Health = 0;
            HealthCheck();
            

        }

    }
}
