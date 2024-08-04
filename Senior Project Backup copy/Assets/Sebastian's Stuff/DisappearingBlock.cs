using UnityEngine;
using System.Collections;

public class DisappearingBlock : MonoBehaviour
{
    public float visibleTime = 2.0f;  // Time block remains visible
    public float respawnTime = 3.0f;  // Time until block respawns after disappearing

    private float timer;  // Internal timer
    private bool isVisible = true;  // Track visibility state

    private SpriteRenderer spriteRenderer;
    private Collider2D collider2D;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
        if (collider2D == null)
        {
            Debug.LogError("Collider2D component not found on the DisappearingBlock object!");
        }

        timer = visibleTime;
    }

    void Update()
    {
        if (isVisible)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                StartCoroutine(Disappear());
            }
        }
    }

    IEnumerator Disappear()
    {
        isVisible = false;
        spriteRenderer.enabled = false;
        collider2D.enabled = false;
        Debug.Log("Block disappeared!");

        yield return new WaitForSeconds(respawnTime);

        Respawn();
    }

    void Respawn()
    {
        isVisible = true;
        spriteRenderer.enabled = true;
        collider2D.enabled = true;
        timer = visibleTime;  // Reset timer
        Debug.Log("Block respawned!");
    }
}
