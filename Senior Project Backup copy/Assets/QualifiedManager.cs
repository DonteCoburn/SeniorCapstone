using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class QualifiedManager : MonoBehaviourPunCallbacks
{
    public TMP_Text gameModeText;
    public TMP_Text qualifiedText;
    public int totalQualifiersNeeded;
    public GameObject winpage; // UI element to show when the race ends

    private int playersQualified = 0;

    private void Start()
    {
        playersQualified = 0;
        gameModeText.text = "Race to the Finish!";
        totalQualifiersNeeded = Mathf.CeilToInt(PhotonNetwork.CurrentRoom.PlayerCount / 2.0f);
        UpdateQualifiedText(); // Update locally as well since this is only setting up the initial text.
        winpage.SetActive(false); // Initially hide the win page
    }

    public void PlayerReachedGoal()
    {
        photonView.RPC("IncrementQualifiedCount", RpcTarget.All);
    }

    [PunRPC]
    void IncrementQualifiedCount()
    {
        playersQualified++;
        UpdateQualifiedText();

        if (playersQualified >= totalQualifiersNeeded)
        {
            EndRace();
        }
    }

    private void UpdateQualifiedText()
    {
        qualifiedText.text = $"{playersQualified} out of {totalQualifiersNeeded} Qualified";
    }

    public void SwitchToSpectateMode()
    {
        gameModeText.text = "Spectate\nMode";
    }

    private void EndRace()
    {
        Debug.Log("Race has ended. Enough players have qualified.");
        photonView.RPC("ShowWinPage", RpcTarget.All);
        DisableAllPlayersMovement();
    }

    private void DisableAllPlayersMovement()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player"); // Assuming all player GameObjects are tagged as "Player"
        foreach (GameObject player in players)
        {
            PhotonView pv = player.GetComponent<PhotonView>();
            if (pv != null)
            {
                pv.RPC("DisableMovement", RpcTarget.All); // Call the RPC on each player's PhotonView
            }
        }
    }

    [PunRPC]
    public void ShowWinPage()
    {
        winpage.SetActive(true);  // Show the win page to all players
        gameModeText.text = "Spectate\nMode";  // Update game mode text for all players
    }
}
