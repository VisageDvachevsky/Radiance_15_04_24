using UnityEngine;
using UnityEngine.Events;

public class StateMachine : MonoBehaviour
{
    public enum CardState
    {
        Initial,
        QuestInProgress,
        QuestCompleted,
        NextCard
    }

    public UnityEvent OnCardClicked;
    public UnityEvent OnQuestInProgress;
    public UnityEvent OnQuestCompleted;
    public UnityEvent OnNextCard;

    private CardState currentState;

    private void Start()
    {
        currentState = CardState.Initial;
    }

    public void HandleCardClick()
    {
        switch (currentState)
        {
            case CardState.Initial:
                Debug.Log("Card clicked.");
                OnCardClicked?.Invoke();
                currentState = CardState.QuestInProgress;
                break;
            case CardState.QuestInProgress:
                Debug.Log("Quest in progress.");
                OnQuestInProgress?.Invoke();
                // Ћогика выполнени€ квеста
                // ѕосле успешного выполнени€ квеста переходим к следующему состо€нию
                currentState = CardState.QuestCompleted;
                break;
            case CardState.QuestCompleted:
                Debug.Log("Quest completed.");
                OnQuestCompleted?.Invoke();
                // Ћогика завершени€ квеста
                // ѕереходим к состо€нию дл€ перехода к следующей карточке
                currentState = CardState.NextCard;
                break;
            case CardState.NextCard:
                Debug.Log("Moving to next card.");
                OnNextCard?.Invoke();
                // Ћогика дл€ перехода к следующей карточке
                // ѕереходим к начальному состо€нию дл€ нового цикла
                currentState = CardState.Initial;
                break;
            default:
                break;
        }
    }
}
