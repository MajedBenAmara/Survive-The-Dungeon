using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{

    #region Variables

    //public Sword Weapon;

    [SerializeField]
    private float _durationBetweenCombos = .5f;
    [SerializeField]
    private float _durationBetweenAttacks = .2f;
    [SerializeField]
    private List<AttackSO> _upAttacks;
    [SerializeField]
    private List<AttackSO> _downAttacks;
    [SerializeField]
    private List<AttackSO> _sideAttacks;



    public Weapon CurrentWeapon;

    private int _sideComboCounter = 0, _upComboCounter = 0, _downComboCounter = 0;
    private float _lastClickedTime;
    private float _lastComboEnd;
    private float mouseAngle;
    private List<AttackSO> _choosenAttacksSet;
    private string _comboName = "Side Combo";


    #endregion

    #region Unity Func

    void Update()
    {
        mouseAngle = CurrentWeapon.GetMouseAngle(transform);
        ChoosingAttackSet(mouseAngle);
        ExitAttack();
    }

    #endregion

    #region Built Func
    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && !GameManager.Instance.PlayerCannotTurn)
        {
            //Attack();
            ChooseCombo();
        }
    }

    private void SideCombo()
    {
        _upComboCounter = 0;
        _downComboCounter = 0;
        // we're inside a combo
        // Time.time - lastComboEnd > DurationBetweenCombos represent the duration between two set of combos,
        // for example : Atk1/Atk2/Atk3 we wait DurationBetweenCombos sec then 
        // we go into another set of attacks
        // In other words we can't start a new combos untill the DurationBetweenCombos duration end.
        if (Time.time - _lastComboEnd > _durationBetweenCombos && _sideComboCounter <= _sideAttacks.Count)
        {
            //Don't end the combo
            CancelInvoke("ResetCombo");

            // Time.time - lastClickedTime represent the duration between attacks
            // It is used to prevent any problems that could be caused by spamming the attack button
            // So we can't go for an attack to another one until that duration ends.
            if (Time.time - _lastClickedTime >= _durationBetweenAttacks)
            {
                CurrentWeapon.Anim.runtimeAnimatorController = _sideAttacks[_sideComboCounter].AnimatorOverrider;
                CurrentWeapon.Anim.Play("Attack", 0, 0);

                PlaySlashSfx(_sideComboCounter);

                _sideComboCounter++;

                _lastClickedTime = Time.time;

                if (_sideComboCounter >= _sideAttacks.Count)
                    _sideComboCounter = 0;
            }
        }
    }

    private void UpCombo()
    {
        _downComboCounter = 0;
        _sideComboCounter = 0;
        // we're inside a combo
        // Time.time - lastComboEnd > DurationBetweenCombos represent the duration between two set of combos,
        // for example : Atk1/Atk2/Atk3 we wait DurationBetweenCombos sec then 
        // we go into another set of attacks
        // In other words we can't start a new combos untill the DurationBetweenCombos duration end.
        if (Time.time - _lastComboEnd > _durationBetweenCombos && _upComboCounter <= _upAttacks.Count)
        {
            //Don't end the combo
            CancelInvoke("ResetCombo");

            // Time.time - lastClickedTime represent the duration between attacks
            // It is used to prevent any problems that could be caused by spamming the attack button
            // So we can't go for an attack to another one until that duration ends.
            if (Time.time - _lastClickedTime >= _durationBetweenAttacks)
            {
                CurrentWeapon.Anim.runtimeAnimatorController = _upAttacks[_upComboCounter].AnimatorOverrider;
                CurrentWeapon.Anim.Play("Attack", 0, 0);

                PlaySlashSfx(_upComboCounter);

                _upComboCounter++;

                _lastClickedTime = Time.time;

                if (_upComboCounter >= _upAttacks.Count)
                    _upComboCounter = 0;
            }
        }
    }

    private void DownCombo()
    {
        _sideComboCounter = 0;
        _upComboCounter = 0;
        // we're inside a combo
        // Time.time - lastComboEnd > DurationBetweenCombos represent the duration between two set of combos,
        // for example : Atk1/Atk2/Atk3 we wait DurationBetweenCombos sec then 
        // we go into another set of attacks
        // In other words we can't start a new combos untill the DurationBetweenCombos duration end.
        if (Time.time - _lastComboEnd > _durationBetweenCombos && _downComboCounter <= _downAttacks.Count)
        {
            //Don't end the combo
            CancelInvoke("ResetCombo");

            // Time.time - lastClickedTime represent the duration between attacks
            // It is used to prevent any problems that could be caused by spamming the attack button
            // So we can't go for an attack to another one until that duration ends.
            if (Time.time - _lastClickedTime >= _durationBetweenAttacks)
            {
                CurrentWeapon.Anim.runtimeAnimatorController = _downAttacks[_downComboCounter].AnimatorOverrider;
                CurrentWeapon.Anim.Play("Attack", 0, 0);

                PlaySlashSfx(_downComboCounter);

                _downComboCounter++;

                _lastClickedTime = Time.time;

                if (_downComboCounter >= _downAttacks.Count)
                    _downComboCounter = 0;
            }
        }
    }

    private void ExitAttack()
    {
        if (CurrentWeapon.GetAttackAnimationCondition(.9f))
        {
            Invoke("ResetCombo", .5f);
        }
    }

    private void ResetCombo()
    {
        _sideComboCounter = 0;
        _upComboCounter = 0;
        _downComboCounter = 0;
        _lastComboEnd = Time.time;
    }

    private void ChoosingAttackSet(float angle)
    {

        if((angle <= -135 || angle >= 135) || (angle <= 45 && angle >= -45))
        {
            //_choosenAttacksSet = SideAttacks;
            _comboName = "Side Combo";
        }

        if ((angle < 135 && angle > 45))
        {
            // _choosenAttacksSet = UpAttacks;
            _comboName = "Up Combo";

        }

        if ((angle > -135 && angle < -45))
        {
            // _choosenAttacksSet = DownAttacks;
            _comboName = "Down Combo";
        }

    }

    private void PlaySlashSfx(int counter)
    {
        switch (counter)
        {
            case 0:
                SfxManager.Instance.PlaySlashSfx(1);
                break;
            case 1:
                SfxManager.Instance.PlaySlashSfx(2);
                break;
            case 2:
                SfxManager.Instance.PlaySlashSfx(3);
                break;
            default:
                break;
        }
    }

    private void ChooseCombo()
    {
        switch (_comboName)
        {
            case "Side Combo":
                SideCombo();
                break;

            case "Up Combo":
                UpCombo();
                break;

            case "Down Combo":
                DownCombo();
                break;

            default:
                break;
        }
    }

    #endregion

}