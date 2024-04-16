using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreCounter : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    [SerializeField] private ScoreSender _scoreSender;

    private ScoreSystem _scoreSystem;

    void Start()
    {
        _scoreSystem = ScoreSystem.Instance;
        _scoreSystem.OnScoreChanged.AddListener(HandleScoreChange);
        HandleScoreChange();

        //if (ScoreSystem.Instance != null)
        //{

        //    int targetScore = ScoreSystem.Instance.GetScore();
        //    Debug.Log("Target Score: " + targetScore);

        //    AnimateScore(targetScore);

        //    //_scoreSender.SendScore(targetScore);
        //}
        //else
        //{
        //    Debug.LogError("ScoreSystem instance not found!");
        //}
    }

    private void OnDestroy()
    {
        _scoreSystem.OnScoreChanged.RemoveListener(HandleScoreChange);
    }

    private void HandleScoreChange()
    {
        scoreText.text = $"Ñ÷¸ò: {_scoreSystem.GetScore()}";
    }

    void AnimateScore(int targetScore)
    {
        StartCoroutine(AnimateScoreCoroutine(targetScore));
    }

    IEnumerator AnimateScoreCoroutine(int targetScore)
    {
        int currentScore = 0; 
        int incrementAmount = 1;

        while (currentScore < targetScore)
        {
            currentScore += incrementAmount;

            scoreText.text = currentScore.ToString();

            yield return null;
        }

        scoreText.text = targetScore.ToString();
    }
}
