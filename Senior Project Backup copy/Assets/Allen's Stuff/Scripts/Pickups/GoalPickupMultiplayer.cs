using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GoalPickupMultiplayer : MonoBehaviour
{
    public EndGoal endGoal;
    public SpectateSystem spectateSystem;
    public QualifiedManager qualifiedManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            PhotonView pv = collision.GetComponent<PhotonView>();
            if (pv != null && pv.IsMine)
            {
                PlayerMovement2 movePlayerScript = collision.GetComponent<PlayerMovement2>();
                if (movePlayerScript != null)
                {
                    movePlayerScript.DisableMovement();
                }

                if (endGoal != null)
                {
                    endGoal.SendFinishTime();
                }

                if (spectateSystem != null)
                {
                    spectateSystem.MakeButtonVisible();
                }

                if (qualifiedManager != null)
                {
                    qualifiedManager.PlayerReachedGoal();
                }
            }
        }
    }
}