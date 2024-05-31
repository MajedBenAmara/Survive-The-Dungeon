using UnityEngine;

[CreateAssetMenu(menuName = "EquipmentSO")]
public class EquipmentSO : ScriptableObject
{
    public string EquipmentName;
    public Sprite EquipmentIcon;
    public string SetName;
    public Sprite SetIcon;
    public int Level;
    public float[] BuffValue;
    public string BuffDescription;

    // set the values of this equipment to the same calue as the input
    public void SetEquipmentValues(EquipmentSO equipment)
    {
        EquipmentName = equipment.EquipmentName;
        EquipmentIcon = equipment.EquipmentIcon;
        SetName = equipment.SetName;
        SetIcon = equipment.SetIcon;
        Level = equipment.Level;
        BuffValue = equipment.BuffValue;
        BuffDescription = equipment.BuffDescription;

    }

    // Rest the value of this equipment to it's given default value
    public void ResetToDefault()
    {
        EquipmentName = null;
        EquipmentIcon = null;
        SetName = null;
        SetIcon = null;
        Level = 1;
        BuffValue = null;
        BuffDescription = null;

    }

    public void ResetLevel()
    {
        Level = 1;
    }
    public void IncreaseLevel()
    {
        Level ++;
    }
}
