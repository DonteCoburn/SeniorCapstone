using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;
using UnityEngine.SceneManagement;

public class TestLobbyUIManager : MonoBehaviourPunCallbacks
{
    public TMP_Text displayText;
    public TMP_Text waitingText;
    public TMP_Dropdown levelDropdown;
    public Button startButton;
    public int startCount = 3;
    public MultiplayerTimerScript timerScript;

    private void Start()
    {
        displayText.gameObject.SetActive(false);
        timerScript = FindObjectOfType<MultiplayerTimerScript>();
        if (timerScript == null)
        {
            Debug.LogError("MultiplayerTimerScript component not found on any GameObject in the scene.");
        }
        PhotonNetwork.AddCallbackTarget(this);
        UpdateStartButtonVisibility();
        photonView.RPC("UpdatePreGameText", RpcTarget.All);
    }

    [PunRPC]
    private void UpdateGameStartedText()
    {
        Debug.Log("Game started text updated");
        if (PhotonNetwork.IsMasterClient)
        {
            waitingText.text = "";
        }
        else
        {
            waitingText.text = "Game is starting";
        }
    }

    [PunRPC]
    private void UpdatePreGameText()
    {
        Debug.Log("Pre-game text updated");
        if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            waitingText.text = "Waiting for more players";
        }
        else
        {
            if (PhotonNetwork.IsMasterClient)
            {
                waitingText.text = "Game ready to start";
            }
            else
            {
                waitingText.text = "Waiting for host to start game";
            }
        }
    }


    private void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        UpdateStartButtonVisibility();
        photonView.RPC("UpdatePreGameText", RpcTarget.All);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        UpdateStartButtonVisibility();
        photonView.RPC("UpdatePreGameText", RpcTarget.All);
    }

    private void UpdateStartButtonVisibility()
    {
        startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2);
        levelDropdown.gameObject.SetActive(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2);
    }

    //The start button has an onClick where it will start this
    public void StartNetworkedCountdownAndLoadScene()
    {
        photonView.RPC("UpdateGameStartedText", RpcTarget.All);
        startButton.interactable = false;
        levelDropdown.interactable = false;
        photonView.RPC("StartCountdown", RpcTarget.All);
    }

    [PunRPC]
    public void StartCountdown()
    {
        displayText.gameObject.SetActive(true);
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        int count = startCount;
        while (count > 0)
        {
            displayText.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        displayText.text = "Go!";
        yield return new WaitForSeconds(1);

        if (PhotonNetwork.IsMasterClient)
        {
            timerScript.ResetTimer();
            // Picks level based on the host's level choice
            switch (levelDropdown.value)
            {
                case 0:
                    photonView.RPC("LoadSceneForAll", RpcTarget.All, "SingleLevel1 Multiplayer");
                    break;
                case 1:
                    photonView.RPC("LoadSceneForAll", RpcTarget.All, "SingleLevel2 Multiplayer");
                    break;
                case 2:
                    photonView.RPC("LoadSceneForAll", RpcTarget.All, "SingleLevel3 Multiplayer");
                    break;
                default:
                    Debug.Log("Invalid level selection");
                    break;
            }
        }
    }

    [PunRPC]
    public void LoadSceneForAll(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }

}