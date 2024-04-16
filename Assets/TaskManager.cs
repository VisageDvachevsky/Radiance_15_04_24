using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public Button[] taskButtons;
    public Image spriteRenderer;
    public Sprite[] sprites;
    public ScoreSystem scoreSystem;

    private CompletedQuestions _completedQuestions;
    private int currentTaskIndex = 0;

    private void Start()
    {
        _completedQuestions = CompletedQuestions.Instance;

        for (int i = 1; i < taskButtons.Length; i++)
        {
            taskButtons[i].interactable = false;
        }
        UpdateImage();

        _completedQuestions.OnQuestionCompleted.AddListener(CompleteTask);
    }

    private void OnDestroy()
    {
        _completedQuestions.OnQuestionCompleted.RemoveListener(CompleteTask);
    }

    private void CompleteTask(QuestionInfo _)
    {
        currentTaskIndex++;
        if (currentTaskIndex < taskButtons.Length)
        {
            taskButtons[currentTaskIndex].interactable = true;
            UpdateImage();
            scoreSystem.IncrementScore(10);
        }
    }

    private void UpdateImage()
    {
        spriteRenderer.sprite = sprites[currentTaskIndex + 1];
    }
}
