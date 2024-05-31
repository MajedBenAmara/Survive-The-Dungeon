using UnityEngine;

public class Projectil : MonoBehaviour
{

    #region Variables

    [Header("Projectil Stats")]
    [SerializeField]
    protected float _projectilSpeed = 2f;
    [SerializeField]
    protected float _LifeDuration = 2f;

    #endregion

    #region Unity Func

    protected virtual void Start()
    {
        // destroy the projectil after it's life duration expair
        Invoke("DestroyBullet", _LifeDuration);
    }
    protected void Update()
    {
        transform.Translate(Vector2.right * _projectilSpeed * Time.deltaTime);
    }

    #endregion

    #region Built Fun

    protected virtual void DestroyBullet()
    {
        Destroy(gameObject);
    }

    #endregion

}

