using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InterestPointInteractor : MonoBehaviour
{
    QuestionData questionData;

    private void Start()
    {
        questionData = new QuestionData
        {
            Theme = "Тема вопроса 1",
            Questions = new List<QuestionData.Question>
            {
                new QuestionData.Question
                {
                    QuestionText = "Текст вопроса 1?",
                    AnswerIndex = new Dictionary<int, string>
                    {
                        { 1, "Ответ 1" },
                        { 2, "Ответ 2" },
                        { 3, "Ответ 3" }
                    },
                    CorrectAnswer = 1
                },
                new QuestionData.Question
                {
                    QuestionText = "Текст вопроса 2?",
                    AnswerIndex = new Dictionary<int, string>
                    {
                        { 1, "Ответ 1" },
                        { 2, "Ответ 2" },
                        { 3, "Ответ 3" }
                    },
                    CorrectAnswer = 2
                }
            }
        };
    }

    private void Update()
    {
        RuntimePlatform platform = Application.platform;
        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        CheckTouch(Input.GetTouch(0).position);
                    }
                }
            }
        }
        else if (platform == RuntimePlatform.WindowsEditor || platform == RuntimePlatform.OSXEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CheckTouch(Input.mousePosition);
            }
        }
    }

    private void CheckTouch(Vector3 pos)
    {
        RaycastHit hit;
        Ray ray = GetComponent<Camera>().ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out hit))
        {
            var _InterestPoint = hit.transform.GetComponent<InterestPoint>();

            if (_InterestPoint != null)
            {
                QuestionInfo questionInfo = questionData.GetQuestionInfoByTheme(_InterestPoint.ThemeName);
                _InterestPoint.Invoke(questionInfo);
            }
        }
    }
}
