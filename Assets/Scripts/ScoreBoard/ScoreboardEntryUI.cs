using TMPro;
using UnityEngine;

public class ScoreboardEntryUI : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI _entryScoreText = null;

    public void Initialize(ScoreboardEntryData scoreboardEntryData)
    {
        _entryScoreText.text = scoreboardEntryData.EntryScoreRank.ToString() + "." + " " 
            + scoreboardEntryData.EntryScoreText;
    }

    private void Update()
    {
        if(transform.parent==null)
        {
            Destroy(gameObject);
        }
    }

}
