using UnityEngine;

public class ShamanFirePoint : MonoBehaviour
{

    #region Unity Func

    private void Update()
    {
        GetDirectionToPlayer();
    }

    #endregion

    #region Built Func 

    public void GetDirectionToPlayer()
    {
        Vector3 shootDirection;
        float angle;
        Quaternion rotaion;
         shootDirection = (GameManager.Instance.PlayerTransform.position - transform.position).normalized;
        angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        rotaion = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = rotaion;
    }

    #endregion

}
