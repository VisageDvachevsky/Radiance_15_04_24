using UnityEngine;
using UnityEngine.Events;

public class StateMachine : MonoBehaviour
{
    public enum GameState
    {
        CardNotPressed,         // Карточка не нажата
        CardPressed,            // Карточка нажата
        MiniGameInProgress,     // Миниигра в процессе выполнения
        MiniGameCompleted,      // Миниигра успешно завершена
        NextCard                // Переход к следующей карточке
    }

    private GameState currentState;

    public UnityEvent OnMiniGameStart;
    public UnityEvent OnMiniGameCompleted; 

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
            case GameState.CardPressed:
                CheckMiniGameStart();
                break;
            case GameState.MiniGameInProgress:
                CheckMiniGameCompletion();
                break;
            case GameState.MiniGameCompleted:
                CheckNextCard();
                break;
            case GameState.NextCard:
                GoToNextCard();
                break;
        }
    }

    private void CheckCardPress()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentState = GameState.CardPressed;
        }
    }

    private void CheckMiniGameStart()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnMiniGameStart.Invoke();
            currentState = GameState.MiniGameInProgress;
        }
    }

    private void CheckMiniGameCompletion()
    {
        if (/* Условие завершения миниигры */true)
        {
            OnMiniGameCompleted.Invoke();
            currentState = GameState.MiniGameCompleted;
        }
    }

    private void CheckNextCard()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GoToNextCard();
        }
    }

    private void GoToNextCard()
    {
        currentState = GameState.CardNotPressed;
    }
}
