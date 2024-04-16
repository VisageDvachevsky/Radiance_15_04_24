using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizToggle : MonoBehaviour
{
    public Toggle toggle;
    public TextMeshProUGUI label;

    public void Init(ToggleGroup group, string text)
    {
        toggle.group = group;
        label.text = text;
    }
}
