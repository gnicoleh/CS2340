using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerFB : MonoBehaviour
{
    public GameObject gameOverCanvas;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
    }

    public void Replay()
    {
        // SceneManager.LoadScene(0);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
