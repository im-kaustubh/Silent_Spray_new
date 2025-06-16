using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    public void TriggerGameOver()
    {
        Debug.Log("🧨 TriggerGameOver() CALLED");

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Debug.Log("✅ gameOverPanel.activeSelf: " + gameOverPanel.activeSelf);
            Debug.Log("✅ Transform position: " + gameOverPanel.transform.position);
            Debug.Log("✅ Rect size: " + gameOverPanel.GetComponent<RectTransform>().rect.size);
            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogError("🚫 GameOverPanel reference is missing!");
        }
    }

    public void Retry()
    {
        Debug.Log("🔁 Retry pressed — Reloading scene");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        Debug.Log("🏠 Menu pressed — Loading 'Newspaper' scene");
        Time.timeScale = 1f;
        SceneManager.LoadScene("NewspaperArea");
    }
}
