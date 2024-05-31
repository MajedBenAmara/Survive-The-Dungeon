using UnityEngine;

public class OrcWarrior : Orc
{

    #region Variables

    [Header("Weapon Components")]
    public int AxeDamage = 2;
    public Animator AxeAnimator;

    #endregion

    #region Unity Func
    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        Burn();
        CheckDistanceToPlayer();
        FacePlayer();
    }

    #endregion

    #region Built Func
    protected override void AttackPlayer()
    {
        if (Time.time - fireMoment > _timeBetweenRangedAttacks)
        {
            AxeAnimator.Play(_weaponAttackAnim);
            _anim.Play(_orcIdleAnim);
            if (AxeAnimator.GetCurrentAnimatorStateInfo(0).IsName(_weaponAttackAnim)
                    && AxeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > .8f)
            {
                AxeAnimator.Play(_weaponIdleAnim);
                fireMoment = Time.time;
            }

        }
    }
    #endregion

}
