using UnityEngine;
using UnityEngine.UI;

public class RiddleButtonLinker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null && HintsManager.instance != null)
        {
            button.onClick.AddListener(() => HintsManager.instance.ShowRiddle());
        }

    }
}
