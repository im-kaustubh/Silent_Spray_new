using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class JobInfoPanelManager : MonoBehaviour
{
    public GameObject mapPanel;
    public GameObject jobInfoPanel;
    public TMP_Text riddleText;

    private string selectedScene;

    public void ShowInfo(string message, string sceneName)
    {
        riddleText.text = message;
        selectedScene = sceneName;
        jobInfoPanel.SetActive(true);
    }

    public void HideInfoPanel()
    {
        jobInfoPanel.SetActive(false);
    }

    public void AcceptMission()
    {
        Debug.Log("Mission Accepted! Opening Map.");
        jobInfoPanel.SetActive(false); // Hide job info panel
        if (mapPanel != null)
        {
            mapPanel.SetActive(true);  // Show map panel
        }
        else
        {
            Debug.LogWarning("Map Panel reference not set!");
        }
    }
}
