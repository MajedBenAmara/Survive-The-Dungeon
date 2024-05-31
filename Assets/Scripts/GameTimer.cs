using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private int minutes;
    private int seconds;
    private int milliseconds;
    private string counterText;

    internal float Score;
    public void CalculateTime()
    {

        minutes = (int)(Time.timeSinceLevelLoad / 60f) % 60;

        seconds = (int)(Time.timeSinceLevelLoad % 60f);

        milliseconds = (int)(Time.timeSinceLevelLoad * 1000f) % 1000;

        Score = minutes * 100000 + seconds * 1000 + milliseconds;
    }

    public string GetClearTime()
    {
        counterText = minutes.ToString("D2") + ":" +
            seconds.ToString("D2") + ":" + milliseconds.ToString("D2");
        return counterText;
    }
}
