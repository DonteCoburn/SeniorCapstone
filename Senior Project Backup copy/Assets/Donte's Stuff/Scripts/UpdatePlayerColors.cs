using UnityEngine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;

public class UpdatePlayerColors : MonoBehaviour
{
    public ColorManager colorManager;

    void Start()
    {
        StartCoroutine(PeriodicUpdate());
    }

    public void UpdateAllPlayerColors()
    {
        Dictionary<int, GameObject> playerObjects = new Dictionary<int, GameObject>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            PhotonView photonView = player.GetComponent<PhotonView>();
            if (photonView != null)
            {
                playerObjects[photonView.Owner.ActorNumber] = player;
            }
        }

        List<int> sortedActorNumbers = new List<int>(playerObjects.Keys);
        sortedActorNumbers.Sort();

        foreach (int actorNumber in sortedActorNumbers)
        {
            GameObject player = playerObjects[actorNumber];
            Color color = colorManager.GetPlayerColor(actorNumber);
            // Update SpriteRenderer
            SpriteRenderer spriteRenderer = player.GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = color;
            }
            // Directly update TrailRenderer
            TrailRenderer trailRenderer = player.GetComponent<TrailRenderer>();
            if (trailRenderer != null)
            {
                trailRenderer.startColor = color;
                trailRenderer.endColor = color;
            }
        }
        Debug.Log("Player colors have been updated");
    }

    IEnumerator PeriodicUpdate()
    {
        while (true)
        {
            UpdateAllPlayerColors();
            yield return new WaitForSeconds(1);  // Update colors every 1 second as a failsafe
        }
    }
}