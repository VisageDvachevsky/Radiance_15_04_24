using TMPro;
using UnityEngine;

public class InterestPoint : MonoBehaviour
{
    [SerializeField] private QuestionInfo _questionInfo;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Color _default;
    [SerializeField] private Color _completed;

    private CompletedQuestions _completedQuestions;

    private void Start()
    {
        _completedQuestions = CompletedQuestions.Instance;
        _completedQuestions.OnQuestionCompleted.AddListener(HandleCompletion);
        _text.color = _default;
    }

    private void OnDestroy()
    {
        _completedQuestions.OnQuestionCompleted.RemoveListener(HandleCompletion);
    }

    public void Invoke()
    {
        QuestWindow.GetInstance().Open(_questionInfo);
    }

    private void HandleCompletion(QuestionInfo info)
    {
        if (info == _questionInfo)
        {
            _text.color = _completed;
        }
    }
}
