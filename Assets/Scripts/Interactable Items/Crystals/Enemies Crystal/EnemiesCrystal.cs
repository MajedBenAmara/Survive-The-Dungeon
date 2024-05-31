using System.Collections;
using UnityEngine;

public class EnemiesCrystal : Crystal
{
    #region Variabales
    [Header("Enemies Spawning Elements")]
    [SerializeField]
    private SpawnPoint[] SpawnPoints = { };
    [SerializeField]
    private int _totalEnemyNumber = 0;
    private bool _activateEffects = true;
    #endregion

    #region Unity Specific Func
    protected void Start()
    {
        _totalEnemyNumber = 0;
        CalculateSpawnedEnemiesTotalNumber();
        HideEnemiesSpawnPoints();
        OpenDoor();
    }

    private void Update()
    {
        ShowInteractionButton();
        CheckPlayerInteraction();
        CheckIfCrystalCleared();
    }

    #endregion

    #region Built Func
    // Describe what happen when player get close to the gem
    protected override void InteractWithPlayer()
    {
        GameManager.Instance.PlayerStats.NumberOfKilledEnemies = 0;
        CloseDoor();
        ChangeCrystalColour();
        ShowEnemySpawnPoints();
    }


    private void ShowEnemySpawnPoints()
    {
        foreach (SpawnPoint item in SpawnPoints)
        {
            item.gameObject.SetActive(true);
            item.CreatedEnemiesCounter = 0;
        }
    }

    private void HideEnemiesSpawnPoints()
    {
        foreach (SpawnPoint item in SpawnPoints)
        {
            item.gameObject.SetActive(false);
        }
    }

    private void CheckIfCrystalCleared()
    {
        if(_activateEffects)
        {
            if (_totalEnemyNumber == GameManager.Instance.PlayerStats.NumberOfKilledEnemies
                    && _spriteRenderer.color == endColor)
            {
                //Debug.Log("player kill score = " + GameManager.Instance.PlayerStats.NumberOfKilledEnemies);
                StartCoroutine(ClearingDungeonRoom());
            }
        }

    }


    private void PlaySmallCrystalEffect()
    {
        foreach (SpawnPoint item in SpawnPoints)
        {
            item.ActivateDestructionEffect();
        }
    }

    // What happen when player clear a dungeon gem
    IEnumerator ClearingDungeonRoom()
    {
        HideEnemiesSpawnPoints();
        PlaySmallCrystalEffect();
        yield return new WaitForSeconds(.5f);
        OpenDoor();
        HideCrystal();
        yield return new WaitForSeconds(1f);
        EquipmentWindow.Instance.ShowEquipmentSelectionWindow();
        EquipmentWindow.Instance.UpdateUI();
        GameManager.Instance.ResetKillScore();
        _activateEffects = false;
        StopAllCoroutines();
    }

    private void CalculateSpawnedEnemiesTotalNumber()
    {
        foreach (var item in SpawnPoints)
        {
            _totalEnemyNumber += item.SpawnedEnemyNumber;
        }
        //Debug.Log("totalEnemyNumber = " + _totalEnemyNumber);
    }
    #endregion



}
