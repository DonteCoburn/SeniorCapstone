using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.Collections.Generic;

//Basically how this code works is it cycles through the current players whenever you hit the spectate button
//Then it tells the players own camera to change target based on where it is in the cycle
//Technically there is a minor low-priority bug if the currect target leaves, but it only makes the camera look at nothing
//Bug goes away when button is hit once more.
//-Donte


public class SpectateSystem : MonoBehaviourPun
{
    public Button logButton;
    private int currentActorNumber;


    //Initially there is no current number, it gets assigned to the player's number
    void Start()
    {
        if (logButton != null)
        {
            logButton.onClick.AddListener(UpdateCurrentActorNumber);
        }
        currentActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        Debug.Log($"Initial actor number is {currentActorNumber}");
    }

    //Whenever the button is hit it goes through the players and gets the next player and changes camera target
    void UpdateCurrentActorNumber()
    {
        List<int> actorNumbers = new List<int>();
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            actorNumbers.Add(player.ActorNumber);
        }

        actorNumbers.Sort();

        int currentIndex = actorNumbers.IndexOf(currentActorNumber);
        Debug.Log($"Current index: {currentIndex} for actor number: {currentActorNumber}");

        if (currentIndex + 1 < actorNumbers.Count)
        {
            currentActorNumber = actorNumbers[currentIndex + 1];
        }
        else
        {
            currentActorNumber = actorNumbers[0];
        }

        MultiplayerCameraFollow cameraFollow = FindObjectOfType<MultiplayerCameraFollow>();
        if (cameraFollow != null)
        {
            cameraFollow.SetTarget(currentActorNumber);
            Debug.Log($"Updated camera target to actor number: {currentActorNumber}");
        }
    }
}