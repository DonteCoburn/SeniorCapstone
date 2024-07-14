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

    private void Start()
    {
        //Have to make this part more elaborate to work with our level structure -Donte
        Vector2 randomPosition;
        if (SceneManager.GetActiveScene().name == "SingleLevel1")
        {
            randomPosition = new Vector2(-14f, 0f);
        }
        else
        {
            randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        }
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        Instantiate(cameraPrefab, randomPosition, Quaternion.identity);
    }
}
