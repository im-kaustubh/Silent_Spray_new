using UnityEngine;

public class MailboxTeleport : MonoBehaviour
{
    public string targetSceneName = "NewspaperArea";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FadeManager.Instance.FadeToScene(targetSceneName);
        }
    }
}