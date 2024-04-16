using UnityEngine;
using UnityEngine.UI;
using System.Collections;
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
    [SerializeField] private TextMeshProUGUI _answerResultText;
    [SerializeField] private float _animationSmoothness = 10f;

    private bool _isOpened = false;
    private QuestionInfo _currentQuestionInfo;

    public StateMachine _stateMachine;

    public event Action OnQuestCompleted;

    [SerializeField] private TextMeshProUGUI _explanationText; 
    [SerializeField] private float _explanationAnimationSpeed = 1f; 
    [SerializeField] private RectTransform _explanationTransform;

    private bool _isExplanationShown = false;

    private void Start()
    {
        if (_instance)
        {
            throw new Exception("More than one QuestWindow instance in scene!");
        }
        _instance = this;

        _windowTransform.localScale = Vector3.zero;

        _explanationTransform.localScale = Vector3.zero;
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
        _stateMachine.StartMiniGame();

        _currentQuestionInfo = questionInfo;

        _header.text = _currentQuestionInfo.Theme;
        _mainText.text = _currentQuestionInfo.QuestionText;

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
        Toggle selectedToggle = _answerOptionsContainer.GetComponent<ToggleGroup>().ActiveToggles().FirstOrDefault();

        if (selectedToggle != null)
        {
            int selectedAnswer = int.Parse(selectedToggle.name.Replace("RadioButton", ""));
            if (_currentQuestionInfo.AnswerIndex.ContainsKey(selectedAnswer) && selectedAnswer == _currentQuestionInfo.CorrectAnswer)
            {
                Debug.Log("Quest completed.");
                OnQuestCompleted?.Invoke();
                _stateMachine.MiniGameCompleted();

                ShowExplanation(); 
            }
        }

        _answerOptionsContainer.GetComponent<ToggleGroup>().SetAllTogglesOff();
    }

    private void ShowExplanation()
    {
        if (!_isExplanationShown)
        {
            _isExplanationShown = true;
            StartCoroutine(AnimateExplanation(true));
        }
    }

    private IEnumerator AnimateExplanation(bool show)
    {
        Vector3 targetScale = show ? Vector3.one : Vector3.zero;
        float elapsedTime = 0f;

        while (elapsedTime < _explanationAnimationSpeed)
        {
            _explanationTransform.localScale = Vector3.Lerp(_explanationTransform.localScale, targetScale, elapsedTime / _explanationAnimationSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _explanationTransform.localScale = targetScale;
    }

    public void Close()
    {
        _isOpened = false;

        foreach (Transform child in _answerOptionsContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
