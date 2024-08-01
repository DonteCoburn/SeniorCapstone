using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GoalPickup : MonoBehaviour
{
    public EndGoal endGoal;
    public SpectateSystem spectateSystem;

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
                    spectateSystem.MakeButtonInteractable();
                }
            }
        }
    }
}






/* OLD CODE
public class GoalPickup : Pickup
{
    /// <summary>
    /// Description:
    /// Function called when this pickup is picked up
    /// Tells the game manager that the level was cleared
    /// </summary>
    /// <param name="collision">The collider that is picking up this pickup</param>
    public override void DoOnPickup(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject.GetComponent<Health>() != null)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.LevelCleared();
            }
        }
        base.DoOnPickup(collision);
    }
}

*/