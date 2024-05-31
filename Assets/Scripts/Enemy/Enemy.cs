using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Enemy : MonoBehaviour
{

    #region Variables
    public static event Action<float> OnInflictDamage;
    protected Animator _anim;
    protected bool _doItOnce = true;

    [Header("Movement")]
    [SerializeField]
    protected float _movementSpeed = 1f;


    [Header("Movement")]
    public float Health = 5;


    [Header("Damage")]
    public int BaseDamage = 1;
    [SerializeField]
    private float _timeBetweenTakingDamage = 1f;
    private float _momentOfTakingDamage;


    [Header("Time")]
    [SerializeField]
    protected float _timeBetweenAttacks = 1f;
    protected float _hitMoment = 0f;


    [Header("Visual Effect")]
    [SerializeField]
    protected ParticleSystem _deathEffect;
    protected EnemyFlashEffect _flashEffect;


    [Header("Burn Effect")]
    [SerializeField]
    protected float _timeBetweenBurn = 4f;
    [SerializeField]
    protected float _burnDuration = .5f;
    [SerializeField]
    private ParticleSystem _burnEffect;
    protected float _contactMoment = 0;
    protected float _burnMoment = 0;
    private bool _canBurn;


    [Header("Electric Effect")]
    [SerializeField]
    private ParticleSystem _electricEffect;

    internal bool IsElectricuted = false;

    private float _electricutedTime = 0f;
    private float _timeBetweenElectricution = 1f;


    [Header("Life Steal")]
    [SerializeField]
    private float _timeBetweenHeal = 1f;
    private float _healMoment;


    #endregion

    #region Unity Func
    protected virtual void Start()
    {
        _anim = GetComponent<Animator>();
        _flashEffect = GetComponent<EnemyFlashEffect>(); 
    }
    #endregion

    #region BuiltFunc
    protected void HandleTakingDamageFromPlayer(int damage)
    {
        if (Time.time - _momentOfTakingDamage > _timeBetweenTakingDamage)
        {
            _momentOfTakingDamage = Time.time;
            UpdateHealth(damage, "Normal");
            if (GameManager.Instance.PlayerStats.FireBuffIsActive)
            {
                _canBurn = true;
                _contactMoment = Time.time;
            }

            if (GameManager.Instance.PlayerStats.EarthBuffIsActive)
            {
                LifeSteal();
            }

            //Debug.Log("Damage Taken From Player");
        }

    }

    public void ChasePlayer()
    {
        transform.Translate((GameManager.Instance.PlayerTransform.position - transform.position).normalized * _movementSpeed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DealDamage(BaseDamage);
        }
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DealDamage(BaseDamage);
        }
        if (GameManager.Instance.PlayerStats.ThunderBuffIsActive)
        {
            if (collision.CompareTag("Enemy"))
            {
                if (collision.gameObject.GetComponent<Enemy>().IsElectricuted)
                {
                    StartCoroutine(GetElectricuted());
                    electricuted();
                }
            }
        }

    }

    public void DealDamage(float damage)
    {

        OnInflictDamage?.Invoke(damage);

    }

    // Turn the enemey to face the player 
    protected void FacePlayer()
    {
        if (GameManager.Instance.PlayerTransform.gameObject != null)
        {
            // calaculate direction from enemy to player
            Vector2 direction = GameManager.Instance.PlayerTransform.position - transform.position;

            // calculate the angle of the direction vector
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if ((angle < 90) && (angle > -90))
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);

            }
            else
            {
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
        }
    }

    protected virtual void HealthCheck()
    {
        if (Health <= 0)
        {
            if(_doItOnce)
            {
                _doItOnce = false;
                GameManager.Instance.IncreaseNumberOfKills();
                Instantiate(_deathEffect, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }

    protected void UpdateHealth(float damage, string damageType)
    {
        
        Health = Health - damage;

        switch (damageType)
        {
            case "Normal":
                _flashEffect.NormalFlash();
                break;
            case "Burn":
                _flashEffect.BurnFlash();
                _burnEffect.Play();
                break;
            case "Electro":
                _flashEffect.ElectroFlash();
                _electricEffect.Play();
                break;

            default:
                break;
        }
        HealthCheck();
      
    }

    protected float CalculateDistanceFromPlayerToEnemy()
    {
        return Vector2.Distance(transform.position, GameManager.Instance.PlayerTransform.position);
    }

    // Burn the enemy over time
    protected void Burn()
    {
        if (GameManager.Instance.PlayerStats.FireBuffIsActive)
        {
            // the enemy can only be burn when he get hit by the player
            if (_canBurn)
            {
                // the burn effect will have a duration where it can take effect
                if (Time.time - _contactMoment < _timeBetweenBurn)
                {
                    // the enemy can be burn only once every "burnDuration"
                    if (Time.time - _burnMoment > _burnDuration)
                    {
                        _burnMoment = Time.time;
                        UpdateHealth(GameManager.Instance.PlayerStats.BurnDamage, "Burn");
                    }

                }
                else
                {
                    _canBurn = false;
                }
            }
        }
    }

    private void electricuted()
    {
        if (Time.time - _electricutedTime > _timeBetweenElectricution)
        {
            _electricutedTime = Time.time;
            UpdateHealth(GameManager.Instance.PlayerStats.ElectrocDamage, "Electro");
        }
    }

    public IEnumerator GetElectricuted()
    {
        IsElectricuted = true;
        yield return new WaitForSeconds(.5f);
        IsElectricuted = false;
    }

    // Heal the player when he hit an enemy
    private void LifeSteal()
    {
        PlayerStats playerStats = GameManager.Instance.PlayerStats;
        // Heal the player every "TimeBetweenHeal"
        if (Time.time - _healMoment > _timeBetweenHeal)
        {
            GameManager.Instance.PlayerVFXControler.PlayHealEffect();
            if (playerStats.PlayerCurrentHealth == playerStats.PlayerMaxHealth)
            {
                // add Armor
                if (playerStats.ArmorAmount < playerStats.ArmorMaxAmount)
                {
                    playerStats.ArmorAmount += playerStats.LifeStealAmount;
                }
                else
                {
                    playerStats.ArmorAmount = playerStats.ArmorMaxAmount;
                }
            }
            else
            {
                // add HP
                if (playerStats.PlayerCurrentHealth < playerStats.PlayerMaxHealth)
                {
                    playerStats.PlayerCurrentHealth += playerStats.LifeStealAmount;
                }
                else
                {
                    playerStats.PlayerCurrentHealth = playerStats.PlayerMaxHealth;
                }
            }
        }

    }

    #endregion

}
