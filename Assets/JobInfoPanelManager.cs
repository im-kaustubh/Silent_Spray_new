/*using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JobInfoPanelManager : MonoBehaviour
{
    public GameObject jobInfoPanel;
    //public TMP_Text infoText;

    public void ShowInfoPanel()
    {
        jobInfoPanel.SetActive(true);
    }

    public void HideInfo()
    {
        jobInfoPanel.SetActive(false);
    }

    public void AcceptMission()
    {
        // TODO: Add logic here to load mission scene or flag mission start
        Debug.Log("Mission Accepted!");
    }
}*/

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class JobInfoPanelManager : MonoBehaviour
{
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
        Debug.Log("Mission Accepted! Loading: " + selectedScene);
        Time.timeScale = 1f;
        SceneManager.LoadScene(selectedScene);
    }
}
