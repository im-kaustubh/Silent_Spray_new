using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLocationButton : MonoBehaviour
{
    public string sceneToLoad;
    //public Animator sunsetAnimator;
    public float animationDuration = 2f;

    [SerializeField] GameObject animationObject;

    /* public void LoadLocation()
    {
        Debug.Log("Playing sunset before loading: " + sceneToLoad);

        if (sunsetAnimator != null)
        {
            StartCoroutine(PlayAnimation());
            
            //sunsetAnimator.gameObject.SetActive(true);
            //sunsetAnimator.SetTrigger("PlaySunset");          // Trigger the sunset animation
            //Invoke("LoadScene", animationDuration);           // Wait for animation before loading
        }
        else
        {
            LoadScene(); // Fallback if animator not assigned
        }
    } */

    public void LoadScene()
    {
        Time.timeScale = 1f;                                  // helps to ensure time is not paused
        SceneManager.LoadScene(sceneToLoad);                  // Load the selected scene
    }

    public void LoadLocationWithAnimation()
    {
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(animationDuration);
        animationObject.SetActive(false);
        LoadScene();
    }

    
}
