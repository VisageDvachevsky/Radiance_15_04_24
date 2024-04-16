using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public StateMachine stateMachine;
    public Button[] taskButtons;
    public Image spriteRenderer;
    public Sprite[] sprites;
    public ScoreSystem scoreSystem;

    private int currentTaskIndex = 0;

    private void Start()
    {
        for (int i = 1; i < taskButtons.Length; i++)
        {
            taskButtons[i].interactable = false;
        }
        UpdateImage();

        stateMachine.OnMiniGameCompletedEnter += CompleteTask;
    }


    private void CompleteTask()
    {
        stateMachine.MiniGameCompleted();

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
