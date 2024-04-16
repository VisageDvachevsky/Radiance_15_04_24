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
    [SerializeField] private ToggleGroup _answerOptionsContainer;
    [SerializeField] private QuizToggle _togglePrefab;
    [SerializeField] private TextMeshProUGUI _answerResultText;
    [SerializeField] private float _animationSmoothness = 10f;
    [SerializeField] private TextMeshProUGUI _explanationText; 
    [SerializeField] private float _explanationAnimationSpeed = 1f; 
    [SerializeField] private RectTransform _explanationTransform;

    private StateMachine _stateMachine;
    private bool _isOpened = false;
    private QuestionInfo _currentQuestionInfo;


    public event Action OnQuestCompleted;


    private bool _isExplanationShown = false;

    private void Awake()
    {
        if (_instance)
        {
            throw new Exception("More than one QuestWindow instance in scene!");
        }
        _instance = this;
    }

    private void Start()
    {
        _stateMachine = StateMachine.Instance;

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

    public static QuestWindow GetInstance()
    {
        return _instance;
    }

    public void Open(QuestionInfo questionInfo)
    {
        _stateMachine.StartMiniGame();

        _currentQuestionInfo = questionInfo;

        _header.text = _currentQuestionInfo.Theme.VisibleName;
        _mainText.text = _currentQuestionInfo.QuestionTitle;

        for (int i = 0; i < _currentQuestionInfo.Options.Length; i++)
        {
            CreateAnswerOption(i, _currentQuestionInfo.Options[i]);
        }
        _answerOptionsContainer.SetAllTogglesOff();

        _isOpened = true;
    }


    private void CreateAnswerOption(int index, string answerText)
    {
        QuizToggle toggle = Instantiate(_togglePrefab, _answerOptionsContainer.transform);
        toggle.name = "RadioButton" + index;
        toggle.Init(_answerOptionsContainer, answerText);
    }

    public void SubmitAnswer()
    {
        Toggle selectedToggle = _answerOptionsContainer.GetComponent<ToggleGroup>().ActiveToggles().FirstOrDefault();

        if (selectedToggle != null)
        {
            int selectedAnswer = int.Parse(selectedToggle.name.Replace("RadioButton", ""));
            if (_currentQuestionInfo.CorrectIndex == selectedAnswer)
            {
                Debug.Log("Quest completed.");
                //OnQuestCompleted?.Invoke();
                //_stateMachine.MiniGameCompleted();

                ShowExplanation(); 
            }
        }

        _answerOptionsContainer.SetAllTogglesOff();
    }

    private void ShowExplanation()
    {
        if (!_isExplanationShown)
        {
            _isExplanationShown = true;
            _explanationText.text = _currentQuestionInfo.Explanation;

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

        foreach (Transform child in _answerOptionsContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
