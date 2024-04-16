using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public StateMachine stateMachine;
    public Button[] taskButtons;
    public ScoreSystem scoreSystem;

    private int currentTaskIndex = 0;

    private void Start()
    {
        for (int i = 1; i < taskButtons.Length; i++)
        {
            taskButtons[i].interactable = false;
        }

        stateMachine.OnMiniGameCompletedEnter += CompleteTask;
    }


    private void CompleteTask()
    {
        stateMachine.MiniGameCompleted();

        currentTaskIndex++;
        if (currentTaskIndex < taskButtons.Length)
        {
            taskButtons[currentTaskIndex].interactable = true;
            scoreSystem.IncrementScore(10);
        }
    }
}
