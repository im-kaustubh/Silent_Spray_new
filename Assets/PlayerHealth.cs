using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image[] hearts;
    public Transform playerTransform;
    public Vector3 startPosition; // Set this to where the player should respawn

    private int currentHealth = 3;
    private float hitCooldown = 2f;
    private float lastHitTime = -10f;

    private GameOverManager gameOverManager;

    void Start()
    {
        gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager == null)
        {
            Debug.LogWarning("⚠️ GameOverManager not found in scene!");
        }

        // Optional: set default respawn point from player's current position
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        startPosition = playerTransform.position;
    }

    public void TakeDamage()
    {
        if (Time.time - lastHitTime < hitCooldown || currentHealth <= 0) return;

        lastHitTime = Time.time;
        currentHealth--;

        if (currentHealth >= 0 && currentHealth < hearts.Length)
        {
            hearts[currentHealth].enabled = false;
        }

        if (currentHealth <= 0)
        {
            Debug.Log("💀 Player died — triggering game over");
            if (gameOverManager != null)
            {
                gameOverManager.TriggerGameOver();
            }
        }
        else
        {
            Debug.Log("🚨 Player caught — respawning at start");
            playerTransform.position = startPosition;

            // Optional: shake camera
            FindObjectOfType<CameraShake>()?.Shake();
        }
    }
}
