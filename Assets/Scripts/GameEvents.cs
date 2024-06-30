using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Init,
    Alive,
    Dead,
    GameOver
}

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance { get; private set; }

    public GameState CurrentState;
    public event Action<GameState> OnCurrentStateChanged = null;
    public TextMeshProUGUI ScoreText;
    public int Score = 0;
    public int BestScore = 0;
    public Rigidbody2D Player;

    public Canvas GameOverCanvas;
    public Canvas InitCanvas;

    public TextMeshProUGUI FinalScoreText;
    public TextMeshProUGUI BestScoreText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        BestScore = PlayerPrefs.GetInt("BestScore");
    }

    private void StartGame()
    {
        Player.simulated = true;
        ScoreText.gameObject.SetActive(true);
        InitCanvas.gameObject.SetActive(false);
    }

    private void ShowGameOver()
    {
        if (Score > BestScore)
        {
            BestScore = Score;
            PlayerPrefs.SetInt("BestScore", BestScore);
        }
        FinalScoreText.SetText(Score.ToString());
        BestScoreText.SetText(BestScore.ToString());
        ScoreText.gameObject.SetActive(false);
        GameOverCanvas.gameObject.SetActive(true);
    }

    public void SetCurrentState(GameState state)
    {
        if (CurrentState != state)
        {
            CurrentState = state;
            OnCurrentStateChanged?.Invoke(CurrentState);

            if (CurrentState != GameState.Init)
            {
                StartGame();
            }

            if (CurrentState == GameState.GameOver)
            {
                ShowGameOver();
            }
        }
    }

    public void PlayerScores()
    {
        Score++;
        ScoreText.SetText(Score.ToString());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

}
