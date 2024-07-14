using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class FindHost : MonoBehaviourPunCallbacks
{
    public GameObject startGameButton;

    private void Start()
    {
        Debug.Log("Start: Checking if current player is master client...");
        // May have to get rid of this line
        //startGameButton.SetActive(false); // Ensure the button is hidden on start

        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("Start: Setting initial host as this player.");
            SetHost(PhotonNetwork.LocalPlayer);
        }
    }

    //Who is the host and who can see the "start game" button needs to be updated any time the player count changes.
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom: A player has joined the room.");
        UpdateButtonVisibility();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log($"OnPlayerEnteredRoom: New player entered: {newPlayer.NickName}");
        UpdateButtonVisibility();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"OnPlayerLeftRoom: Player left: {otherPlayer.NickName}");

        // Check if the current player is now the MasterClient after someone has left.
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("OnPlayerLeftRoom: I am now the MasterClient, checking if I should be the new host.");
            // This client is now the MasterClient, we may need to take over as host
            AssignNewHost(); // Ensure this method adjusts the host responsibilities appropriately
        }
        else
        {
            Debug.Log("OnPlayerLeftRoom: I am not the MasterClient, no need to assign new host.");
        }

        UpdateButtonVisibility();
    }

    private void AssignNewHost()
    {
        // Assign the new host as the next oldest player (next in line)
        Debug.Log("AssignNewHost: Assigning new host as this player.");
        if (PhotonNetwork.IsMasterClient)
        {
            SetHost(PhotonNetwork.MasterClient);
        }
    }

    private void SetHost(Player player)
    {
        // Set custom properties to define who is the host
        ExitGames.Client.Photon.Hashtable hostProps = new ExitGames.Client.Photon.Hashtable
        {
            { "host", player.UserId }
        };
        PhotonNetwork.CurrentRoom.SetCustomProperties(hostProps);
    }

    private void UpdateButtonVisibility()
    {
        // Chek for if the button should be visable based on # of players and host status
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            var roomProps = PhotonNetwork.CurrentRoom.CustomProperties;
            if (roomProps.ContainsKey("host") && PhotonNetwork.LocalPlayer.UserId == roomProps["host"].ToString())
            {
                Debug.Log("UpdateButtonVisibility: This player is the host and there are enough players. Showing button.");
                startGameButton.SetActive(true);
            }
            else
            {
                Debug.Log("UpdateButtonVisibility: This player is not the host or not enough players. Hiding button.");
                startGameButton.SetActive(false);
            }
        }
        else
        {
            Debug.Log("UpdateButtonVisibility: Not enough players to start the game. Hiding button.");
            startGameButton.SetActive(false);
        }
    }
}