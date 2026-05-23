using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    private bool isPaused = false;

    public void TogglePause()
    {
        isPaused = !isPaused;

        pauseMenu.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0;

            AudioListener.pause = true;
        }
        else
        {
            Time.timeScale = 1;

            AudioListener.pause = false;
        }
    }

    public void ResumeGame()
    {
        isPaused = false;

        pauseMenu.SetActive(false);

        Time.timeScale = 1;

        AudioListener.pause = false;
    }

    public void ExitGame()
    {
        Time.timeScale = 1;

        AudioListener.pause = false;

        SceneManager.LoadScene(0);
    }
}