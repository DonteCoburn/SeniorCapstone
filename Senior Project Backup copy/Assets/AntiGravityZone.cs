using UnityEngine;

public class AntiGravityZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Flip the player's sprite and invert gravity
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale *= -1; // Inverts the gravity
                FlipPlayerSprite(collision);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Revert changes when leaving the anti-gravity zone
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale *= -1; // Reverts the gravity to normal
                FlipPlayerSprite(collision);
            }
        }
    }

    private void FlipPlayerSprite(Collider2D player)
    {
        // This method flips the sprite vertically
        player.transform.localScale = new Vector3(player.transform.localScale.x, -player.transform.localScale.y, player.transform.localScale.z);
    }
}

