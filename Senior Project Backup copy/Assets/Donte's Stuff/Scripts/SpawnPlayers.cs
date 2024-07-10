using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//This code makes the player a player object to control and a camera for them to see through.

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject cameraPrefab;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        Instantiate(cameraPrefab, randomPosition, Quaternion.identity);
    }
}
