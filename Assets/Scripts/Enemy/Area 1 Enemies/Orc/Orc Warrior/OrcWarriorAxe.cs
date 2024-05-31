using UnityEngine;

public class OrcWarriorAxe : MonoBehaviour
{

    #region Variables
    
    private int _axeDamage;

    #endregion

    #region Unity Func

    private void Start()
    {
        _axeDamage = GetComponentInParent<OrcWarrior>().AxeDamage;
    }

    #endregion

    #region Built Func

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("HandleTackingDamage", _axeDamage);
        }
    }

    #endregion
}
