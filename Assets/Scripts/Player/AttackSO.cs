using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Knight Attack")]
public class AttackSO : ScriptableObject
{
    public AnimatorOverrideController AnimatorOverrider;
    public int AttackDamage;
}
