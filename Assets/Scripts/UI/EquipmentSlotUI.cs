using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour
{
    #region Variables 
    [SerializeField]
    private TextMeshProUGUI Description;
    [SerializeField]
    private Image EquipmentIcon;
    [SerializeField]
    private Image SetIcon;
    [SerializeField]
    private TextMeshProUGUI EquipmentName;
    #endregion

    #region Built Func
    // Initialize the different component of the equipment UI slot by it's corresponding element 
    public void Initialize(EquipmentSO equip, Color equipmentColor)
    {
        string startText = " Give Extra ";
        string buffValue;
        if (equip.BuffValue[equip.Level - 1] < 1f)
        {
             buffValue = ((equip.BuffValue[equip.Level - 1]) * 100).ToString() + "%" + " ";
        }
        else
        {
             buffValue = (equip.BuffValue[equip.Level - 1]).ToString() + " ";
        }
        string endText = "";
        switch (equip.EquipmentName)
        {
            case "Gauntlet":
                endText = "<color=#EF6843>" + "Damage." + "</color>";
                break;
            case "Helmet":
                endText = "<color=#27E3F0>" + "Attack Speed." + "</color>";
                break;
            case "Chest":
                endText = "<color=#00EF18>" + "Health." + "</color>";
                break;
            case "Boots":
                endText = "<color=#EFE900>" +  "Movement Speed." + "</color>";
                break;
            default:
                break;
        }

        buffValue = "<color=#F06CD4>" + buffValue + "</color>";
        Description.text = startText + buffValue + endText;
        EquipmentIcon.sprite = equip.EquipmentIcon;
        EquipmentIcon.color = equipmentColor;
        SetIcon.sprite = equip.SetIcon;
        EquipmentName.text = equip.EquipmentName + " " + "Lv " + equip.Level;
    }

    // This will be called when player click on one of the equipment in the equipment selection screen
    public void OnUpgradePick()
    {
        EquipmentManager.Instance.UpdateEquipment(transform.GetSiblingIndex());
        //Debug.Log("Equipment picked: " + EquipmentName + " Equipment Set: " + SetName + " Level: " + Level.ToString());

    }
    #endregion
}
