using UnityEngine;

public class InterestPoint : MonoBehaviour
{
    [TextArea]
    [SerializeField] private string _headerText;
    [TextArea]
    [SerializeField] private string _mainText;

    public void Invoke()
    {
        InfoWindow.Open(_headerText, _mainText);
    }
}
