using UnityEngine;

public class InterestPoint : MonoBehaviour
{
    [SerializeField] private QuestWindow questWindow;
    public string ThemeName;

    public void Invoke(QuestionInfo themeData)
    {
        questWindow.Open(themeData);
    }

    private void Update()
    {
        questWindow = GameObject.FindGameObjectWithTag("InfoWindow").GetComponent<QuestWindow>();
    }
}
