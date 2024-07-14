using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedPositionShooting : MonoBehaviour
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
