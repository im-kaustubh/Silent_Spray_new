using UnityEngine;

public class ControlsPanelManager : MonoBehaviour
{
    public GameObject controlsPanel;

    public void ToggleControlsPanel()
    {
        controlsPanel.SetActive(!controlsPanel.activeSelf);
    }

    public void CloseControlsPanel()
    {
        controlsPanel.SetActive(false);
    }
}
