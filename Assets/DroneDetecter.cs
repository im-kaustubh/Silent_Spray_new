using UnityEngine;

public class DroneDetector : MonoBehaviour
{
    public SprayProgress sprayProgress;
    public GameOverManager gameOverManager;

    void OnTriggerStay2D(Collider2D other)
    {
        // Debug.Log("🛰 Trigger Stay Entered with: " + other.name);  // Log when anything enters

        if (other.CompareTag("Drone"))
        {
            // Debug.Log("✅ Drone tag confirmed");

            PlayerHide playerHide = GetComponent<PlayerHide>();

            if (sprayProgress == null)
            {
                Debug.LogWarning("🚫 sprayProgress is not assigned!");
                return;
            }

            if (gameOverManager == null)
            {
                Debug.LogWarning("🚫 gameOverManager is not assigned!");
                return;
            }

            if (playerHide != null && playerHide.isHiding)
            {
                Debug.Log("😎 Player is hiding — safe from drone");
                return;
            }

            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("🚨 Drone spotted you spraying! Triggering game over!");
                gameOverManager.TriggerGameOver();
            }
            else
            {
                // Debug.Log("🔕 Player is not spraying right now");
            }
        }
        else
        {
            //Debug.Log("⛔ Detected object is NOT a Drone");
        }
    }

}
