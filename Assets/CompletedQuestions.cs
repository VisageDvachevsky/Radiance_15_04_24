using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CompletedQuestions : MonoBehaviour
{
    private HashSet<QuestionInfo> _completedQuestions = new HashSet<QuestionInfo>();
    public IEnumerable<QuestionInfo> Completed => _completedQuestions;

    public static CompletedQuestions Instance { get; private set; }

    public class QuestionEvent : UnityEvent<QuestionInfo> { }
    public QuestionEvent OnQuestionCompleted = new QuestionEvent();


    private void Awake()
    {
        if(Instance != null)
        {
            throw new Exception($"More than one instance of {GetType().Name} on scene");
        }
        Instance = this;
    }

    public bool HasCompleted(QuestionInfo info)
    {
        return _completedQuestions.Contains(info);
    }

    public bool Complete(QuestionInfo info, bool notify = true)
    {
        if (_completedQuestions.Contains(info)) return false;

        _completedQuestions.Add(info);

        if (notify)
        {
            OnQuestionCompleted?.Invoke(info);
        }

        return true;
    }
}
