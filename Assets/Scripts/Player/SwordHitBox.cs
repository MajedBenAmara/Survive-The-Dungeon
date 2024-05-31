using UnityEngine;

public class SwordHitBox : MonoBehaviour
{
    #region Built Func

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            DealDamaget(collision);
            collision.gameObject.GetComponent<Enemy>().
                StartCoroutine(collision.gameObject.GetComponent<Enemy>().GetElectricuted());
        }
        if (collision.CompareTag("Box"))
        {
            collision.gameObject.GetComponent<Box>().HandleWeaponHit();
        }
    }

    private void DealDamaget(Collider2D collision)
    {

        collision.gameObject.SendMessage("HandleTakingDamageFromPlayer",
        GameManager.Instance.PlayerStats.PlayerDamage);
        
    }


    #endregion

}
