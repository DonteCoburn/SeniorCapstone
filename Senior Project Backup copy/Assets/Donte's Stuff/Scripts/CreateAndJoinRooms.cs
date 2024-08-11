using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(createInput.text))
        {
            Debug.LogError("Room name is empty. Please enter a room name.");
            return;
        }
        Debug.Log($"Attempting to create room: {createInput.text}");
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom()
    {
        if (string.IsNullOrEmpty(joinInput.text))
        {
            Debug.LogError("Room name is empty. Please enter a room name.");
            return;
        }
        Debug.Log($"Attempting to join room: {joinInput.text}");
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public void LoadMainMenu()
    {
        Debug.Log("Disconnecting from Photon and returning to our main menu");
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room successfully, loading Game level...");
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Failed to create room: {message}");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Failed to join room: {message}");
    }
}