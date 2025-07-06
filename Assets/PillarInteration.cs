using UnityEngine;

public class PillarInteraction : MonoBehaviour
{
    public GameObject instructionText;
    public GameObject jobPanel;

    private bool playerNear = false;
    [SerializeField] private AudioSource paperSound;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.F))
        {
            if (jobPanel.activeSelf)
            {
                jobPanel.SetActive(false);
                Time.timeScale = 1f; // Resume the game if paused
            }
            else
            {
                paperSound.Play();
                instructionText.SetActive(false);
                jobPanel.SetActive(true);
                Time.timeScale = 0f; // Pause the game
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            instructionText.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;

            if (instructionText != null)
                instructionText.SetActive(false);

            // Ensure panel is closed when player walks away
            jobPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
