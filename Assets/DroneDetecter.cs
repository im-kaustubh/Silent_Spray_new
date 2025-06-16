using UnityEngine;

public class DroneDetector : MonoBehaviour
{
    public GameOverManager gameOverManager;

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerHide playerHide = other.GetComponent<PlayerHide>();

        if (gameOverManager == null)
        {
            Debug.LogWarning("🚫 gameOverManager is not assigned!");
            return;
        }

        // If player is hiding, drone ignores
        if (playerHide != null && playerHide.isHiding)
        {
            Debug.Log("😎 Player is hiding — safe from drone");
            return;
        }

        // Player is in spotlight and not hiding: game over
        Debug.Log("🚨 Player caught in spotlight! Game Over!");
        gameOverManager.TriggerGameOver();
    }
}
