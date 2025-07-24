using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HintsManager : MonoBehaviour
{
    [System.Serializable]
    public class HintData{
        public AudioClip hintAudios;
        [TextArea] public string hintText;
    }

    [System.Serializable]
    public class RiddleHints
    {
        public HintData[] hints;
    }

    public static HintsManager instance;

    public RiddleHints[] allRiddleHints;

    public TextMeshProUGUI subtitleText;
    public AudioSource audioSource;
    public GameObject hintPanel;
    public Button closeButton;

    private int[] hintProgress;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            hintProgress = new int[allRiddleHints.Length];
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AssignUI(GameObject panel, TextMeshProUGUI subtitle, AudioSource audio, Button close)
    {
        hintPanel = panel;
        subtitleText = subtitle;
        audioSource = audio;
        closeButton = close;

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(CloseHintPanel);

        if (hintPanel != null)
            hintPanel.SetActive(false);

        Debug.Log("UI set up done");
    }

    public void ShowHint()
    {

        int riddleIndex = GameManager.instance.activeRiddle;

        if (riddleIndex == -1 || riddleIndex >= allRiddleHints.Length)
        {
            print("no riddle or hints assigned");
            return;
        }

        if (hintPanel == null || subtitleText == null || audioSource == null || closeButton == null)
        {
            Debug.LogWarning("no UI assigned");
            return;
            
        }
        
        HintData[] hints = allRiddleHints[riddleIndex].hints;

        int currentIndex = hintProgress[riddleIndex];

        subtitleText.text = hints[currentIndex].hintText;
        audioSource.clip = hints[currentIndex].hintAudios;
        audioSource.Play();

        hintPanel.SetActive(true);

        hintProgress[riddleIndex] = (currentIndex + 1) % hints.Length;
    }

    public void CloseHintPanel()
    {
        if(hintPanel != null)
        {
            hintPanel.SetActive(false);
        }

        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    [ContextMenu("Reset Hints")]
    public void ResetHints()
    {
        for (int i = 0; i < hintProgress.Length; i++)
            hintProgress[i] = 0;
    }
}
