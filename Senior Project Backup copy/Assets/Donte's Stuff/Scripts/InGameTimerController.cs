using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.SceneManagement;  // Add this for managing scenes

public class MultiplayerTimerScript : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI timerText;
    private bool timerActive = false;
    private float startTime;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;  // Register to listen to the scene loaded event

        object startTimeFromProperties;
        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("StartTime", out startTimeFromProperties))
        {
            startTime = (float)startTimeFromProperties;
            timerActive = true;
        }
        else if (PhotonNetwork.IsMasterClient)
        {
            StartTimer();
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  // Unregister the listener
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetTimer();  // Reset the timer when a new scene is loaded
    }

    public void StartTimer()
    {
        startTime = (float)PhotonNetwork.Time;
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "StartTime", startTime } });
        timerActive = true;
    }

    public void ResetTimer()
    {
        startTime = (float)PhotonNetwork.Time;
        timerActive = false;  // Stop the timer until explicitly started again
        UpdateRoomProperties();  // Update the room properties to reflect the reset
    }

    void UpdateRoomProperties()
    {
        string concatenatedTimes = string.Join(",", new string[] { }); // Clear any previous times
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "finishTimes", concatenatedTimes }, { "StartTime", startTime } });
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("StartTime"))
        {
            startTime = (float)propertiesThatChanged["StartTime"];
            timerActive = true;
        }
    }

    void Update()
    {
        if (timerActive)
        {
            float t = (float)PhotonNetwork.Time - startTime;
            string minutes = ((int)t / 60).ToString("00");
            string seconds = (t % 60).ToString("00");
            string milliseconds = ((int)(t * 1000) % 1000).ToString("000");
            timerText.text = minutes + ":" + seconds + ":" + milliseconds;
        }
    }
}



/*
using UnityEngine;
using TMPro;
using Photon.Pun;

public class MultiplayerTimerScript : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI timerText;
    private bool timerActive = false;
    private float startTime;

    void Start()
    {
        object startTimeFromProperties;
        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("StartTime", out startTimeFromProperties))
        {
            startTime = (float)startTimeFromProperties;
            timerActive = true;
        }
        else if (PhotonNetwork.IsMasterClient)
        {
            StartTimer();
        }
    }

    public void StartTimer()
    {
        startTime = (float)PhotonNetwork.Time;
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "StartTime", startTime } });
        timerActive = true;
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("StartTime"))
        {
            startTime = (float)propertiesThatChanged["StartTime"];
            timerActive = true;
        }
    }

    void Update()
    {
        if (timerActive)
        {
            float t = (float)PhotonNetwork.Time - startTime;
            string minutes = ((int)t / 60).ToString("00");
            string seconds = (t % 60).ToString("00");
            string milliseconds = ((int)(t * 1000) % 1000).ToString("000");
            timerText.text = minutes + ":" + seconds + ":" + milliseconds;
        }
    }
}
*/