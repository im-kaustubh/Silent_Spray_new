using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HintUIConnecter : MonoBehaviour
{
    [SerializeField] GameObject hintPanel;
    [SerializeField] TextMeshProUGUI subtitleText;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Button closeBtn;

    private void Start()
    {
        if(HintsManager.instance != null)
        {
            HintsManager.instance.AssignUI(hintPanel, subtitleText, audioSource, closeBtn);
        }
        else
        {
            print("No Hint Manager found");
        }
    }
}
