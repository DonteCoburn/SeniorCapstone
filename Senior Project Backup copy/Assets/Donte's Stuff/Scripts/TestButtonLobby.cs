using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// This code is for controlling the button that starts the REAL game. -Donte
/// </summary>

public class TestLobbyUIManager : MonoBehaviourPun
{
    public TMP_Text displayText;
    public Button startButton;
    public int startCount = 3;

    private void Start()
    {
        displayText.gameObject.SetActive(false);
    }

    //This is ran when the button is hit, it makes it so they can't attempt to start the game again and the countdown for all players starts
    public void StartNetworkedCountdownAndLoadScene()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.interactable = false;
            photonView.RPC("StartCountdown", RpcTarget.All);
        }
    }

    [PunRPC]
    public void StartCountdown()
    {
        displayText.gameObject.SetActive(true);
        StartCoroutine(CountdownCoroutine());
    }

    //Countdown code
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
            photonView.RPC("LoadSceneForAll", RpcTarget.All, "SingleLevel1");
        }
    }

    [PunRPC]
    public void LoadSceneForAll(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
}