using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public Text scoreText;
    [SerializeField] private ScoreSender _scoreSender;

    void Start()
    {
        if (ScoreSystem.instance != null)
        {

            int targetScore = ScoreSystem.instance.GetScore();
            Debug.Log("Target Score: " + targetScore);

            AnimateScore(targetScore);

            _scoreSender.SendScore(targetScore);
        }
        else
        {
            Debug.LogError("ScoreSystem instance not found!");
        }
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
