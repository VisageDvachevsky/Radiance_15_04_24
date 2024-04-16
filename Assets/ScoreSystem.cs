using UnityEngine;
using UnityEngine.Events;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance;

    private int score = 0;

    public class ScoreChangeEvent : UnityEvent<int, int> { }
    public ScoreChangeEvent OnScoreChanged = new ScoreChangeEvent();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

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
        OnScoreChanged?.Invoke(score, points);
    }

    public void DecrementScore(int points)
    {
        int last = score;

        score -= points;
        if (score < 0)
        {
            score = 0;
        }
        OnScoreChanged?.Invoke(score, score - last);
    }

    public int GetScore()
    {
        return score;
    }
}
