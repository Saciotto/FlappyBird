using System;
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
}
