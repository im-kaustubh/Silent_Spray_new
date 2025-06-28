using TMPro;
using UnityEngine;

public class JobInfoPanelManager : MonoBehaviour
{
    public GameObject jobInfoPanel;
    public TMP_Text riddleText;
    public GameObject mapPanel;
    private string selectedScene;

    public bool IsPanelOpen => jobInfoPanel.activeSelf;

    public void ShowInfo(string message, string sceneName)
    {
        riddleText.text = message;
        selectedScene = sceneName;
        jobInfoPanel.SetActive(true);
    }

    public void HideInfo()
    {
        jobInfoPanel.SetActive(false);
    }

    public void OnBackButtonPressed()
    {
        HideInfo();
    }

    public void AcceptMission()
    {
        Debug.Log("Mission Accepted! Opening Map.");
        HideInfo();

        Time.timeScale = 1f;

        if (mapPanel != null)
        {
            mapPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Map Panel reference not set!");
        }
    }
}
