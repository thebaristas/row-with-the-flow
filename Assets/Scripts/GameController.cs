using UnityEngine;

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
    public GameObject rythmUI;

    void Awake() {
        instance = this;
    }

    void Start()
    {
        gameState = GameState.Playing;
        Conductor.instance.Play();
    }

    public void GameOver() {
        if (gameState == GameState.Playing) {
            gameState = GameState.GameOver;
            Debug.Log("Game Over");
            Stop();
            gameOverUI.SetActive(true);
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

    private void Stop() {
        Time.timeScale = 0;
    }
}
