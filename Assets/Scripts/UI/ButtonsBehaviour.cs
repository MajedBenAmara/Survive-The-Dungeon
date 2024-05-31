using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsBehaviour : MonoBehaviour
{

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
