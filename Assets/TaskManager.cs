using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    [System.Serializable]
    public struct Stage
    {
        public QuestionInfo[] Questions;
    }

    public Stage[] Stages;


    public Button[] taskButtons;
    public Image spriteRenderer;
    public Sprite[] sprites;
    public ScoreSystem scoreSystem;

    public UnityEvent NewStage;

    private CompletedQuestions _completedQuestions;
    private int currentTaskIndex = 0;

    public static TaskManager Instance { get; private set; }

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

    private bool CheckTasks()
    {
        if (currentTaskIndex >= Stages.Length) return false;

        for (int i = 0; i < Stages[currentTaskIndex].Questions.Length; i++)
        {
            if (!_completedQuestions.HasCompleted(Stages[currentTaskIndex].Questions[i]))
            {
                return false;
            }
        }

        return true;
    }

    private void CompleteTask(QuestionInfo _)
    {
        scoreSystem.IncrementScore(25);

        if (!CheckTasks()) return;

        currentTaskIndex++;
        if (currentTaskIndex < taskButtons.Length)
        {
            taskButtons[currentTaskIndex].interactable = true;
            UpdateImage();
            NewStage?.Invoke();
        }
    }

    private void UpdateImage()
    {
        spriteRenderer.sprite = sprites[currentTaskIndex + 1];
    }
}
