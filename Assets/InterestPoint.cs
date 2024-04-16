using UnityEngine;

public class InterestPoint : MonoBehaviour
{
    [SerializeField] private QuestionInfo _questionInfo;

    public void Invoke()
    {
        QuestWindow.GetInstance().Open(_questionInfo);
    }
}
