using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLocationButton : MonoBehaviour
{
    public string sceneToLoad;
    public Animator sunsetAnimator;
    public float animationDuration = 2f;

    public void LoadLocation()
    {
        Debug.Log("Playing sunset before loading: " + sceneToLoad);

        if (sunsetAnimator != null)
        {
            sunsetAnimator.gameObject.SetActive(true);
            sunsetAnimator.SetTrigger("PlaySunset");          // Trigger the sunset animation
            Invoke("LoadScene", animationDuration);           // Wait for animation before loading
        }
        else
        {
            LoadScene(); // Fallback if animator not assigned
        }
    }

    private void LoadScene()
    {
        Time.timeScale = 1f;                                  // Ensure time is not paused
        SceneManager.LoadScene(sceneToLoad);                  // Load the selected scene
    }
}
