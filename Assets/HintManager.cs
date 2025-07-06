using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HintManager : MonoBehaviour
{
    [System.Serializable]
    public class HintData
    {
        public AudioClip audioClip;
        [TextArea]
        public string subtitle;
    }

    public HintData[] hintClips;

    public TextMeshProUGUI subtitleText;
    public AudioSource audioSource;
    public GameObject hintPanel;
    public GameObject jobInfoPanel;
    public Button closeButton;

    private int currentHintIndex = 0;
    private bool isPanelOpen = false;

    void Start()
    {
        hintPanel.SetActive(false);
        closeButton.onClick.AddListener(CloseHintPanel);
    }

    public void ShowHint()
    {
        if (hintClips == null || hintClips.Length == 0)
        {
            Debug.LogWarning("❗ No hints assigned.");
            return;
        }

        if (currentHintIndex >= hintClips.Length)
            currentHintIndex = 0;

        hintPanel.SetActive(true);
        isPanelOpen = true;

        subtitleText.text = hintClips[currentHintIndex].subtitle;
        audioSource.clip = hintClips[currentHintIndex].audioClip;
        audioSource.Play();

        currentHintIndex++; // Move to next for future click
    }

    public void CloseHintPanel()
    {
        hintPanel.SetActive(false);
        audioSource.Stop();
        isPanelOpen = false;
    }

    public void CloseJobInfoPanel()
    {
        if (jobInfoPanel != null)
        {
            jobInfoPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
