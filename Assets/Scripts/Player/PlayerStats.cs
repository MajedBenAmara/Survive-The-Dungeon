using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    #region Variables

    [Header("Health")]
    [SerializeField]
    private int _playerDefaultMaxHealth = 50;
    public float PlayerCurrentHealth;
    public float PlayerMaxHealth;
    public int PotionHealthAmount = 30;
    public float ArmorMaxAmount = 75;
    public float ArmorAmount = 0f;


    [Header("Movement")]
    [SerializeField]
    private float _playerDefaultMovementSpeed = .1f;
    public float PlayerMovementSpeed = .1f;


    [Header("Combat")]
    [SerializeField]
    private float _playerDefaultDamage = 8f;
    public float PlayerDamage;
    public float AttackSpeed = 1f;
    public float TimeBetweenTakingDamage = .5f;
    float _takingDamageMoment = 0f;


    [Header("Score")]
    public int NumberOfKilledEnemies = 0;
    public int NumberOfPotion;
    public int RerollPoints = 0;
    public int NumberOfKeys = 0;


    [Header("Effect")]
    public GameObject DeathEffect;


    [Header("Buffs")]

    [Header("Fire Buff")]
    public bool FireBuffIsActive = false;
    public float BurnDamage = .5f;

    [Header("Thunder Buff")]
    public bool ThunderBuffIsActive = false;
    public float ElectrocDamage = .5f;
    public float TimeBetweenElectrocution = 1f;

    [Header("Earth Buff")]
    public bool EarthBuffIsActive = false;
    public float TimeBetweenHeal = 1f;
    public float LifeStealAmount = 1f;



    #endregion

    #region Unity Func

    private void Start()
    {
        Enemy.OnInflictDamage += HandleTackingDamage;
        EnemyProjectil.OnContactWithPlayer += HandleTackingDamage;

        PlayerCurrentHealth = _playerDefaultMaxHealth;
        PlayerDamage = _playerDefaultDamage;
        PlayerMovementSpeed = _playerDefaultMovementSpeed;
    }

    private void Update()
    {
       StartCoroutine(CheckHealth());
    }

    #endregion

    #region Built Func
    private void HandleTackingDamage(float damage)
    {
        //Debug.Log("damage = " + damage);
        //Debug.Log("Damaged Tacken from Enemy =  " + damageAmount);

        if(Time.time - _takingDamageMoment >= TimeBetweenTakingDamage)
        {
            _takingDamageMoment = Time.time;
            GameManager.Instance.PlayerTookDamage = true;
            GameManager.Instance.PlayerFlashEffect.NormalFlash();
            if(ArmorAmount <= 0)
            {
                ArmorAmount = 0;
                PlayerCurrentHealth -= damage;
            }
            else
            {
                ArmorAmount -= damage;
            }
        }

    }

    private IEnumerator CheckHealth()
    {
        if (PlayerCurrentHealth <= 0)
        {
            SfxManager.Instance.PlayPlayerDeathSfx();
            Instantiate(DeathEffect, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(.5f);
            GameManager.Instance.ClearingIU.ActivatePostClearUI(false);
            gameObject.SetActive(false);
        }
    }

    private void UpgradePlayerMaxHP(float value)
    {
        PlayerMaxHealth = _playerDefaultMaxHealth + value ;
    }

    private void UpgradePlayerDamage(float value)
    {
        //Debug.Log("player damage Bonus = " + value * _playerDefaultDamage);
        PlayerDamage = _playerDefaultDamage + value ;
    }

    private void UpgradePlayerMovementSpeed(float value)
    {
        PlayerMovementSpeed = _playerDefaultMovementSpeed + value ;
    }

    private void UpgradePlayerAttackSpeed(float value)
    {
        GameManager.Instance.PlayerCombat.CurrentWeapon.Anim.speed += value;
        AttackSpeed = GameManager.Instance.PlayerCombat.CurrentWeapon.Anim.speed += value;

    }

    public void UpgradePlayerStats(string equipmentName ,float value)
    {
        switch (equipmentName)
        {
            case "Gauntlet":
                UpgradePlayerDamage(value);
                break;
            case "Helmet":
                UpgradePlayerAttackSpeed(value);
                break;
            case "Chest":
                UpgradePlayerMaxHP(value);
                break;
            case "Boots":
                UpgradePlayerMovementSpeed(value);
                break;
            default:
                break;
        }
    }

    public void HealPlayer()
    {
        if(PlayerCurrentHealth < PlayerMaxHealth)
        {
            PlayerCurrentHealth += PotionHealthAmount;
            GameManager.Instance.PlayerVFXControler.PlayHealEffect();
            if (PlayerCurrentHealth > PlayerMaxHealth)
            {
                PlayerCurrentHealth = PlayerMaxHealth;
            }
        }
    }

    public IEnumerator DealPoisonDamage(float damage, float timeBetweenDamage, int totalCount)
    {

        for (int i = 0; i < totalCount; i++)
        {
            if(ArmorAmount <= 0)
            {
                PlayerCurrentHealth -= damage;
            }
            else
            {
                ArmorAmount -= damage;
            }
            GameManager.Instance.PlayerVFXControler.PlayDrainEffect();
            GameManager.Instance.PlayerFlashEffect.DrainFlash();
            yield return new WaitForSeconds(timeBetweenDamage);
        }
        GameManager.Instance.PlayerVFXControler.StopDrainEffect();

    }

    #endregion

}