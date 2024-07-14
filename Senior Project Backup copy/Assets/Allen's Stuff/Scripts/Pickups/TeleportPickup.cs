using UnityEngine;
using System.Collections;

public class TeleportPickup : MonoBehaviour
{
    [Tooltip("The position to teleport the player to")]
    public Transform teleportTarget;
    [Tooltip("The cooldown time before the pickup can be used again")]
    public float cooldownTime = 2f;

    private bool canTeleport = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canTeleport && other.CompareTag("Player"))
        {
            TeleportPlayer(other.gameObject);
            StartCoroutine(Cooldown());
        }
    }

    private void TeleportPlayer(GameObject player)
    {
        if (teleportTarget != null)
        {
            player.transform.position = teleportTarget.position;
        }
        else
        {
            Debug.LogWarning("Teleport target not set.");
        }
    }

    private IEnumerator Cooldown()
    {
        canTeleport = false;
        yield return new WaitForSeconds(cooldownTime);
        canTeleport = true;
    }
}


