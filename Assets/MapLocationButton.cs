using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLocationButton : MonoBehaviour
{
    public string sceneToLoad;

    public void LoadLocation()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}