using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;
    public Animator fadeAnimator;
    public Image fadeImage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Also keep FadePanel alive
            if (fadeAnimator != null)
            {
                DontDestroyOnLoad(fadeAnimator.gameObject);
            }
            if (fadeImage != null)
            {
                DontDestroyOnLoad(fadeImage.gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeAndSwitch(sceneName));
    }

    private IEnumerator FadeAndSwitch(string sceneName)
    {
        fadeAnimator.SetTrigger("StartFadeOut"); // 🔄 only play when triggered
        yield return new WaitForSeconds(1f); // wait for fade animation to finish
        SceneManager.LoadScene(sceneName);
    }
}
