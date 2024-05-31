using UnityEngine;

public class DemonKingHitbox : MonoBehaviour
{
    [SerializeField]
    private DemonKingStateManager _bossStateManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _bossStateManager.IncreaseHiteStackes();
        }
    }
}
