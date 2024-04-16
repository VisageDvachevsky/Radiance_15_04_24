using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ScoreSender : MonoBehaviour
{
    public string serverURL = "http://localhost:8080/send_score";

    public void SendScore(int score)
    {
        StartCoroutine(SendScoreRequest(score));
    }

    IEnumerator SendScoreRequest(int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("score", score.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to send score: " + www.error);
            }
            else
            {
                Debug.Log("Score sent successfully!");
            }
        }
    }
}
