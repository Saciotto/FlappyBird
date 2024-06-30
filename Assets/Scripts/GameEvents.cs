using System;
using TMPro;
using UnityEngine;

public enum GameState
{
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
        DontDestroyOnLoad(gameObject);
    }

    public void SetCurrentState(GameState state)
    {
        if (CurrentState != state)
        {
            CurrentState = state;
            OnCurrentStateChanged?.Invoke(CurrentState);
        }
    }

    public void PlayerScores()
    {
        Score++;
        ScoreText.SetText(Score.ToString());
    }
}
