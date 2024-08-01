using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float speed;
    public float destroyDelay = 10.0f; // Time in seconds before the bullet destroys itself

    void Start()
    {
        Destroy(gameObject, destroyDelay); // Schedule the destruction of the bullet
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject); // Destroy the bullet immediately on collision
        }
    }
}
