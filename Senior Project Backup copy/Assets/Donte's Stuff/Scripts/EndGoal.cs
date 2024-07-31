using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // Needed to handle scene changes

public class EndGoal : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI resultText;
    public Button finishButton;
    public GameObject background;
    private List<string> finishTimes = new List<string>();
    private const int maxTimes = 10;
    private Dictionary<int, int> playerNumbers = new Dictionary<int, int>();
    private List<Color> playerColors = new List<Color>
    {
        HexToColor("#FF8000"), // Player 1: Orange
        HexToColor("#0000FF"), // Player 2: Blue
        HexToColor("#008000"), // Player 3: Green
        HexToColor("#FF0000"), // Player 4: Red
        HexToColor("#800080"), // Player 5: Purple
        HexToColor("#FFFF00"), // Player 6: Yellow
        HexToColor("#00FFFF"), // Player 7: Cyan
        HexToColor("#FF00FF"), // Player 8: Magenta
        HexToColor("#00FF00"), // Player 9: Lime
        HexToColor("#FFC0CB")  // Player 10: Pink
    };

    void Start()
    {
        if (finishButton != null)
        {
            finishButton.onClick.AddListener(SendFinishTime);
        }
        if (background != null)
        {
            background.SetActive(false);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (PhotonNetwork.IsConnected)
        {
            AssignPlayerNumbers();
            //UpdateUIForNewPlayer();
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        finishTimes.Clear();
        UpdateResultText();
        background.SetActive(false);
    }

    public void SendFinishTime()
    {
        if (finishTimes.Count < maxTimes)
        {
            int playerNumber = playerNumbers[PhotonNetwork.LocalPlayer.ActorNumber];
            string timeRecord = "P" + playerNumber + ": " + timerText.text;
            Debug.Log("Sending time: " + timeRecord); // Log the time being sent
            photonView.RPC("RecordFinishTime", RpcTarget.All, timeRecord);
        }
    }


    [PunRPC]
    void RecordFinishTime(string time)
    {
        Debug.Log("Received time: " + time); // Log the received time
        if (!string.IsNullOrEmpty(time))
        {
            finishTimes.Add(time);
            UpdateResultText();
            if (finishTimes.Count > 0)
            {
                background.SetActive(true);
            }
            UpdateRoomProperties();
        }
        else
        {
            Debug.LogError("Received empty time string");
        }
    }


    void UpdateRoomProperties()
    {
        string concatenatedTimes = string.Join(",", finishTimes);
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "finishTimes", concatenatedTimes } });
    }

    void UpdateResultText()
    {
        resultText.text = "";
        foreach (string finishTime in finishTimes)
        {
            if (finishTime.Length > 1)
            {
                int playerNumber = int.Parse(finishTime.Substring(1, 1)) - 1;
                Color playerColor = playerColors[playerNumber];
                resultText.text += $"<color=#{ColorUtility.ToHtmlStringRGB(playerColor)}>{finishTime.Substring(0, 2)}</color><color=#FFFFFF>{finishTime.Substring(2)}</color>\n";
            }
            else
            {
                // Handle the case where finishTime is not as expected
                Debug.LogError("Unexpected finishTime format: " + finishTime);
            }
        }
    }


    void UpdateUIForNewPlayer()
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("finishTimes", out object times))
        {
            string concatenatedTimes = times as string;
            if (!string.IsNullOrEmpty(concatenatedTimes))
            {
                finishTimes = new List<string>(concatenatedTimes.Split(','));
                UpdateResultText();
            }
            else
            {
                Debug.LogError("Finish times string is empty on room properties update");
            }

            if (finishTimes.Count > 0)
            {
                background.SetActive(true);
            }
        }
    }


    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        AssignPlayerNumbers();
        if (PhotonNetwork.IsMasterClient)
        {
            UpdateRoomProperties();
        }
    }

    void AssignPlayerNumbers()
    {
        List<int> actorNumbers = new List<int>();
        foreach (var player in PhotonNetwork.PlayerList)
        {
            actorNumbers.Add(player.ActorNumber);
        }
        actorNumbers.Sort();
        for (int i = 0; i < actorNumbers.Count; i++)
        {
            playerNumbers[actorNumbers[i]] = i + 1; // P1, P2, P3, etc.
        }
    }

    static Color HexToColor(string hex)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hex, out color))
        {
            return color;
        }
        return Color.white; // default color in case of error
    }
}

