using System.Collections;
using UnityEngine;

public class OneWayPlatformController : MonoBehaviour
{
    private Collider2D playerCollider;

    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DisableCollision());
        }
    }

    IEnumerator DisableCollision()
    {
        Physics2D.IgnoreCollision(playerCollider, GetComponent<Collider2D>(), true);
        yield return new WaitForSeconds(0.5f); // Time allowed to pass through the platform
        Physics2D.IgnoreCollision(playerCollider, GetComponent<Collider2D>(), false);
    }
}


