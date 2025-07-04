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
    public GameObject nextButton;
    public GameObject previousButton;
    public GameObject hintTriggerButton;

    private int currentIndex = 0;
    private bool isPanelOpen = false;

    public void ToggleHintPanel()
    {
        if (isPanelOpen)
        {
            hintPanel.SetActive(false);
            audioSource.Stop();
        }
        else
        {
            hintPanel.SetActive(true);
            currentIndex = 0;
            ShowHint(currentIndex);
            previousButton.SetActive(false);
            nextButton.SetActive(true);
        }

        isPanelOpen = !isPanelOpen;
    }

    public void ShowNextHint()
    {
        if (currentIndex < hintClips.Length - 1)
        {
            currentIndex++;
            ShowHint(currentIndex);
        }

        if (currentIndex == hintClips.Length - 1)
        {
            nextButton.SetActive(false);
            Invoke(nameof(AutoCloseHintPanel), 8f);
        }

        previousButton.SetActive(true);
    }

    public void ShowPreviousHint()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            ShowHint(currentIndex);
        }

        nextButton.SetActive(true);
        if (currentIndex == 0)
        {
            previousButton.SetActive(false);
        }
    }

    private void ShowHint(int index)
    {
        subtitleText.text = hintClips[index].subtitle;
        audioSource.clip = hintClips[index].audioClip;
        audioSource.Play();

        previousButton.SetActive(index > 0);
        nextButton.SetActive(index < hintClips.Length - 1);
    }

    private void AutoCloseHintPanel()
    {
        hintPanel.SetActive(false);
        audioSource.Stop();
        isPanelOpen = false;
    }
}
