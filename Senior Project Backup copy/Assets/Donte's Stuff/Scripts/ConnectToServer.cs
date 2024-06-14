using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("Attempting to connect to Photon...");
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "us"; //making the region specific to see if it fixing the multiple devices issue -Donte :)
        PhotonNetwork.ConnectUsingSettings();
        // Old code :(
        //Debug.Log("Attempting to connect to Photon...");
        //PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server, joining lobby...");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby, loading Lobby scene...");
        SceneManager.LoadScene("Lobby");
    }
}
