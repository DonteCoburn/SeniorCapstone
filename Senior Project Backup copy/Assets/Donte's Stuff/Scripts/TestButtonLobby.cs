using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;
using UnityEngine.SceneManagement;

public class TestLobbyUIManager : MonoBehaviourPunCallbacks
{
    public TMP_Text displayText;
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
    }

    private void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        UpdateStartButtonVisibility();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        UpdateStartButtonVisibility();
    }

    private void UpdateStartButtonVisibility()
    {
        startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2);
    }

    public void StartNetworkedCountdownAndLoadScene()
    {
        startButton.interactable = false;
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
            photonView.RPC("LoadSceneForAll", RpcTarget.All, "SingleLevel2 Multiplayer");
        }
    }

    [PunRPC]
    public void LoadSceneForAll(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
}