using UnityEngine;

public class PlayerArmorBar : MonoBehaviour
{
    [SerializeField]
    private float _armorBarMaxScale = 7.5f;
    [SerializeField]
    private RectTransform _armorBarImage;

    private void Update()
    {
        UpdateArmorBar();
    }
    public void UpdateArmorBar()
    {
        // Debug.Log("Hp bar percentage = " + HpbarScale());
        _armorBarImage.localScale = new Vector2(ArmorbarScale(), _armorBarImage.localScale.y);
    }
    private float ArmorbarScale()
    {
        float currentScale;
        //Debug.Log("player Hp percentage = " + CalculatePlayerHpPercentage());

        return currentScale = _armorBarMaxScale * CalculatePlayerArmorPercentage();
    }
    private float CalculatePlayerArmorPercentage()
    {
        float percentage;
        return percentage = GameManager.Instance.PlayerStats.ArmorAmount /
           GameManager.Instance.PlayerStats.ArmorMaxAmount;
    }
}
