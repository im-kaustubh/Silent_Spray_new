using UnityEngine;

public class JobButtonTrigger : MonoBehaviour
{
    [TextArea(3, 10)]
    public string riddleText;
    public string sceneName;
    public JobInfoPanelManager jobInfoPanelManager;

    public void OnButtonClick()
    {
        if (jobInfoPanelManager.IsPanelOpen)
        {
            jobInfoPanelManager.HideInfo();
        }
        else
        {
            jobInfoPanelManager.ShowInfo(riddleText, sceneName);
        }
    }
}
