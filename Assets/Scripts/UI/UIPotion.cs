using TMPro;
using UnityEngine;

public class UIPotion : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _potionNumber;

    public void UpdateUI(int potionNumber)
    {
        _potionNumber.text = potionNumber.ToString();
    }
}
