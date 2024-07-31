using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject cameraPrefab;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public UpdatePlayerColors updatePlayerColorsScript; // Reference to the UpdatePlayerColors script

    private void Start()
    {
        // Determine the spawn position based on the current scene
        Vector2 randomPosition;
        if (SceneManager.GetActiveScene().name == "SingleLevel1")
        {
            randomPosition = new Vector2(-14f, 0f);
        }
        else
        {
            randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        }

        // Instantiate the player and camera objects
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        Instantiate(cameraPrefab, randomPosition, Quaternion.identity);

        // Update player colors after all players have spawned
        if (updatePlayerColorsScript != null)
        {
            updatePlayerColorsScript.UpdateAllPlayerColors();
        }
        else
        {
            Debug.LogError("UpdatePlayerColors script not assigned!");
        }
        Debug.Log("Player colors have been updated");
    }
}
