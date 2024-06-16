using UnityEngine;
using UnityEngine.UI; // Use TMPro if using TextMeshPro components
using System;

public class Timer : MonoBehaviour
{
    public Text timerText; // Replace with TMPro.TextMeshProUGUI if using TextMeshPro
    private float startTime;
    private bool timerActive = false;

    // Use this for initialization
    void Start()
    {
        StartTimer(); // Automatically starts the timer
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

    // Update is called once per frame
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
}

