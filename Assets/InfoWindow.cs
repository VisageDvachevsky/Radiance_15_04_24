using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoWindow : MonoBehaviour
{
    private static InfoWindow _instance;

    [SerializeField] private Transform _windowTransform;
    [SerializeField] private TextMeshProUGUI _header;
    [SerializeField] private TextMeshProUGUI _mainText;
    [SerializeField] private float _animationSmoothness = 10f;

    private bool _isOpened = false;

    private void Start()
    {
        if (_instance) throw new System.Exception("More than one InfoWindow on scene!");
        _instance = this;

        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.K)) { Open("grger", "geregrger"); }

        if (!_isOpened)
        {
            _windowTransform.localScale = Vector3.MoveTowards(_windowTransform.localScale, Vector3.zero, Time.deltaTime * _animationSmoothness);
        }
        else
        {
            _windowTransform.localScale = Vector3.MoveTowards(_windowTransform.localScale, Vector3.one, Time.deltaTime * _animationSmoothness);
        }
    }

    public void Close()
    {
        _isOpened = false;
    }
    
    public static void Open(string header, string mainText)
    {
        if (_instance._isOpened)
        {
            return;
        }

        _instance._header.text = header;
        _instance._mainText.text = mainText;
        _instance._isOpened = true;
    }
}
