using UnityEngine;

public class DroneDetector : MonoBehaviour
{
    private float hitCooldown = 2f;
    private float lastHitTime = -10f;

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerHide playerHide = other.GetComponent<PlayerHide>();
        if (playerHide != null && playerHide.isHiding)
        {
            Debug.Log("Player is hiding — safe from drone");
            return;
        }

        if (Time.time - lastHitTime < hitCooldown) return;
        lastHitTime = Time.time;

        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            Debug.Log("Player caught by drone — Taking damage");
            playerHealth.TakeDamage();
        }
        else
        {
            Debug.LogWarning("PlayerHealth not found!");
        }
    }
}
