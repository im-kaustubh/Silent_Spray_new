using UnityEngine;
using UnityEngine.UI;

public class SprayProgress : MonoBehaviour
{
    public Slider spraySlider;
    public float sprayTime = 9f;
    public float refillDelay = 3f;
    public float refillSpeed = 3f;

    private float currentValue = 1f;
    private bool isSpraying = false;
    private bool isRefilling = false;
    private float refillTimer = 0f;

    private bool useFrameProgress = false;
    private float frameMaxValue = 1f;

    public bool CanSpray => currentValue > 0f;
    public float CurrentValue => currentValue;

    void Start()
    {
        if (spraySlider != null)
        {
            spraySlider.minValue = 0f;
            spraySlider.maxValue = 1f;
            spraySlider.value = currentValue;
        }
    }

    void Update()
    {
        if (useFrameProgress) return; // Skip if in frame-controlled spray mode

        if (Input.GetKey(KeyCode.E) && currentValue > 0f)
        {
            isSpraying = true;
            isRefilling = false;
            refillTimer = 0f;

            currentValue -= Time.deltaTime / sprayTime;
            currentValue = Mathf.Clamp01(currentValue);

            if (spraySlider != null)
                spraySlider.value = currentValue;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            isSpraying = false;
            refillTimer = 0f;
        }

        if (!isSpraying && currentValue < 1f)
        {
            refillTimer += Time.deltaTime;
            if (refillTimer >= refillDelay)
                isRefilling = true;
        }

        if (isRefilling)
        {
            currentValue += Time.deltaTime * refillSpeed;
            currentValue = Mathf.Clamp01(currentValue);

            if (spraySlider != null)
                spraySlider.value = currentValue;

            if (currentValue >= 1f)
            {
                isRefilling = false;
                refillTimer = 0f;
            }
        }
    }

    // Called by GraffitiSprayer: switches to frame-controlled mode
    public void SetMaxValue(int max)
    {
        useFrameProgress = true;
        frameMaxValue = max;

        if (spraySlider != null)
        {
            spraySlider.minValue = 0f;
            spraySlider.maxValue = frameMaxValue;
            spraySlider.value = 0f;
        }
    }

    public void SetValue(int val)
    {
        useFrameProgress = true;

        if (spraySlider != null)
            spraySlider.value = Mathf.Clamp(val, 0, frameMaxValue);
    }

    public void ResetToTimeBased()
    {
        useFrameProgress = false;
        currentValue = 1f;

        if (spraySlider != null)
        {
            spraySlider.minValue = 0f;
            spraySlider.maxValue = 1f;
            spraySlider.value = currentValue;
        }
    }
}
