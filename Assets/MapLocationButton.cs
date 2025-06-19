using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLocationButton : MonoBehaviour
{
    public string sceneToLoad;

    public void LoadLocation()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneToLoad);
    }
}