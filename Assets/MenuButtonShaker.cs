using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MenuButtonShaker : MonoBehaviour
{
    [SerializeField] private float scale = 1.25f;
    [SerializeField] private float speed = 5f;

    private TaskManager taskManager;

    private Coroutine shakeRoutine;

    private void Start()
    {
        taskManager = TaskManager.Instance;
        taskManager.NewStage.AddListener(HandleComplete);
    }

    private void OnDestroy()
    {
        taskManager.NewStage.RemoveListener(HandleComplete);
    }

    public void HandleClick()
    {
        if (shakeRoutine != null )
        {
            StopCoroutine(shakeRoutine);
            shakeRoutine = null;
        }
        transform.localScale = Vector3.one;
    }

    private void HandleComplete()
    {
        if (shakeRoutine == null)
        {
            shakeRoutine = StartCoroutine(AnimateShake());
        }
    }

    private IEnumerator AnimateShake()
    {
        float time = 0f;
        while (true)
        {
            transform.localScale = Vector3.one * (1f + (Mathf.Sin(time*speed) +1f)*scale);

            time += Time.deltaTime;
            yield return null;
        }
    }
}
