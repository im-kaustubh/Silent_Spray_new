/*using UnityEngine;

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
}*/
using UnityEngine;
using UnityEngine.UI; // Needed for Slider

public class GraffitiSprayer : MonoBehaviour
{
    public GraffitiSelector graffitiSelector;
    public SprayProgress sprayProgress;
    public Transform sprayPoint;

    public GameObject[] animatedGraffitiPrefabs;
    public GameObject[] staticGraffitiPrefabs;

    public GameObject sprayProgressBar; // 🔧 Assign SprayProgressBar here
    private Slider progressSlider;

    private GameObject currentAnimated;
    private bool hasSprayed = false;

    void Start()
    {
        if (sprayProgressBar != null)
        {
            progressSlider = sprayProgressBar.GetComponent<Slider>();
            sprayProgressBar.SetActive(false); // hidden by default
            Debug.Log("✅ SprayProgressBar linked and hidden.");
        }
        else
        {
            Debug.LogError("❌ SprayProgressBar not assigned in GraffitiSprayer!");
        }
    }

    void Update()
    {
        int selectedIndex = graffitiSelector.GetSelectedIndex();

        // ✅ Prevent spraying if selection is invalid or graffiti not assigned
        bool isGraffitiSelected = selectedIndex >= 0 &&
                                  selectedIndex < staticGraffitiPrefabs.Length &&
                                  staticGraffitiPrefabs[selectedIndex] != null;

        if (!isGraffitiSelected)
        {
            Debug.LogWarning("🚫 No valid graffiti selected. Cannot spray.");
            if (sprayProgressBar != null && sprayProgressBar.activeSelf)
                sprayProgressBar.SetActive(false);
            return;
        }

        // ✅ Spraying in progress
        if (Input.GetKey(KeyCode.E) && sprayProgress.CurrentValue > 0f)
        {
            if (currentAnimated == null && !hasSprayed)
            {
                currentAnimated = Instantiate(animatedGraffitiPrefabs[selectedIndex], sprayPoint.position, Quaternion.identity);
                Debug.Log("🎨 Started spraying animation.");
            }

            if (sprayProgressBar != null && !sprayProgressBar.activeSelf)
                sprayProgressBar.SetActive(true);

            if (progressSlider != null)
                progressSlider.value = sprayProgress.CurrentValue;
        }
        else
        {
            if (sprayProgressBar != null && sprayProgressBar.activeSelf)
                sprayProgressBar.SetActive(false);
        }

        // ✅ Spraying complete
        if (!hasSprayed && sprayProgress.CurrentValue <= 0f && currentAnimated != null)
        {
            Vector3 pos = currentAnimated.transform.position;
            string sprayedGraffitiName = staticGraffitiPrefabs[selectedIndex].name + "(Clone)";

            Destroy(currentAnimated);
            Instantiate(staticGraffitiPrefabs[selectedIndex], pos, Quaternion.identity);
            Debug.Log("🖼️ Spray complete. Static graffiti placed: " + sprayedGraffitiName);

            var validator = Object.FindFirstObjectByType<SprayValidator>();
            if (validator != null)
            {
                validator.ValidateSpray(sprayedGraffitiName);
                Debug.Log("✅ Spray validated.");
            }

            currentAnimated = null;
            hasSprayed = true;
        }

        // ❌ Cancel spray or reset
        if (Input.GetKeyUp(KeyCode.E) || sprayProgress.CurrentValue > 0.1f)
        {
            if (currentAnimated != null)
            {
                Destroy(currentAnimated);
                currentAnimated = null;
                Debug.Log("🛑 Spray cancelled mid-way.");
            }
            hasSprayed = false;
        }
    }
}
