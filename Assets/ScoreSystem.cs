using UnityEngine;
using UnityEngine.Events;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance;

    private int score = 0;

    public UnityEvent OnScoreChanged;

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
        OnScoreChanged?.Invoke();
    }

    public void DecrementScore(int points)
    {
        score -= points;
        if (score < 0)
        {
            score = 0;
        }
        OnScoreChanged?.Invoke();
    }

    public int GetScore()
    {
        return score;
    }
}
