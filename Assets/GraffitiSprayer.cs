using UnityEngine;

public class GraffitiSprayer : MonoBehaviour
{
    public GraffitiSelector graffitiSelector;
    public SprayProgress sprayProgress;
    public Transform sprayPoint;

    public GameObject[] animatedGraffitiPrefabs;
    public GameObject[] staticGraffitiPrefabs;

    private GameObject currentAnimated;
    private bool hasSprayed = false;

    void Update()
    {
        int selectedIndex = graffitiSelector.GetSelectedIndex();

        // Start spraying with animation
        if (Input.GetKey(KeyCode.E) && sprayProgress.CurrentValue > 0f)
        {
            if (currentAnimated == null && !hasSprayed)
            {
                currentAnimated = Instantiate(animatedGraffitiPrefabs[selectedIndex], sprayPoint.position, Quaternion.identity);
            }
        }

        // Spray complete — switch to static graffiti
        if (!hasSprayed && sprayProgress.CurrentValue <= 0f && currentAnimated != null)
        {
            Vector3 pos = currentAnimated.transform.position;
            Destroy(currentAnimated);
            Instantiate(staticGraffitiPrefabs[selectedIndex], pos, Quaternion.identity);
            currentAnimated = null;
            hasSprayed = true;
        }

        // Reset spray lockout when player releases E or bar refills
        if (Input.GetKeyUp(KeyCode.E) || sprayProgress.CurrentValue > 0.1f)
        {
            if (currentAnimated != null)
            {
                Destroy(currentAnimated);
                currentAnimated = null;
            }
            hasSprayed = false;
        }
    }
}
