using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Collections;


//This is bad code, doesn't work, don't use
public class GameCountdown : MonoBehaviourPun
{
    public TMP_Text countdownText;

    private void Start()
    {
        Debug.Log("Countdown Start");
    }

    public void StartCountdown()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_StartCountdown", RpcTarget.All);
        }
    }

    [PunRPC]
    void RPC_StartCountdown()
    {
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        countdownText.gameObject.SetActive(true);
        int count = 3;
        while (count > 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        countdownText.text = "Go!";
        yield return new WaitForSeconds(1);
        countdownText.gameObject.SetActive(false);
    }
}
