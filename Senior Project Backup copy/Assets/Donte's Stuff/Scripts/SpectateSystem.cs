using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.Collections.Generic;

public class SpectateSystem : MonoBehaviourPun
{
    public Button logButton;
    private int currentActorNumber;

    void Start()
    {
        if (logButton != null)
        {
            logButton.onClick.AddListener(UpdateCurrentActorNumber);
            logButton.gameObject.SetActive(false); // Make the button invisible initially
        }
        currentActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        Debug.Log($"Initial actor number is {currentActorNumber}");
    }

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

    // This method now makes the button visible only.
    public void MakeButtonVisible()
    {
        if (logButton != null)
        {
            logButton.gameObject.SetActive(true); // Make the button visible
        }
    }
}
