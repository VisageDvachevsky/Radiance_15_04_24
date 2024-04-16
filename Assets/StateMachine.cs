using UnityEngine;
using System;

public class StateMachine : MonoBehaviour
{
    public enum GameState
    {
        CardNotPressed,     // Карточка не нажата
        MiniGameStarted,    // Миниигра началась
        MiniGameCompleted,  // Миниигра завершена
        NextCard            // Переход к начальному состоянию
    }

    public static StateMachine Instance { get; private set; }

    private GameState currentState;

    public event Action OnCardNotPressedEnter;
    public event Action OnMiniGameStartedEnter;
    public event Action OnMiniGameCompletedEnter;
    public event Action OnNextCardEnter;

    // Синглтон - бан, синглтон в Start() - расстрел, затем бан
    private void Awake()
    {
        if (Instance != null)
        {
            throw new Exception($"More than one instance of {GetType().Name} on scene");
        }
        Instance = this;
    }

    private void Start()
    {
        currentState = GameState.CardNotPressed;
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameState.CardNotPressed:
                CheckCardPress();
                break;
            case GameState.MiniGameStarted:
                break;
            case GameState.MiniGameCompleted:
                GoToNextCard();
                break;
            case GameState.NextCard:
                ResetGame();
                break;
        }
    }

    private void CheckCardPress()
    {
        currentState = GameState.MiniGameStarted;
        OnMiniGameStartedEnter?.Invoke();
    }

    public void MiniGameCompleted()
    {
        currentState = GameState.MiniGameCompleted;
        OnMiniGameCompletedEnter?.Invoke();
    }

    private void GoToNextCard()
    {
        // К начальному состоянию
        currentState = GameState.NextCard;
        OnNextCardEnter?.Invoke();
    }

    private void ResetGame()
    {
        // Карточка не нажата
        currentState = GameState.CardNotPressed;
        OnCardNotPressedEnter?.Invoke();
    }

    public void StartMiniGame()
    {
        currentState = GameState.MiniGameStarted;
        OnMiniGameStartedEnter?.Invoke();
    }
}

