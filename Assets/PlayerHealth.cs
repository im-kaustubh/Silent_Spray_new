using UnityEngine;
using UnityEngine.UI;
using System; 

public class PlayerHealth : MonoBehaviour
{
    public Image[] hearts;
    private int currentHealth = 3;

    private GameOverManager gameOverManager;
    private float hitCooldown = 2f;
    private float lastHitTime = -10f;

    void Start()
    {
        gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager == null)
        {
            Debug.LogWarning("⚠️ GameOverManager not found in scene!");
        }
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
        FindObjectOfType<CameraShake>().Shake();


    }
}
