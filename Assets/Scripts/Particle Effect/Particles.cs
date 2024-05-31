using UnityEngine;

public class Particles : MonoBehaviour
{

    #region Variables

    private ParticleSystem _system;

    #endregion

    #region Unity Func
    private void Start()
    {
        _system = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if (_system.isStopped)
            Destroy(gameObject);
    }

    #endregion

}
