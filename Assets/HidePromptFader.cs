using UnityEngine;

public class HidePromptFader : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeSpeed = 5f;

    private float targetAlpha = 0f;

    void Start()
    {
        canvasGroup.alpha = 0f; // start invisible
    }

    void Update()
    {
        canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, fadeSpeed * Time.deltaTime);
    }

    public void Show()
    {
        targetAlpha = 1f;
    }

    public void Hide()
    {
        targetAlpha = 0f;
    }
}
