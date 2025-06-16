using UnityEngine;
using UnityEngine.SceneManagement;

public class MailboxTeleport : MonoBehaviour
{
    public string targetSceneName = "NewspaperArea"; // Change this to your actual scene name

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
