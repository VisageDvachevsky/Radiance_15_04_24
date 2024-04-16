using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonShaker : MonoBehaviour
{
    [SerializeField] private float scale = 1.25f;
    [SerializeField] private float speed = 5f;

    private CompletedQuestions completedQuestions;

    private Coroutine shakeRoutine;

    private void Start()
    {
        completedQuestions = CompletedQuestions.Instance;
        completedQuestions.OnQuestionCompleted.AddListener(HandleComplete);
    }

    private void OnDestroy()
    {
        completedQuestions.OnQuestionCompleted.RemoveListener(HandleComplete);
    }

    public void HandleClick()
    {
        if (shakeRoutine != null )
        {
            StopCoroutine(shakeRoutine);
        }
        transform.localScale = Vector3.one;
    }

    private void HandleComplete(QuestionInfo _)
    {
        if (shakeRoutine == null)
        {
            shakeRoutine = StartCoroutine(AnimateShake());
        }
    }

    private IEnumerator AnimateShake()
    {
        float time = 0f;
        while (true)
        {
            transform.localScale = Vector3.one * (1f + (Mathf.Sin(time*speed) +1f)*scale);

            time += Time.deltaTime;
            yield return null;
        }
    }
}
