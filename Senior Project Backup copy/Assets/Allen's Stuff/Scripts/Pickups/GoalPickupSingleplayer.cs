using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPickupSingleplayer : Pickup
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


