using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class QuestWindow : MonoBehaviour
{
    private static QuestWindow _instance;

    [SerializeField] private RectTransform _windowTransform;
    [SerializeField] private TextMeshProUGUI _header;
    [SerializeField] private TextMeshProUGUI _mainText;
    [SerializeField] private Transform _answerOptionsContainer;
    [SerializeField] private Button _submitButton;
    [SerializeField] private TextMeshProUGUI _answerResultText;
    [SerializeField] private float _animationSmoothness = 10f;

    private bool _isOpened = false;
    private QuestionInfo _currentQuestionInfo; 

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

    public void Open(QuestionInfo questionInfo)
    {
        _currentQuestionInfo = questionInfo;

        _header.text = _currentQuestionInfo.Theme;
        _mainText.text = _currentQuestionInfo.QuestionText;

        // Создаем радиокнопки для каждого варианта ответа
        foreach (var option in _currentQuestionInfo.AnswerIndex)
        {
            CreateAnswerOption(option.Key, option.Value);
        }

        _isOpened = true;
    }

    private void CreateAnswerOption(int index, string answerText)
    {
        GameObject radioButtonGO = new GameObject("RadioButton" + index);
        radioButtonGO.transform.SetParent(_answerOptionsContainer);
        Toggle toggle = radioButtonGO.AddComponent<Toggle>();

        TextMeshProUGUI optionText = radioButtonGO.AddComponent<TextMeshProUGUI>();
        optionText.text = answerText;
        optionText.fontSize = 14;
        optionText.alignment = TextAlignmentOptions.Left;
        optionText.rectTransform.sizeDelta = new Vector2(300, 30);

        toggle.group = _answerOptionsContainer.GetComponent<ToggleGroup>();
    }

    public void SubmitAnswer()
    {
        // Находим выбранную радиокнопку
        Toggle selectedToggle = _answerOptionsContainer.GetComponent<ToggleGroup>().ActiveToggles().FirstOrDefault();

        if (selectedToggle != null)
        {
            int selectedAnswer = int.Parse(selectedToggle.name.Replace("RadioButton", ""));
            if (_currentQuestionInfo.AnswerIndex.ContainsKey(selectedAnswer) && selectedAnswer == _currentQuestionInfo.CorrectAnswer)
            {
                Debug.Log("Quest completed.");
                OnQuestCompleted?.Invoke();
            }
        }

        // Сбрасываем выбор радиокнопок
        _answerOptionsContainer.GetComponent<ToggleGroup>().SetAllTogglesOff();
    }

    public void Close()
    {
        _isOpened = false;

        // Удаляем все радиокнопки
        foreach (Transform child in _answerOptionsContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
