using UnityEngine;

public class PillarInteraction : MonoBehaviour
{
    public GameObject instructionText;
    public GameObject jobPanel;

    private bool playerNear = false;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.F))
        {
            instructionText.SetActive(false);
            jobPanel.SetActive(true);
            Time.timeScale = 0f; // optional: pause game while UI open
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
        }
    }
}
