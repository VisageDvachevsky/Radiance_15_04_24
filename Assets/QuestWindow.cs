using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;
using System.Linq;
using System.Collections.Generic;

public class QuestWindow : MonoBehaviour
{
    private static QuestWindow _instance;

    [SerializeField] private RectTransform _windowTransform;
    [SerializeField] private TextMeshProUGUI _header;
    [SerializeField] private TextMeshProUGUI _mainText;
    [SerializeField] private Transform _answerOptionsContainer;
    [SerializeField] private QuizToggle _togglePrefab;
    [SerializeField] private TextMeshProUGUI _answerResultText;
    [SerializeField] private float _animationSmoothness = 10f;
    [SerializeField] private TextMeshProUGUI _explanationText; 
    [SerializeField] private float _explanationAnimationSpeed = 1f; 
    [SerializeField] private RectTransform _explanationTransform;

    private StateMachine _stateMachine;
    private CompletedQuestions _completedQuestions;
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
        _completedQuestions = CompletedQuestions.Instance;

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
        if (_isOpened) return;

        _stateMachine.StartMiniGame();

        _currentQuestionInfo = questionInfo;

        _header.text = _currentQuestionInfo.Theme.VisibleName;
        _mainText.text = _currentQuestionInfo.QuestionTitle;
        _explanationTransform.localScale = Vector3.zero;
        _isExplanationShown = false;

        List<KeyValuePair<int, string>> options = new List<KeyValuePair<int, string>>();
        for (int i = 0; i < _currentQuestionInfo.Options.Length; i++)
            options.Add(new KeyValuePair<int, string>(i, _currentQuestionInfo.Options[i]));
        options.Shuffle();


        foreach (KeyValuePair<int, string> option in options)
        {
            CreateAnswerOption(option.Key, option.Value);
        }

        _isOpened = true;
    }


    private void CreateAnswerOption(int index, string answerText)
    {
        QuizToggle toggle = Instantiate(_togglePrefab, _answerOptionsContainer.transform);
        toggle.Init(answerText, index);
        toggle.OnClick.AddListener(SubmitAnswer);
    }

    public void SubmitAnswer(QuizToggle toggle)
    {
        int selected = toggle.Index;
        int correct = _currentQuestionInfo.CorrectIndex;

        if (selected == correct)
        {
            OnQuestCompleted?.Invoke();
            
            _stateMachine.MiniGameCompleted();
            _completedQuestions.Complete(_currentQuestionInfo);

            foreach (Transform child in _answerOptionsContainer.transform)
            {
                QuizToggle t = child.GetComponent<QuizToggle>();
                t.DisableInteract();

                if (t == toggle) t.SetOk();
                else t.SetBlank();
            }
        }
        else
        {
            foreach (Transform child in _answerOptionsContainer.transform)
            {
                QuizToggle t = child.GetComponent<QuizToggle>();
                t.DisableInteract();

                if (t == toggle) t.SetWrong();
                else if (t.Index == correct) t.SetNotButOk();
                else t.SetBlank();
            }
        }

        ShowExplanation();
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
            child.GetComponent<QuizToggle>().OnClick.RemoveAllListeners();
            Destroy(child.gameObject);
        }
    }
}
