using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EquipmentManager : MonoBehaviour
{
    #region Variables

    public static EquipmentManager Instance;

    [Header("Equipment Icon Colors")]
    public Color[] EquipmentColors;

    [Header("Lists")]
    [Header("Thunder Set")]
    public List<EquipmentSO> ThunderSet;

    [Header("Earth Set")]
    public List<EquipmentSO> EarthSet;

    [Header("Fire Set")]
    public List<EquipmentSO> FireSet;

    [Header("Player Current Equipment")]
    public List<EquipmentSO> PlayerEquipments;

    [Header(" Random Equipments list")]
    public List<EquipmentSO> RandomEquipments;

    [Header(" Buffs")]
    [SerializeField]
    private float[] _burnValues = { };
    [SerializeField]
    private float[] _electrocutionValues = { };
    [SerializeField]
    private float[] _lifeStealValues = { };

    [SerializeField]
    private BuffsIconManager _buffsIconManager;

    private int _fireCounter = 0;
    private int _earthCounter = 0;
    private int _thunderCounter = 0;



    internal int NumberOfEquipment = 3;

    private List<List<EquipmentSO>> _listOfSets = new List<List<EquipmentSO>>();

    private EquipmentSO _newEquipment;

    private int _playerEquipmentIndex;



    #endregion

    #region Unity Func
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Initialization();
    }
    #endregion

    #region Built Func
    private void Initialization()
    {
        // Initialize the list of sets by the different set
        _listOfSets.Add(ThunderSet);
        _listOfSets.Add(EarthSet);
        _listOfSets.Add(FireSet);

        // Reset the level of the different equipments in each given set 
        foreach (var item in _listOfSets)
        {
            ResetEquipmentLevel(item);
        }
        // Rest the player equipments components to there default values 
        foreach (var item in PlayerEquipments)
        {
            item.ResetToDefault();
        }
    }

    // pick a random set from the list of sets
    private List<EquipmentSO> PickRandomEquipmentSet()
    {
        int rand = Random.Range(0, _listOfSets.Count);
        //Debug.Log(Sets[rand]);
        return _listOfSets[rand];
    }

    // pick a random equipment from a random set if it's level is more than 5(max equipment level)
    private EquipmentSO PickRandomEquipment()
    {
        List<EquipmentSO> equipmentSet = PickRandomEquipmentSet();

        int rand = Random.Range(0, equipmentSet.Count);
        EquipmentSO equipment = equipmentSet[rand];
        //Debug.Log(equipmentSet[rand]);

        // Pick an equipment that doesn't already exist in the randomEquipment list
        while (RandomEquipments.Contains(equipmentSet[rand]))
        {
            rand = Random.Range(0, equipmentSet.Count);
            equipment = equipmentSet[rand];
        }

        // we don't pick an equipment that has a level above 5 or null

        while (equipment == null || equipment.Level > 5)
        {
            equipmentSet = PickRandomEquipmentSet();
            rand = Random.Range(0, equipmentSet.Count);
            equipment = equipmentSet[rand];
        }

        return equipmentSet[rand];
    }

    // fill the list of random equipments
    private void FillRandomEquipmentsList()
    {
        for (int i = 0; i < NumberOfEquipment; i++)
        {
            RandomEquipments.Add(PickRandomEquipment());
        }
    }

    // if the random equipment list is empty we fill it up if not we clear it from it's old values
    // and then fill it again
    public void UpdateRandomEquipmentList()
    {

        if (RandomEquipments == null)
        {
            FillRandomEquipmentsList();
        }
        else
        {
            RandomEquipments.Clear();
            FillRandomEquipmentsList();
        }
       
    }

    // pick the player equipment set using the index of the UI element that the player clicked on
    public void UpdateEquipment(int UIEquipmentIndex)
    {
        string setName = RandomEquipments[UIEquipmentIndex].SetName;
        // depanding on the equipment set 
        switch (setName)
        {
            case "Fire":
                // choose an eqipment by giving it's set and the corresponding index of that equipment
                // UI element
                ChooseEquipment(FireSet, UIEquipmentIndex);
                break;
            case "Earth":
                ChooseEquipment(EarthSet, UIEquipmentIndex);

                break;
            case "Thunder":
                ChooseEquipment(ThunderSet, UIEquipmentIndex);

                break;
            default:
                break;
        }
    }

    // A func that define what happen what player choose an equipment
    private void ChooseEquipment(List<EquipmentSO> list, int index)
    {
        // go through each equipment of the given set
        foreach (EquipmentSO item in list)
        {
            // check if that equipment does exist in the random equipment list that we generated
            if (item == RandomEquipments[index])
            {
                // check it's level
                if (RandomEquipments[index].Level <= 5)
                {
                    switch (item.EquipmentName)
                    {
                        case "Gauntlet":
                            AddEquipmentToPlayer(2, item);
                            break;
                        case "Helmet":
                            AddEquipmentToPlayer(0, item);
                            break;
                        case "Chest":
                            AddEquipmentToPlayer(1, item);
                            break;
                        case "Boots":
                            AddEquipmentToPlayer(3, item);
                            break;
                        default:
                            break;
                    }
                    //Debug.Log(item.EquipmentName + " " + item.SetName + " " + item.Level);
                }
                break;
            }
        }

    }

    private void ResetEquipmentLevel(List<EquipmentSO> list)
    {
        foreach (EquipmentSO item in list)
        {
            item.ResetLevel();
        }
    }

    // add the chosen equipment to the player equipment list and set that equipment values using the
    // corresponding index
    // where index 0 -> helmet, 1 -> Chest, 2 -> Gauntlet, 3 -> Boots
    private void AddEquipmentToPlayer(int index, EquipmentSO equipment)
    {
        _playerEquipmentIndex = index;
        // if the chosen player equipment is empty or the chosen equipment is from the same set as the 
        // equipped one we don't swap
        if (PlayerEquipments[index].EquipmentIcon == null || PlayerEquipments[index].SetName == equipment.SetName)
        {
            PlayerEquipments[index].SetEquipmentValues(equipment);

            GameManager.Instance.PlayerStats.UpgradePlayerStats(PlayerEquipments[index].EquipmentName,
                PlayerEquipments[index].BuffValue[PlayerEquipments[index].Level-1]);

            equipment.IncreaseLevel();
            //Debug.Log(equipment.EquipmentName + " " + equipment.Level);

            UpdatePLayerUIEquipments();

            CheckSetEffect();

            EquipmentWindow.Instance.HideEquipmentSelectionWindow();
            //Debug.Log("Equip When list is null");

        }
        else 
        {

            _newEquipment = equipment;
            EquipmentWindow.Instance.ActivateSwapNotificationWindow();


        }

    }

    // Swap between the current equipment that the player has and the one he chose when he click on the 
    // swap button
    public void AcceptSwap()
    {
        ResetEquipmentLevel(PlayerEquipments[_playerEquipmentIndex]);

        PlayerEquipments[_playerEquipmentIndex].SetEquipmentValues(_newEquipment);

        GameManager.Instance.PlayerStats.UpgradePlayerStats(PlayerEquipments[_playerEquipmentIndex].EquipmentName,
            PlayerEquipments[_playerEquipmentIndex].BuffValue[PlayerEquipments[_playerEquipmentIndex].Level]);

        _newEquipment.IncreaseLevel();

        //Debug.Log("Swap");

        UpdatePLayerUIEquipments();

        CheckSetEffect();

        EquipmentWindow.Instance.HideEquipmentSelectionWindow();

        EquipmentWindow.Instance.DesactivateSwapNotificationWindow();
    }

    // desactivate the swap window when click on the decline button
    public void DeclineSwap()
    {
        EquipmentWindow.Instance.DesactivateSwapNotificationWindow();
    }

    // Reset the level of all the equipment in all the sets
    private void ResetEquipmentLevel(EquipmentSO equip)
    {
        foreach (var item in _listOfSets)
        {

            for (int i = 0; i < item.Count; i++)
            {
                if (equip.EquipmentName == item[i].EquipmentName && equip.SetName == item[i].SetName)
                {
                    item[i].ResetLevel();
                    break;
                }
            }

        }
    }
    // go through the list of player equipment and update his equipments UI
    private void UpdatePLayerUIEquipments()
    {
        for (int i = 0; i < PlayerEquipments.Count; i++)
        {
            if(PlayerEquipments[i].EquipmentIcon != null)
            {
                PlayerEquipmentUIManager.Instance.UpdatePlayerEquipmentsUI(i,
                    PlayerEquipments[i].EquipmentIcon, PlayerEquipments[i].SetIcon,
                        EquipmentColors[PlayerEquipments[i].Level - 1]);
            }

        }
    }

    // check if the player have all equipment from the same set
    private void CheckSetEffect()
    {
        // after picking an equip 
        // asseign a counter for every set that increase every time 
        //  there's an equipment from the counter's set

        _fireCounter = 0;
        _earthCounter = 0;
        _thunderCounter = 0;

        // go through the player equip list
        foreach (var item in PlayerEquipments)
        {
            switch (item.SetName)
            {
                case "Fire":
                    _fireCounter++;
                    break;

                case "Thunder":
                    _thunderCounter++;
                    break;

                case "Earth":
                    _earthCounter++;
                    break;

                default:
                    break;
            }
        }

        ActivateSetEffect();

        SetBuffValue();

    }

    // activate a specific effect when player has all his equipment from the same set
    private void ActivateSetEffect()
    {

        PlayerStats playerStats = GameManager.Instance.PlayerStats;

        if (_fireCounter < 2)
        {
            playerStats.FireBuffIsActive = false;
            _buffsIconManager.ActivateFireIcon(false);
        }
        else
        {
            playerStats.FireBuffIsActive = true;
            _buffsIconManager.ActivateFireIcon(true);
        }

        if (_thunderCounter < 2)
        {
            playerStats.ThunderBuffIsActive = false;
            _buffsIconManager.ActivateThunderIcon(false);
        }
        else
        {
            playerStats.ThunderBuffIsActive = true;
            _buffsIconManager.ActivateThunderIcon(true);
        }

        if (_earthCounter < 2)
        {
            playerStats.EarthBuffIsActive = false;
            _buffsIconManager.ActivateEarthIcon(false);
        }
        else
        {
            playerStats.EarthBuffIsActive = true;
            _buffsIconManager.ActivateEarthIcon(true);
        }

/*        Debug.Log("Fire buff active = " + playerStats.FireBuffIsActive);
        Debug.Log("Thunder buff active = " + playerStats.ThunderBuffIsActive);
        Debug.Log("Earth buff active = " + playerStats.EarthBuffIsActive);*/


    }

    private void SetBuffValue()
    {
        PlayerStats playerStats = GameManager.Instance.PlayerStats;

        if(_fireCounter >= 2)
        {
            playerStats.BurnDamage = _burnValues[_fireCounter - 2];
            //Debug.Log("BurnDamage = " + _burnValues[_fireCounter - 2]);
        }
        if (_thunderCounter >= 2)
        {
            playerStats.ElectrocDamage = _electrocutionValues[_thunderCounter - 2];
            //Debug.Log("ElectrocDamage = " + _electrocutionValues[_thunderCounter - 2]);
        }
        if (_earthCounter >= 2)
        {
            playerStats.LifeStealAmount = _lifeStealValues[_earthCounter - 2];
            //Debug.Log("LifeStealAmount = " + _lifeStealValues[_earthCounter - 2]);
        }

    }
    #endregion
}

