using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region Variables

    public static GameManager Instance;

    public UIPotion UIPotion;
    public GameObject PlayerGameObject;
    public GameTimer Timer;
    public PostClearUI ClearingIU;

    internal bool PlayerTookDamage = false; 
    internal bool PlayerCannotTurn = false;
    internal bool PlayerCanMove = true;
    internal PlayerControler PlayerControler;
    internal PlayerStats PlayerStats;
    internal Transform PlayerTransform;
    internal PlayerCombat PlayerCombat;
    internal PlayerInputManager PlayerInputManager;
    internal PlayerFlashEffect PlayerFlashEffect;
    internal PlayerVFXControler PlayerVFXControler;
    internal bool PlayerIsInteracting = false;
    internal bool InteractionButtonPressed = false;
    internal bool SkipButtonPresses = false;


    private float _fearMoment;
    private float _duration;

    #endregion

    #region Unity Func

    private void Awake()
    {
        Instance = this;
        PlayerControler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControler>();
        PlayerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        PlayerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        PlayerInputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputManager>();
        PlayerFlashEffect = GetComponent<PlayerFlashEffect>();
        PlayerVFXControler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerVFXControler>();
    }
    void Start()
    {

    }
    private void Update()
    {
        Timer.CalculateTime();

        StartPlayerDeath();

        DeactivateFear();
    }

    private void DeactivateFear()
    {
        if (Time.time - _fearMoment > _duration)
        {
            PlayerStats.PlayerMovementSpeed = Mathf.Abs(PlayerStats.PlayerMovementSpeed);
            PlayerVFXControler.StopFearEffect();

        }
    }

    private void StartPlayerDeath()
    {
        if (PlayerStats.PlayerCurrentHealth <= 0)
        {
            StartCoroutine(PlayerDeath());
        }
    }

    #endregion

    #region Built Func

    public void IncreaseNumberOfKills()
    {
        if (PlayerGameObject != null)
            PlayerStats.NumberOfKilledEnemies++;
    }

    IEnumerator PlayerDeath()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void ResetKillScore()
    {
        PlayerStats.NumberOfKilledEnemies = 0;
    }

    public void StopTheGame()
    {
        Time.timeScale = 0f;
    }
    public void ResumeTheGame()
    {
        Time.timeScale = 1f;
    }

    public void FearPlayer(float fearDuration)
    {
        _duration = fearDuration;
        _fearMoment = Time.time;
        PlayerStats.PlayerMovementSpeed = -Mathf.Abs(PlayerStats.PlayerMovementSpeed); 
        PlayerVFXControler.PlayFearEffect();

    }

    // root the player for _rootDuration then free him and destroy this game object
    public IEnumerator RootPlayer(GameObject gameObjectToDestroy, float rootDuration)
    {

        if (PlayerCanMove)
        {
            PlayerControler.Rb.velocity = Vector2.zero;
            PlayerCanMove = false;
            PlayerFlashEffect.RootFlash();
            PlayerVFXControler.PlayRootEffect();

            yield return new WaitForSeconds(rootDuration);
            PlayerVFXControler.StopRootEffect();
            PlayerCanMove = true;
            //Debug.Log("PlayerCanMove = " + GameManager.Instance.PlayerStunned);

            Destroy(gameObjectToDestroy);
        }

    }

    public IEnumerator ShowEquipmentSelectionUI()
    {
        yield return new WaitForSeconds(1f);
        EquipmentWindow.Instance.ShowEquipmentSelectionWindow();
        EquipmentWindow.Instance.UpdateUI();
    }
    #endregion

}
