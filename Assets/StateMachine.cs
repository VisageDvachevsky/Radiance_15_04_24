using UnityEngine;
using System;

public class StateMachine : MonoBehaviour
{
    public enum GameState
    {
        CardNotPressed,     // �������� �� ������
        MiniGameStarted,    // �������� ��������
        MiniGameCompleted,  // �������� ���������
        NextCard            // ������� � ���������� ���������
    }

    private GameState currentState;

    public event Action OnCardNotPressedEnter;
    public event Action OnMiniGameStartedEnter;
    public event Action OnMiniGameCompletedEnter;
    public event Action OnNextCardEnter;

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
        // � ���������� ���������
        currentState = GameState.NextCard;
        OnNextCardEnter?.Invoke();
    }

    private void ResetGame()
    {
        // �������� �� ������
        currentState = GameState.CardNotPressed;
        OnCardNotPressedEnter?.Invoke();
    }

    public void StartMiniGame()
    {
        currentState = GameState.MiniGameStarted;
        OnMiniGameStartedEnter?.Invoke();
    }
}

