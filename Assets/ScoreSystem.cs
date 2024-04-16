using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem instance;

    private int score = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncrementScore(int points)
    {
        score += points;
    }

    public void DecrementScore(int points)
    {
        score -= points;
        if (score < 0)
        {
            score = 0;
        }
    }

    public int GetScore()
    {
        return score;
    }
}
