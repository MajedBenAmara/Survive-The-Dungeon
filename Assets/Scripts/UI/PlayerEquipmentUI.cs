using UnityEngine;
using UnityEngine.UI;

public class PlayerEquipmentUI : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private Image EquipmentIcon;
    [SerializeField]
    private Image SetIcon;

    #endregion

    #region Built Func

    // set the different component of the player equipment UI by it's corresponding element
    public void SetPlayerEquipmentUI(Sprite equipmentIcon, Sprite setIcon, Color equipmentColor)
    {
        EquipmentIcon.sprite = equipmentIcon;
        EquipmentIcon.color = equipmentColor;
        SetIcon.sprite = setIcon;
        SetIcon.color = new Color(SetIcon.color.r, SetIcon.color.g, SetIcon.color.b, 1);
    }

    #endregion
}
