using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject mapPanel;
    private bool isMapOpen = false;

    public void ToggleMap()
    {
        isMapOpen = !isMapOpen;
        Debug.Log("Clicked Map Icon");  // Confirm button works
        mapPanel.SetActive(isMapOpen);
    }
}
