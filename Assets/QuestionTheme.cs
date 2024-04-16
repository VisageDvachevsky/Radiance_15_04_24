using UnityEngine;

[CreateAssetMenu()]
public class QuestionTheme : ScriptableObject
{
    [field: SerializeField] public string VisibleName { get; private set; }
}
