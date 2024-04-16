using UnityEngine;

[CreateAssetMenu()]
public class QuestionInfo : ScriptableObject
{
    [field: SerializeField] public QuestionTheme Theme { get; private set; }
    [field: TextArea]
    [field: SerializeField] public string QuestionTitle { get; private set; }
    [field: SerializeField] public string[] Options { get; private set; }
    [field: SerializeField] public int CorrectIndex { get; private set; }
    [field: TextArea]
    [field: SerializeField] public string Explanation { get; private set; }
}
