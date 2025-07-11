using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GraffitiSprayer : MonoBehaviour
{
    public GraffitiSelector graffitiSelector;
    public Transform sprayPoint;

    public Slider graffitiProgressBar;
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
            if (!IsSprayPointInsideArea())
            {
                FailSpray("You can't spray here!");
                return;
            }

            LoadFrames(graffitiName);
            if (currentFrames.Length > 0)
            {
                isSpraying = true;
                sprayTimer = 0f;
                currentFrameIndex = 0;
                qteStarted = false;
                qteCompleted = false;

                if (staticPreview == null)
                {
                    staticPreview = new GameObject("StaticPreview");
                    SpriteRenderer renderer = staticPreview.AddComponent<SpriteRenderer>();
                    renderer.sortingLayerName = "Ground";
                    renderer.sortingOrder = 5;
                }

                staticPreview.transform.position = sprayPoint.position;
                graffitiProgressBar.value = 0f;
                graffitiProgressBar.gameObject.SetActive(true);
            }
        }

        if (isSpraying && Input.GetKey(KeyCode.E))
        {
            if (!IsSprayPointInsideArea())
            {
                FailSpray("You can't spray here!");
                return;
            }

            if (qteStarted && !qteCompleted) return;

            sprayTimer += Time.deltaTime;

            if (currentFrameIndex < currentFrames.Length && sprayTimer >= currentFrameIndex + 1f)
            {
                if (currentFrameIndex == currentFrames.Length - 1)
                {
                    if (!qteCompleted)
                        return;

                    SetStaticPreviewFrame(currentFrameIndex);
                    currentFrameIndex++;
                    FinalizeSpray(currentFrames.Length - 1);
                    return;
                }

                SetStaticPreviewFrame(currentFrameIndex);

                if (graffitiProgressBar != null && currentFrames.Length > 0)
                    graffitiProgressBar.value = (float)(currentFrameIndex + 1) / currentFrames.Length;

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
                bool sprayFullyCompleted = (currentFrameIndex >= currentFrames.Length);

                if (!sprayFullyCompleted)
                {
                    FailSpray("Ohh! You lose your graffiti");
                }
                else
                {
                    int frameToPaint = Mathf.Clamp(currentFrameIndex - 1, 0, currentFrames.Length - 1);
                    FinalizeSpray(frameToPaint);
                }
            }

            if (graffitiProgressBar != null)
                graffitiProgressBar.value = 0f;
        }
    }

    public void OnQTESuccess() => qteCompleted = true;

    public void OnQTEFail()
    {
        qteCompleted = false;
        qteStarted = false;
        isSpraying = false;
        FailSpray("Ohh! You lose your progress");
    }

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
            sprayed.tag = "SprayedGraffiti";

            SpriteRenderer sr = sprayed.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingLayerName = "Ground";
                sr.sortingOrder = -1;
            }

            var validator = Object.FindFirstObjectByType<SprayValidator>();
            if (validator != null)
                validator.ValidateSpray(sprayed);
        }

        if (graffitiProgressBar != null)
            graffitiProgressBar.gameObject.SetActive(false);
    }

    void FailSpray(string message)
    {
        isSpraying = false;
        sprayTimer = 0f;
        currentFrameIndex = 0;
        sprayFinished = false;
        sprayLockedUntilKeyReleased = false;
        waitingForEReleaseAfterSuccess = false;
        qteStarted = false;
        qteCompleted = false;

        if (staticPreview != null)
        {
            Destroy(staticPreview);
            staticPreview = null;
        }

        if (graffitiProgressBar != null)
        {
            graffitiProgressBar.value = 0f;
            graffitiProgressBar.gameObject.SetActive(false);
        }

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

    void SetStaticPreviewFrame(int index)
    {
        if (staticPreview != null && index < currentFrames.Length)
        {
            var renderer = staticPreview.GetComponent<SpriteRenderer>();
            renderer.sprite = currentFrames[index];
            renderer.sortingLayerName = "Ground";
            renderer.sortingOrder = -1;
            staticPreview.transform.localScale = Vector3.one;
            staticPreview.transform.position = sprayPoint.position;
        }
    }

    // Precise check: is sprayPoint inside a valid spray zone
    bool IsSprayPointInsideArea()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(sprayPoint.position);
        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("SprayableArea"))
                return true;
        }
        return false;
    }

    // Trigger detection (still useful for debug, not used for accuracy now)
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ENTERED: " + other.name + " | TAG: " + other.tag);
        if (other.CompareTag("SprayableArea"))
        {
            Debug.Log("✅ Inside SprayableArea!");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("EXITED: " + other.name + " | TAG: " + other.tag);
        if (other.CompareTag("SprayableArea"))
        {
            Debug.Log("⬅️ Exited SprayableArea.");
        }
    }
}
