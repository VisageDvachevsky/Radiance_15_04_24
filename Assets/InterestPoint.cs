using UnityEngine;

public class InterestPoint : MonoBehaviour
{
    public string ThemeName;

    private QuestWindow _qWindow;

    public void Invoke(QuestionInfo themeData)
    {
        GetQuestWindow().Open(themeData);
    }

    private QuestWindow GetQuestWindow()
    {
        if (_qWindow == null)
        {
            _qWindow = GameObject.FindGameObjectWithTag("InfoWindow").GetComponent<QuestWindow>();
        }

        return _qWindow;
    }
}
