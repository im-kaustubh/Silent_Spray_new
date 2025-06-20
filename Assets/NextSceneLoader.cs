using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene(2);
    }
}
