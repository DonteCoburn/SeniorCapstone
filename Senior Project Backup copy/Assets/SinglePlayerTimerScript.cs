using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// This is like a simplified version of the multiplayer timer, but with less functionalities since they arne't needed
/// </summary>

public class SinglePlayerTimerScript : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private bool timerActive = false;
    private float startTime;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        StartTimer();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetTimer();
    }

    public void StartTimer()
    {
        startTime = Time.time;
        timerActive = true;
    }

    public void ResetTimer()
    {
        startTime = Time.time;
        timerActive = false;
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
