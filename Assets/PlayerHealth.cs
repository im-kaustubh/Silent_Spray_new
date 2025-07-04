using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image[] hearts;
    public Transform playerTransform;
    public Vector3 startPosition;

    private Vector3 startScale;
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

        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
            else
            {
                Debug.LogError("❌ Player GameObject with tag 'Player' not found!");
                return;
            }
        }

        // Store start position and scale
        startPosition = playerTransform.position;
        startScale = playerTransform.localScale;
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
            Debug.Log("🚨 Player caught — respawning");

            // 🔥 Destroy all sprayed graffitis
            GameObject[] sprayed = GameObject.FindGameObjectsWithTag("SprayedGraffiti");
            foreach (GameObject g in sprayed)
            {
                Destroy(g);
            }

            // ⏪ Reset position and scale
            playerTransform.position = startPosition;
            playerTransform.localScale = startScale;

            // 💢 Camera shake
            FindObjectOfType<CameraShake>()?.Shake();
        }
    }

}
