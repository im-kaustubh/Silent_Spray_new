using UnityEngine;
using UnityEngine.UI;

public class GraffitiSprayer : MonoBehaviour
{
    public GraffitiSelector graffitiSelector;
    public SprayProgress sprayProgress;
    public Transform sprayPoint;

    public GameObject sprayProgressBar;
    private Slider progressSlider;

    private Sprite[] currentFrames;
    private float sprayTimer;
    private int currentFrameIndex;
    private bool isSpraying;

    private GameObject staticPreview;

    void Start()
    {
        if (sprayProgressBar != null)
        {
            progressSlider = sprayProgressBar.GetComponent<Slider>();
            sprayProgressBar.SetActive(false);
        }
    }

    void Update()
    {
        string graffitiName = graffitiSelector.GetSelectedGraffitiName();
        if (string.IsNullOrEmpty(graffitiName)) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            LoadFrames(graffitiName);
            if (currentFrames.Length > 0)
            {
                isSpraying = true;
                sprayTimer = 0f;
                currentFrameIndex = 0;

                if (staticPreview == null)
                {
                    staticPreview = new GameObject("StaticPreview");
                    SpriteRenderer renderer = staticPreview.AddComponent<SpriteRenderer>();
                    renderer.sortingLayerName = "Ground";
                    renderer.sortingOrder = 5;
                    staticPreview.transform.position = sprayPoint.position;
                }

                sprayProgressBar.SetActive(true);
                sprayProgress.SetMaxValue(currentFrames.Length);
            }
        }

        if (isSpraying && Input.GetKey(KeyCode.E))
        {
            sprayTimer += Time.deltaTime;

            if (currentFrameIndex < currentFrames.Length && sprayTimer >= currentFrameIndex + 1f)
            {
                staticPreview.GetComponent<SpriteRenderer>().sprite = currentFrames[currentFrameIndex];
                sprayProgress.SetValue(currentFrameIndex + 1);
                currentFrameIndex++;
            }

            if (currentFrameIndex >= currentFrames.Length)
            {
                FinalizeSpray();
            }
        }

        if (Input.GetKeyUp(KeyCode.E) && isSpraying)
        {
            FinalizeSpray();
        }
    }

    void FinalizeSpray()
    {
        isSpraying = false;
        sprayTimer = 0f;

        if (staticPreview != null)
        {
            staticPreview.name = "StaticGraffiti_" + currentFrames[^1].name;
            staticPreview.GetComponent<SpriteRenderer>().sprite = currentFrames[^1];

            var validator = Object.FindFirstObjectByType<SprayValidator>();
            if (validator != null)
                validator.ValidateSpray(staticPreview.name);

            staticPreview = null;
        }

        sprayProgressBar.SetActive(false);
        sprayProgress.ResetToTimeBased();
    }

    void LoadFrames(string graffitiName)
    {
        currentFrames = graffitiSelector.GetSelectedGraffitiFrames();

        if (currentFrames == null || currentFrames.Length == 0)
            Debug.LogError("❌ No frames assigned for selected graffiti!");
        else
            Debug.Log("✅ Loaded " + currentFrames.Length + " frames from selector");
    }
}
