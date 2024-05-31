using UnityEngine;

public class OrcShaman : Orc
{

    #region Variables
    private ShamanStaff _orcStaff;

    [Header("Attack Stats")]
    public int projectilDamage = 3;
    #endregion

    #region Unity Func
    protected override void Start()
    {
        base.Start();
        _orcStaff = GetComponentInChildren<ShamanStaff>();
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
            _anim.Play(_weaponAttackAnim);
            _anim.Play(_orcIdleAnim);
            if (_anim.GetCurrentAnimatorStateInfo(1).IsName(_weaponAttackAnim)
                    && _anim.GetCurrentAnimatorStateInfo(1).normalizedTime > .8f)
            {
                _orcStaff.CreateProjectil();
                fireMoment = Time.time;
            }
        }
        else
        {
            _anim.Play(_weaponIdleAnim);
        }
    }
    #endregion


}
