using UnityEngine;

public class BoostPlatform : MonoBehaviour
{
    public float minBoost = 5f;  // Minimum boost force
    public float maxBoost = 20f; // Maximum boost force
    public float chargeTime = 2f; // Time to reach max boost

    private float keyHoldTime = 0f;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.P))
        {
            keyHoldTime += Time.deltaTime;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float boostForce = Mathf.Lerp(minBoost, maxBoost, Mathf.Clamp01(keyHoldTime / chargeTime));
                rb.AddForce(new Vector2(0, boostForce), ForceMode2D.Impulse);
                keyHoldTime = 0; // Reset hold time
            }
        }
    }
}

