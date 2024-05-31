using UnityEngine;

public class MainMenu : MenuBehaviour
{
    [SerializeField]
    private GameObject _scoreboard;
    [SerializeField]
    private GameObject _tutorialPanel;

    public void ShowScoreBoardMenu()
    {
        _scoreboard.SetActive(true);
    }

    public void HideScoreBoardMenu()
    {
        _scoreboard.SetActive(false);
    }
    public void ShowTutorial()
    {
        _tutorialPanel.SetActive(true);
    }

    public void HideTutorial()
    {
        _tutorialPanel.SetActive(false);
    }



}
