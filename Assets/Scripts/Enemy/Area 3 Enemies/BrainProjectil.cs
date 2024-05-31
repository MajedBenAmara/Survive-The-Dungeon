using System;
using UnityEngine;

public class BrainProjectil : Projectil
{
    #region Variables

    public static event Action<float> OnContactWithPlayer;

    [SerializeField]
    private float ProjectileDamage = 3f;
    [SerializeField]
    private float _rootDuration = 1f;
    #endregion

    #region Unity Func

    protected override void Start()
    {
        base.Start();
    }

    #endregion

    #region Built Func

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnContactWithPlayer?.Invoke(ProjectileDamage);
            StartCoroutine(GameManager.Instance.RootPlayer(null, _rootDuration));
        }

    }

    #endregion
}
