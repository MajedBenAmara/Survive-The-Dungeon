using TMPro;
using UnityEngine;

public class ButcherKnife : MonoBehaviour
{
    internal int Stakes = 0;
    [SerializeField]
    private OrcWarrior _stakesDemon;

    [SerializeField]
    private TextMeshProUGUI _stakesNumberText;
    private float _stakesDamage; 
    private void Start()
    {
        _stakesDamage = _stakesDemon.AxeDamage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Stakes++;
            _stakesNumberText.text = Stakes.ToString();
            if (Stakes >= 3)
            {
                Stakes = 0;
                _stakesNumberText.text = Stakes.ToString();
                collision.gameObject.SendMessage("HandleTackingDamage", _stakesDamage);
            }
        }
    }
}
