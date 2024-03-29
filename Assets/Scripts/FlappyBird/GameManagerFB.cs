using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerFB : MonoBehaviour
{
    public GameObject gameOverCanvas;

    private void Start()
    {
        gameOverCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void Replay()
    {
        // SceneManager.LoadScene(0);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
