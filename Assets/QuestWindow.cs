using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class QuestWindow : MonoBehaviour
{
    private static QuestWindow _instance;

    [SerializeField] private RectTransform _windowTransform;
    [SerializeField] private TextMeshProUGUI _header;
    [SerializeField] private TextMeshProUGUI _mainText;
    [SerializeField] private InputField _answerInput;
    [SerializeField] private Button _submitButton;
    [SerializeField] private TextMeshProUGUI _answerResultText;
    [SerializeField] private float _animationSmoothness = 10f;

    private bool _isOpened = false;
    private QuestionInfo _currentQuestionInfo; // Обновленное поле для хранения информации о вопросе

    public event Action OnQuestCompleted;

    private void Start()
    {
        if (_instance)
        {
            throw new Exception("More than one QuestWindow instance in scene!");
        }
        _instance = this;

        _windowTransform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (!_isOpened)
        {
            _windowTransform.localScale = Vector3.MoveTowards(_windowTransform.localScale, Vector3.zero, Time.deltaTime * _animationSmoothness);
        }
        else
        {
            _windowTransform.localScale = Vector3.MoveTowards(_windowTransform.localScale, Vector3.one, Time.deltaTime * _animationSmoothness);
        }
    }

    // Метод Open принимает объект QuestionInfo вместо ThemeData
    public void Open(QuestionInfo questionInfo)
    {
        _currentQuestionInfo = questionInfo;

        _header.text = _currentQuestionInfo.Theme;
        _mainText.text = _currentQuestionInfo.QuestionText;

        _isOpened = true;
    }

    public void SubmitAnswer()
    {
        int selectedAnswer;
        if (int.TryParse(_answerInput.text, out selectedAnswer))
        {
            if (_currentQuestionInfo.AnswerIndex.ContainsKey(selectedAnswer) && selectedAnswer == _currentQuestionInfo.CorrectAnswer)
            {
                Debug.Log("Quest completed.");
                OnQuestCompleted?.Invoke();
            }
        }

        _answerInput.text = "";
    }

    public void Close()
    {
        _isOpened = false;
    }
}
