using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GraffitiSprayer : MonoBehaviour
{
    public GraffitiSelector graffitiSelector;
    public SprayProgress sprayProgress;
    public Transform sprayPoint;

    public GameObject sprayProgressBar;
    public GameObject monologuePanel;
    public TextMeshProUGUI monologueText;
    public QTEManager qteManager;

    private Sprite[] currentFrames;
    private float sprayTimer;
    private int currentFrameIndex;
    private bool isSpraying;

    private GameObject staticPreview;
    private bool qteStarted = false;
    private bool qteCompleted = false;

    private bool sprayFinished = false;
    private bool sprayLockedUntilKeyReleased = false;
    private bool waitingForEReleaseAfterSuccess = false;

    void Start()
    {
        if (sprayProgressBar != null)
            sprayProgressBar.SetActive(false);

        if (monologuePanel != null)
            monologuePanel.SetActive(false);
    }

    void Update()
    {
        if ((sprayFinished || sprayLockedUntilKeyReleased) && Input.GetKey(KeyCode.E))
        {
            waitingForEReleaseAfterSuccess = true;
            return;
        }

        if (waitingForEReleaseAfterSuccess && Input.GetKeyUp(KeyCode.E))
        {
            sprayFinished = false;
            sprayLockedUntilKeyReleased = false;
            waitingForEReleaseAfterSuccess = false;
            return;
        }

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
                qteStarted = false;
                qteCompleted = false;

                sprayProgress.SetSprayCostPerFrame(currentFrames.Length);

                if (staticPreview == null)
                {
                    staticPreview = new GameObject("StaticPreview");
                    SpriteRenderer renderer = staticPreview.AddComponent<SpriteRenderer>();
                    renderer.sortingLayerName = "Ground";
                    renderer.sortingOrder = 5;
                    staticPreview.transform.position = sprayPoint.position;
                }

                sprayProgressBar.SetActive(true);
            }
        }

        if (isSpraying && Input.GetKey(KeyCode.E))
        {
            if (qteStarted && !qteCompleted) return;

            sprayTimer += Time.deltaTime;

            if (currentFrameIndex < currentFrames.Length && sprayTimer >= currentFrameIndex + 1f)
            {
                if (currentFrameIndex == currentFrames.Length - 1)
                {
                    staticPreview.GetComponent<SpriteRenderer>().sprite = currentFrames[currentFrameIndex];
                    currentFrameIndex++;
                    FinalizeSpray(currentFrames.Length - 1);
                    return;
                }

                if (!sprayProgress.CanSpray || !sprayProgress.UseSprayPerFrame())
                {
                    FailSpray("You don't have enough spray! Wait 5 seconds...");
                    return;
                }

                staticPreview.GetComponent<SpriteRenderer>().sprite = currentFrames[currentFrameIndex];
                currentFrameIndex++;

                if (currentFrameIndex == 4 && !qteStarted)
                {
                    qteStarted = true;
                    qteManager.BeginQTE(this);
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.E) && isSpraying)
        {
            if (!qteCompleted)
            {
                FailSpray("Ohh! You lose your progress");
            }
            else
            {
                int frameToPaint = Mathf.Clamp(currentFrameIndex - 1, 0, currentFrames.Length - 1);
                FinalizeSpray(frameIndexToUse: frameToPaint);
            }
        }
    }

    public void OnQTESuccess() => qteCompleted = true;

    public void OnQTEFail() => FailSpray("Ohh! You lose your progress");

    void FinalizeSpray(int frameIndexToUse)
    {
        isSpraying = false;
        sprayTimer = 0f;
        sprayFinished = true;
        sprayLockedUntilKeyReleased = true;
        waitingForEReleaseAfterSuccess = true;

        if (staticPreview != null)
        {
            Destroy(staticPreview);
            staticPreview = null;
        }

        GameObject prefabToPlace = graffitiSelector.GetSelectedPrefab();
        if (prefabToPlace != null)
        {
            GameObject sprayed = Instantiate(prefabToPlace, sprayPoint.position, Quaternion.identity);
            sprayed.name = prefabToPlace.name + "(Clone)";

            var validator = Object.FindFirstObjectByType<SprayValidator>();
            if (validator != null)
                validator.ValidateSpray(sprayed.name);
        }

        sprayProgressBar.SetActive(false);
    }

    void FailSpray(string message)
    {
        isSpraying = false;
        sprayTimer = 0f;
        currentFrameIndex = 0;
        sprayFinished = false;
        sprayLockedUntilKeyReleased = true;
        waitingForEReleaseAfterSuccess = true;

        if (staticPreview != null)
        {
            Destroy(staticPreview);
            staticPreview = null;
        }

        sprayProgressBar.SetActive(false);

        if (monologuePanel != null && monologueText != null)
        {
            monologuePanel.SetActive(true);
            monologueText.text = message;
            Invoke(nameof(HideMonologue), 2.5f);
        }
    }

    void HideMonologue()
    {
        if (monologuePanel != null)
            monologuePanel.SetActive(false);
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
