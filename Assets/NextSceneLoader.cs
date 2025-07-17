using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader : MonoBehaviour
{
    [SerializeField] int nextSceneToLoad;

    private void OnEnable()
    {
        SceneManager.LoadScene(nextSceneToLoad);
    }
}
