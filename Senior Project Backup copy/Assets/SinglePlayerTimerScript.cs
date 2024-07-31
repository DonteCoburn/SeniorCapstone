using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SinglePlayerTimerScript : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private bool timerActive = false;
    private float startTime;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Register to listen to the scene loaded event
        StartTimer();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unregister the listener
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetTimer(); // Reset the timer when a new scene is loaded
    }

    public void StartTimer()
    {
        startTime = Time.time;
        timerActive = true;
    }

    public void ResetTimer()
    {
        startTime = Time.time;
        timerActive = false; // Stop the timer until explicitly started again
    }

    void Update()
    {
        if (timerActive)
        {
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString("00");
            string seconds = (t % 60).ToString("00");
            string milliseconds = ((int)(t * 1000) % 1000).ToString("000");
            timerText.text = minutes + ":" + seconds + ":" + milliseconds;
        }
    }
}
