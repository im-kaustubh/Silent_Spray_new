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
            string sprayedGraffitiName = staticGraffitiPrefabs[selectedIndex].name + "(Clone)";

            Destroy(currentAnimated);
            Instantiate(staticGraffitiPrefabs[selectedIndex], pos, Quaternion.identity);

            // Validate spray

            var validator = Object.FindFirstObjectByType<SprayValidator>();
            if (validator != null)
            {
                validator.ValidateSpray(sprayedGraffitiName);
            }



            currentAnimated = null;
            hasSprayed = true;
        }

        // Reset spray lockout
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