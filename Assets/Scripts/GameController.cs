using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

enum GameState {
        Playing,
        Pause,
        GameOver,
    }

public class GameController : MonoBehaviour
{
    public static GameController instance;
    GameState gameState;

    public GameObject playerPrefab;
    public Transform playerSpawnPoint;

    public GameObject gameOverUI;
    public Text TimerText;
    public GameObject rythmUI;

    void Awake() {
        instance = this;
    }

    void Start()
    {
        ResetGame();
        Conductor.instance.Play();
    }

    public void GameOver() {
        if (gameState == GameState.Playing) {
            gameState = GameState.GameOver;
            Debug.Log("Game Over");
            Stop();
            gameOverUI.SetActive(true);
            TimerText.text = "Play time: " + Conductor.instance.GetPlayTime();
            rythmUI.SetActive(false);
        }
    }

    void ResetGame() {
        gameOverUI.SetActive(false);
        rythmUI.SetActive(true);
        Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        ItemSpawner.Instance.Reset();
        Time.timeScale = 1;
        Conductor.instance.Play();
        gameState = GameState.Playing;
    }

    public void Restart() {
        ResetGame();
    }

    public void OpenMainMenuScene() {
        Conductor.instance.Stop();
        SceneManager.LoadScene(0);
    }

    public void QuitGame() {
        Application.Quit();
    }

    private void Stop() {
        Time.timeScale = 0;
        Conductor.instance.Stop();
    }
}
