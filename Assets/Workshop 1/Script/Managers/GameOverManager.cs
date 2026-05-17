using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private CanvasGroup gameOverCanvas;

    private void Awake()
    {
        Instance = this;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);

        StartCoroutine(FadeGameOver());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
    private IEnumerator FadeGameOver()
    {
        float duration = 1f;

        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;

            gameOverCanvas.alpha =
                Mathf.Lerp(
                    0,
                    1,
                    time / duration
                );

            yield return null;
        }

        gameOverCanvas.alpha = 1;
    }

}

