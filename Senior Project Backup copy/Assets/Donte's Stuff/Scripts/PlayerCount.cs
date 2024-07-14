using UnityEngine;
using Photon.Pun;
using TMPro;

//This code controls our playercount UI, pretty self explanitory code IMO -Donte

public class LobbyUIManager : MonoBehaviourPunCallbacks
{
    public TMP_Text playerCountText;

    void Start()
    {
        UpdatePlayerCount();
    }

    public override void OnJoinedRoom()
    {
        UpdatePlayerCount();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        UpdatePlayerCount();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        UpdatePlayerCount();
    }

    void UpdatePlayerCount()
    {
        if (PhotonNetwork.CurrentRoom != null)
        {
            playerCountText.text = "Players in Lobby: " + PhotonNetwork.CurrentRoom.PlayerCount;
        }
    }
}
