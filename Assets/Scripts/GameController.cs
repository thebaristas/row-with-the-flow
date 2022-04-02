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

    void Awake() {
        instance = this;
    }

    void Start()
    {
        gameState = GameState.Playing;
    }

    public void GameOver() {
        if (gameState == GameState.Playing) {
            gameState = GameState.GameOver;
            Debug.Log("Game Over");
            Stop();
        }
    }

    private void Stop() {
        Time.timeScale = 0;
    }
}
