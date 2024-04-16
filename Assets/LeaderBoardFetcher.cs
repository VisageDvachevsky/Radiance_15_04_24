using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;

public class LeaderboardFetcher : MonoBehaviour
{
    public string serverURL = "http://localhost:8080/leaderboard";

    public void FetchLeaderboard()
    {
        StartCoroutine(FetchLeaderboardRequest());
    }

    IEnumerator FetchLeaderboardRequest()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(serverURL))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to fetch leaderboard: " + www.error);
            }
            else
            {
                string leaderboardData = www.downloadHandler.text;
                Debug.Log("Leaderboard data: " + leaderboardData);

                string[] leaderboardEntries = leaderboardData.Split('\n');

                List<KeyValuePair<int, int>> leaderboard = new List<KeyValuePair<int, int>>();

                foreach (string entry in leaderboardEntries)
                {
                    string[] parts = entry.Split(':');
                    if (parts.Length == 2)
                    {
                        int place = int.Parse(parts[0]);
                        int score = int.Parse(parts[1]);
                        leaderboard.Add(new KeyValuePair<int, int>(place, score));
                    }
                }

                ProcessLeaderboard(leaderboard);
            }
        }
    }

    private void ProcessLeaderboard(List<KeyValuePair<int, int>> leaderboard)
    {
        // онйю ме опхдслюк йюй бшбеярх врн пейнпд онахр
    }
}
