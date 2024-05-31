using TMPro;
using UnityEngine;

public class PostClearUI : ButtonsBehaviour
{
    [SerializeField]
    private GameTimer _timer;
    [SerializeField]
    private TextMeshProUGUI _timerText;
    [SerializeField]
    private GameObject _playerUI;
    [SerializeField]
    private GameObject _postClearUIContainer;
    [SerializeField]
    private Scoreboard _scoreboard;

    public void ActivatePostClearUI(bool showScore)
    {
        GameManager.Instance.StopTheGame();

        _playerUI.SetActive(false);
        _postClearUIContainer.SetActive(true);
        _timerText.gameObject.SetActive(false);
        if (showScore)
        {
            ScoreboardEntryData scoreboardEntryData = new ScoreboardEntryData();
            scoreboardEntryData.EntryScoreNumber = _timer.Score;
            scoreboardEntryData.EntryScoreText = _timer.GetClearTime();
            _scoreboard.AddEntry(scoreboardEntryData);
            _timerText.gameObject.SetActive(true);
            _timerText.text += _timer.GetClearTime();
        }

    }
}
