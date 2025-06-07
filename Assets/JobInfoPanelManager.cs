using UnityEngine;
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
}
