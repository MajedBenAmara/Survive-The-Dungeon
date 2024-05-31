using UnityEngine;

public class ScoreboardHolder : MonoBehaviour
{
    private void OnEnable()
    {
        Scoreboard.Instance.UpdateUI(Scoreboard.Instance.SavedScores);
    }
}
