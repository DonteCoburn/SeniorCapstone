using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class QualifiedManager : MonoBehaviourPunCallbacks
{
    public TMP_Text gameModeText;
    public TMP_Text qualifiedText;
    public int totalQualifiersNeeded;

    private int playersQualified = 0;

    private void Start()
    {
        playersQualified = 0;
        gameModeText.text = "Race to the Finish!";
        totalQualifiersNeeded = Mathf.CeilToInt(PhotonNetwork.CurrentRoom.PlayerCount / 2.0f);
        UpdateQualifiedText(); // Update locally as well since this is only setting up the initial text.
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
        //Temp Code, change later
        Debug.Log("Race has ended. Enough players have qualified.");
        photonView.RPC("UpdateAllToSpectateMode", RpcTarget.All);
    }

    [PunRPC]
    public void UpdateAllToSpectateMode()
    {
        gameModeText.text = "Spectate\nMode";
    }
}
