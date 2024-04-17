using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuizToggle : MonoBehaviour
{
    public Sprite blank;
    public Sprite ok;
    public Sprite notButOk;
    public Sprite wrong;

    public Button button;
    public Image imgRenderer;
    public TextMeshProUGUI label;
    public GameObject imgLabelContainer;
    public Image imgLabel;
    public int textHeight;
    public int imageHeight;

    public class ToggleClickHandler : UnityEvent<QuizToggle> { }
    public ToggleClickHandler OnClick = new ToggleClickHandler();

    public int Index { get; private set; }

    private void Awake()
    {
        button.onClick.AddListener(HandleClick);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(HandleClick);
    }

    public void Init(string text, int index)
    {
        Index = index;
        label.text = text;
        label.gameObject.SetActive(true);
        imgLabelContainer.SetActive(false);
        RectTransform rt = transform.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, textHeight);
    }

    public void Init(Sprite sprite, int index)
    {
        Index = index;
        imgLabel.sprite = sprite;
        label.gameObject.SetActive(false);
        imgLabelContainer.SetActive(true);
        RectTransform rt = transform.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, imageHeight);
    }

    public void SetOk()
    {
        imgRenderer.sprite = ok;
    }

    public void SetWrong()
    {
        imgRenderer.sprite = wrong;
    }

    public void SetNotButOk()
    {
        imgRenderer.sprite = notButOk;
    }

    public void SetBlank()
    {
        imgRenderer.sprite = blank;
    }

    public void DisableInteract()
    {
        button.interactable = false;
    }

    private void HandleClick()
    {
        OnClick?.Invoke(this);
    }
}
