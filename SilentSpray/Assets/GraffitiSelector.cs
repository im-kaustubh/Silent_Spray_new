using UnityEngine;

public class GraffitiSelector : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject graffitiPanel;
    public GameObject[] graffitiPrefabs;      // Drag your 5 animated graffiti prefabs here

    private GameObject selectedGraffitiPrefab;

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
        if (index >= 0 && index < graffitiPrefabs.Length)
        {
            selectedGraffitiPrefab = graffitiPrefabs[index];

            if (graffitiPanel != null)
                graffitiPanel.SetActive(false);
        }
    }

    public GameObject GetSelectedGraffiti()
    {
        return selectedGraffitiPrefab;
    }
}
