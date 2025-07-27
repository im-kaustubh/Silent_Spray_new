using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MailboxTeleport : MonoBehaviour
{
    public string targetSceneName = "NewspaperArea";
    [SerializeField] GameObject sunrise;
    [SerializeField] int animationDuration;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance != null && GameManager.instance.IsJobReadyToComplete())
            {
                GameManager.instance.SetRiddleSolved(GameManager.instance.activeRiddle);

                if (GameManager.instance.AllRiddlesSolved())
                {
                    Debug.Log("🎉 All riddles completed — Loading Ending scene");
                    SceneManager.LoadScene("Ending");
                }
                else
                {
                    Debug.Log("✅ Job complete — Returning to newspaper area");
                    StartCoroutine(playAnimation());
                    FadeManager.Instance.FadeToScene(targetSceneName);
                }
            }
            else
            {
                Debug.Log("❌ Cannot leave — mission not complete");
                // Optional: show UI warning
            }
        }
    }

    IEnumerator playAnimation()
    {
        if (sunrise != null)
            sunrise.SetActive(true);

        yield return new WaitForSeconds(animationDuration);
    }
}
