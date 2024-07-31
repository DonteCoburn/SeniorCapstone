using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;
using UnityEngine.SceneManagement;

public class TestLobbyUIManager : MonoBehaviourPun
{
    public TMP_Text displayText;
    public Button startButton;
    public int startCount = 3;
    public MultiplayerTimerScript timerScript;  // Reference to the MultiplayerTimerScript

    private void Start()
    {
        displayText.gameObject.SetActive(false);
        // Get reference to the MultiplayerTimerScript
        timerScript = FindObjectOfType<MultiplayerTimerScript>();
        if (timerScript == null)
        {
            Debug.LogError("MultiplayerTimerScript component not found on any GameObject in the scene.");
        }
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
            // Reset the timer before loading the new scene, this has to be done this way or it doesn't work for some reason
            timerScript.ResetTimer();
            photonView.RPC("LoadSceneForAll", RpcTarget.All, "SingleLevel1");
        }
    }

    [PunRPC]
    public void LoadSceneForAll(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
}
