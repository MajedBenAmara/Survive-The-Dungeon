using UnityEngine;

public class PlayerEquipmentUIManager : MonoBehaviour
{
    #region Variables

    public static PlayerEquipmentUIManager Instance;
    [SerializeField]
    private PlayerEquipmentUI[] _playerEquipmentsUI;
    [SerializeField]
    private GameObject[] _playerUI;
    #endregion

    #region Unity Func
    private void Start()
    {
        Instance = this;
    }
    #endregion

    #region Built Func
    // each time there's a change of equipments this func will update the UI of the corresponding player
    // equipment indicated by it's index
    // where index 0 -> helmet, 1 -> Chest, 2 -> Gauntlet, 3 -> Boots 
    public void UpdatePlayerEquipmentsUI(int index, Sprite equipmentIcon, Sprite setIcon, Color equipmentColor)
    {
        _playerEquipmentsUI[index].SetPlayerEquipmentUI(equipmentIcon, setIcon, equipmentColor);
    }

    public void HidePLayerEquipmentUI()
    {
        foreach (var item in _playerUI)
        {
            item.SetActive(false);

        }
    }

    public void ShowPLayerEquipmentUI()
    {
        foreach (var item in _playerUI)
        {
            item.SetActive(true);
        }
    }

    #endregion
}
