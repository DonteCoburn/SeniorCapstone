using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

/// <summary>
/// This class is meant to be used on buttons as a quick easy way to load levels (scenes)
/// </summary>
public class LevelLoadButton : MonoBehaviour
{
    /// <summary>
    /// Description:
    /// Loads a level according to the name provided
    /// </summary>
    /// <param name="levelToLoadName">The name of the level to load</param>
    public void LoadLevelByName(string levelToLoadName)
    {
        //Debug statements
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Level switched. Currently connected to Photon.");
        }
        else
        {
            Debug.Log("Level switched. Not connected to Photon. That's okay");
        }

        // Actual code
        if (levelToLoadName == "MainMenu" && PhotonNetwork.IsConnected)
        {
            Debug.Log("Disconnecting from Photon and returning to our main menu");
            PhotonNetwork.Disconnect(); // This part disconnects the user from Photon so they can later rejoin -Donte
        }
        SceneManager.LoadScene(levelToLoadName);
    }
}
