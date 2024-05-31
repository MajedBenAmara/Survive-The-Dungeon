using UnityEngine;

public class PlayerProjectil : Projectil
{

    #region Variables

    [Header("Effect")]
    [SerializeField]
    private ParticleSystem _effect;
    internal float ProjectileDamage;
    #endregion

    #region Unity Func

    protected override void Start()
    {
        base.Start();
        ProjectileDamage = GameManager.Instance.PlayerStats.PlayerDamage;
    }

    #endregion

    #region Built Func

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.SendMessage("HandleTakingDamageFromPlayer", ProjectileDamage);
            DestroyBullet();
        }
        if (collision.CompareTag("Wall"))
        {
            DestroyBullet();
        }
    }

    protected override void DestroyBullet()
    {
        Instantiate(_effect, transform.position, Quaternion.identity);
        base.DestroyBullet();
    }

    #endregion

}
