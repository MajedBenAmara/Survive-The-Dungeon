using UnityEngine;

public class ShamanStaff : MonoBehaviour
{

    #region Variables
    [SerializeField]
    private Transform _projectilOrigin;
    [SerializeField]
    private Projectil _projectil;
    #endregion

    #region Built Function

    public void CreateProjectil()
    {
        Instantiate(_projectil.gameObject, _projectilOrigin.position, _projectilOrigin.rotation);
    }
    #endregion

}
