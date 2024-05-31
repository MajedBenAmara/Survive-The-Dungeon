using UnityEngine;

public class PoisonCloud : MonoBehaviour
{
    [SerializeField]
    private float _poisonDamage = 1f;
    [SerializeField]
    private int _poisonCounter = 4;
    [SerializeField]
    private float _timeBetweenPoison = .5f;
    [SerializeField]
    private float _cloudDuration = 1f;

    private float _cloudAppearingMoment;

    private void Start()
    {
        _cloudAppearingMoment = Time.time;   
    }

    private void Update()
    {
        if(Time.time - _cloudAppearingMoment > _cloudDuration)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.PlayerStats.StartCoroutine(
                GameManager.Instance.PlayerStats.
                DealPoisonDamage(_poisonDamage, _timeBetweenPoison, _poisonCounter));
        }
    }


}
