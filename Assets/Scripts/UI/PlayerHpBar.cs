using UnityEngine;

public class PlayerHpBar : MonoBehaviour
{
    [SerializeField]
    private float _hpBarMaxScale = 7.5f;
    [SerializeField]
    private RectTransform _hpBarImage;

    private void Update()
    {
        UpdateHpBar();
    }
    public void UpdateHpBar()
    {
       // Debug.Log("Hp bar percentage = " + HpbarScale());
        _hpBarImage.localScale = new Vector2(HpbarScale(), _hpBarImage.localScale.y);
    }
    private float HpbarScale()
    {
        float currentScale;
        //Debug.Log("player Hp percentage = " + CalculatePlayerHpPercentage());

        return currentScale =  _hpBarMaxScale * CalculatePlayerHpPercentage();
    }
    private float CalculatePlayerHpPercentage()
    {
        float percentage;
        return percentage =GameManager.Instance.PlayerStats.PlayerCurrentHealth /
           GameManager.Instance.PlayerStats.PlayerMaxHealth;
    }
}
