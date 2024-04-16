//using UnityEngine;

//public class StateMachine : MonoBehaviour
//{
//    private struct State
//    {
//        public bool state1;
//        public bool state2;
//        public bool state3;
//        public bool state4;
//    }

//    private State currentState;

//    private void Start()
//    {
//        currentState.state1 = false;
//        currentState.state2 = false;
//        currentState.state3 = false;
//        currentState.state4 = false;
//    }

//    public void ToggleState1()
//    {
//        currentState.state1 = !currentState.state1;
//        Debug.Log("State 1 new state: " + currentState.state1);
//    }

//    public void ToggleState2()
//    {
//        currentState.state2 = !currentState.state2;
//        Debug.Log("State 2 new state: " + currentState.state2);
//    }

//    public void ToggleState3()
//    {
//        currentState.state3 = !currentState.state3;
//        Debug.Log("State 3 new state: " + currentState.state3);
//    }

//    public void ToggleState4()
//    {
//        currentState.state4 = !currentState.state4;
//        Debug.Log("State 4 new state: " + currentState.state4);
//    }
//}




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
        if (currentState != GameState.MiniGameCompleted)
        {
            currentState = GameState.MiniGameCompleted;
            OnMiniGameCompletedEnter?.Invoke();
        }
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
