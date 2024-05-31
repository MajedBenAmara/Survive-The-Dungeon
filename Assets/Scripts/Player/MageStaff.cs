using UnityEngine;

public class MageStaff : Weapon
{

    #region Variables

    [SerializeField]
    private float _rotationGap;
    [SerializeField]
    private Transform FireOrigin;
    [SerializeField]
    private Projectil _projectil;

    #endregion

    #region Unity Func

    protected override void Update()
    {
        base.Update();
        GetMouseDirection(FireOrigin, FireOrigin);
        CreateFireBall();
    }

    #endregion

    #region Built Func

    public void CreateFireBall()
    {
        if (!GameManager.Instance.PlayerCannotTurn)
        {
            if (GetAttackAnimationCondition(.9f))
            {
                Instantiate(_projectil.gameObject, FireOrigin.position, FireOrigin.rotation);
            }
        }
    }

    #endregion

}