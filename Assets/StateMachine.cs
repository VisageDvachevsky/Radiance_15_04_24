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
                // ������ ���������� ������
                // ����� ��������� ���������� ������ ��������� � ���������� ���������
                currentState = CardState.QuestCompleted;
                break;
            case CardState.QuestCompleted:
                Debug.Log("Quest completed.");
                OnQuestCompleted?.Invoke();
                // ������ ���������� ������
                // ��������� � ��������� ��� �������� � ��������� ��������
                currentState = CardState.NextCard;
                break;
            case CardState.NextCard:
                Debug.Log("Moving to next card.");
                OnNextCard?.Invoke();
                // ������ ��� �������� � ��������� ��������
                // ��������� � ���������� ��������� ��� ������ �����
                currentState = CardState.Initial;
                break;
            default:
                break;
        }
    }
}
