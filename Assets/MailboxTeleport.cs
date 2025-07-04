using System.Collections;
using UnityEngine;

public class MailboxTeleport : MonoBehaviour
{
    public string targetSceneName = "NewspaperArea";
    [SerializeField] GameObject sunrise;
    [SerializeField] int animationDuration;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(playAnimation());
            FadeManager.Instance.FadeToScene(targetSceneName);
        }
    }

    IEnumerator playAnimation()
    {
        sunrise.SetActive(true);
        yield return new WaitForSeconds(animationDuration);
    }
}