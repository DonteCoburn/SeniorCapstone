using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Class to allow buttons to interact with the static Level Manager
/// </summary>
public class LevelSwitcher : MonoBehaviour
{
    /// <summary>
    /// Description:
    /// Passes a string to the Level Manager, which loads a scene using that name
    /// Input:
    /// string sceneName
    /// Return:
    /// void (no return)
    /// </summary>
    /// <param name="sceneName">The name of the scene to be loaded</param>
    public void LoadScene(string sceneName)
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
        if (sceneName == "MainMenu" && PhotonNetwork.IsConnected)
        {
            Debug.Log("Disconnecting from Photon and returning to our main menu");
            PhotonNetwork.Disconnect(); // This part disconnects the user from Photon so they can later rejoin -Donte
        }
        LevelManager.LoadScene(sceneName);
    }
}
