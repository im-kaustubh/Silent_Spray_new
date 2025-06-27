using UnityEngine;

public class GraffitiSelector : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject graffitiPanel;

    [Header("Graffiti Setup")]
    public GraffitiData[] graffitiData;

    private int selectedIndex = 0;

    void Start()
    {
        if (graffitiPanel != null)
            graffitiPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (graffitiPanel != null)
                graffitiPanel.SetActive(!graffitiPanel.activeSelf);
        }
    }

    public void SelectGraffiti(int index)
    {
        if (index >= 0 && index < graffitiData.Length)
        {
            selectedIndex = index;
            if (graffitiPanel != null)
                graffitiPanel.SetActive(false);

            Debug.Log("🎯 Selected graffiti: " + GetSelectedGraffitiName());
        }
    }

    public int GetSelectedIndex() => selectedIndex;

    public string GetSelectedGraffitiName()
    {
        return graffitiData != null && selectedIndex < graffitiData.Length
            ? graffitiData[selectedIndex].name
            : "Unnamed";
    }

    public Sprite[] GetSelectedGraffitiFrames()
    {
        if (graffitiData != null && selectedIndex < graffitiData.Length)
            return graffitiData[selectedIndex].frames;

        Debug.LogWarning("⚠️ No frames found for selected graffiti");
        return null;
    }

    public GameObject GetSelectedPrefab()
    {
        if (graffitiData != null && selectedIndex < graffitiData.Length)
            return graffitiData[selectedIndex].prefab;

        Debug.LogWarning("⚠️ No prefab found for selected graffiti");
        return null;
    }
}
