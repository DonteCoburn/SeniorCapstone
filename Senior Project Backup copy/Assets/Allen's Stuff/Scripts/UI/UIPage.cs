using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPage : MonoBehaviour
{
    [Tooltip("The default UI to have selected when opening this page")]
    public GameObject defaultSelected;

    [Tooltip("Text element for displaying the timer")]
    public Text timerText;

    private float startTime;
    private bool timerActive = false;

    void Start()
    {
        // You can start the timer here or control it via external triggers
    }

    public void StartTimer()
    {
        timerActive = true;
        startTime = Time.time;
    }

    public void StopTimer()
    {
        timerActive = false;
    }

    public void ResetTimer()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (timerActive)
        {
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString("00");
            string seconds = (t % 60).ToString("00");
            timerText.text = minutes + ":" + seconds;
        }
    }

    public void SetSelectedUIToDefault()
    {
        if (GameManager.instance != null && GameManager.instance.uiManager != null && defaultSelected != null)
        {
            GameManager.instance.uiManager.eventSystem.SetSelectedGameObject(null);
            GameManager.instance.uiManager.eventSystem.SetSelectedGameObject(defaultSelected);
        }
    }
}

