using UnityEngine;

public class CitywallJobPanelController : MonoBehaviour
{
    public GameObject jobPanel;

    public void ShowJobPanel() // Must be public, void, no arguments
    {
        if (jobPanel != null)
        {
            jobPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("❌ JobPanel not assigned!");
        }
    }
}
