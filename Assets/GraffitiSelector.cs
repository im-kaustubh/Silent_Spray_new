using UnityEngine;

public class GraffitiSelector : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject graffitiPanel;
    public GameObject[] graffitiPrefabs; // These should be animated graffiti prefabs

    private GameObject selectedGraffitiPrefab;
    private int selectedIndex = 0; // Track which graffiti is selected

    void Start()
    {
        if (graffitiPanel != null)
            graffitiPanel.SetActive(false);

        // Default selection to first graffiti
        if (graffitiPrefabs.Length > 0)
        {
            selectedGraffitiPrefab = graffitiPrefabs[0];
            selectedIndex = 0;
        }
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
        if (index >= 0 && index < graffitiPrefabs.Length)
        {
            selectedGraffitiPrefab = graffitiPrefabs[index];
            selectedIndex = index;

            if (graffitiPanel != null)
                graffitiPanel.SetActive(false);
        }
    }

    public GameObject GetSelectedGraffiti()
    {
        return selectedGraffitiPrefab;
    }

    public int GetSelectedIndex()
    {
        return selectedIndex;
    }
}
