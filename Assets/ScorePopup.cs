using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

public class ScorePopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _showAnimDuration = 0.25f;
    [SerializeField] private float _hideAnimDuration = 1f;
    [SerializeField] private float _visibleDuration = 1f;
    [SerializeField] private Color _positiveColor;
    [SerializeField] private Color _negativeColor;
    private ScoreSystem _scoreSystem;
    private Coroutine _animation;

    private void Start()
    {
        _scoreSystem = ScoreSystem.Instance;
        _scoreSystem.OnScoreChanged.AddListener(HandleScoreChange);
    }

    private void OnDestroy()
    {
        _scoreSystem.OnScoreChanged.RemoveListener(HandleScoreChange);
    }

    private void HandleScoreChange(int newScore, int delta)
    {
        if (delta == 0) return;

        if (_animation != null)
        {
            StopCoroutine(_animation);
        }

        if (delta > 0)
        {
            _text.text = $"+{delta} очков";
            _text.color = _positiveColor;
        }
        else if (delta < 0)
        {
            _text.text = $"{delta} очков";
            _text.color = _negativeColor;
        }

        StartCoroutine(AnimatePopup());
    }

    private IEnumerator AnimatePopup()
    {
        _text.transform.localScale = Vector3.zero;
        _text.alpha = 0f;
        yield return AnimateExplanation(true, _showAnimDuration);
        yield return new WaitForSeconds(_visibleDuration);
        yield return AnimateExplanation(false, _hideAnimDuration);
    }

    private IEnumerator AnimateExplanation(bool show, float duration)
    {
        Transform textTransform = _text.transform;

        Vector3 targetScale = show ? Vector3.one : Vector3.zero;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            textTransform.localScale = Vector3.Lerp(textTransform.localScale, targetScale, elapsedTime / duration);
            if (show)
            {
                _text.alpha = Mathf.Lerp(0f,1f, elapsedTime / duration);
            }
            else
            {
                _text.alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textTransform.localScale = targetScale;
        _text.alpha = show ? 1f : 0f;
    }
}
