using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MenuBehaviour
{
    [SerializeField]
    private GameObject _pauseMenuContainer;
    [SerializeField]
    private GameObject _playerUI;
    [SerializeField]
    private GameObject _postClearUI;
    [SerializeField]
    private GameObject _upgradeEquipmentUI;

    private OptionMenuContainer _optionMenuContainer;

    [Header("Plauer Stats")]
    [SerializeField]
    private TextMeshProUGUI _playerHP;
    [SerializeField]
    private TextMeshProUGUI _playerATK;
    [SerializeField]
    private TextMeshProUGUI _playerAtckSpd;
    [SerializeField]
    private TextMeshProUGUI _playerMS;

    private void Start()
    {
        _optionMenuContainer = GameObject.FindGameObjectWithTag("OptionMenu").GetComponent<OptionMenuContainer>();
    }

    private void UpdatePlayerStats()
    {
        PlayerStats playerStats = GameManager.Instance.PlayerStats;

        _playerHP.text = Math.Round(playerStats.PlayerCurrentHealth, 2) + "/" + playerStats.PlayerMaxHealth;
        _playerATK.text = playerStats.PlayerDamage.ToString();
        _playerAtckSpd.text = playerStats.AttackSpeed.ToString();
        _playerMS.text = playerStats.PlayerMovementSpeed.ToString(); 
    }


    public void ManagePauseMenuContainer(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(_pauseMenuContainer.activeInHierarchy)
            {
                if (!_optionMenuContainer.OptionMenuIsActive)
                {
                    DeactivatePauseMenu();
                }
            }
            else
            {
                if(!_postClearUI.activeInHierarchy && !_upgradeEquipmentUI.activeInHierarchy)
                    ActivatePauseMenu();
            }
        }

    }

    private void ActivatePauseMenu()
    {
        Time.timeScale = 0f;
        GameManager.Instance.PlayerCannotTurn = true;
        _playerUI.SetActive(false);
        _pauseMenuContainer.SetActive(true);
        UpdatePlayerStats();
    }

    public void DeactivatePauseMenu()
    {
        Time.timeScale = 1f;
        GameManager.Instance.PlayerCannotTurn = false;
        _playerUI.SetActive(true);
        _pauseMenuContainer.SetActive(false);
    }

}
