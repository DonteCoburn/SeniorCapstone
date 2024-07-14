using UnityEngine;

public class ForwardShooter : EnemyBase
{
    [Header("Shooting Settings")]
    public GameObject projectile;
    public float shootingRate = 2f;
    private float nextShootTime = 0f;

    protected override void Update()
    {
        base.Update(); // Maintains any movement or basic behavior defined in EnemyBase
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (Time.time >= nextShootTime)
        {
            ShootForward();
            nextShootTime = Time.time + shootingRate;
        }
    }

    private void ShootForward()
    {
        if (projectile)
        {
            // Instantiate the projectile in front of the enemy
            Instantiate(projectile, transform.position + transform.right, transform.rotation);
        }
    }
}
