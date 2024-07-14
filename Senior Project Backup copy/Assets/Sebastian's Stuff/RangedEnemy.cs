using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public Transform player;
    public GameObject bullet;
    public float startshotcooldown;
    
    private float shotcooldown;

    public float shootingRange;

    void Start()
    {
        shotcooldown = startshotcooldown;
    }

    void Update()
    {
        // Calculate the direction from enemy to player
        Vector2 direction = player.position - transform.position;

        // Set the enemy's rotation to look at the player
        transform.up = direction.normalized;

        // Check if player is within shooting range
        if (direction.magnitude <= shootingRange)
        {
            // Shoot bullet if cooldown is ready
            if (shotcooldown <= 0)
            {
                Instantiate(bullet, transform.position, transform.rotation);
                shotcooldown = startshotcooldown;
            }
            else
            {
                shotcooldown -= Time.deltaTime;
            }
        }
    }
}
