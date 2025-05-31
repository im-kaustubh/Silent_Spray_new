using UnityEngine;
using UnityEngine.SceneManagement;

public class JobLoader : MonoBehaviour
{
    public string sceneToLoad;

    public void LoadJobScene()
    {
        Time.timeScale = 1f; // Resume if paused
        SceneManager.LoadScene(sceneToLoad);
    }
}
