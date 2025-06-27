using UnityEngine;
using UnityEngine.UI;

public class SprayProgress : MonoBehaviour
{
    public Slider spraySlider;
    public float refillDelay = 5f;
    public float refillSpeed = 0.4f;

    private float sprayCapacity = 1f;        // Full spray
    private float sprayUsed = 0f;            // Accumulated spray usage
    private float sprayCostPerFrame = 0.1f;  // Dynamic cost per graffiti frame

    private bool isRefilling = false;
    private float refillTimer = 0f;
    private bool sprayBlocked = false;

    public bool CanSpray => !sprayBlocked;
    public bool IsRefilling => isRefilling;

    void Start()
    {
        if (spraySlider != null)
        {
            spraySlider.minValue = 0f;
            spraySlider.maxValue = sprayCapacity;
            spraySlider.value = sprayCapacity;
            spraySlider.direction = Slider.Direction.RightToLeft; // ✅ Right to Left
        }
    }

    void Update()
    {
        if (sprayBlocked)
        {
            refillTimer += Time.deltaTime;
            if (refillTimer >= refillDelay)
            {
                isRefilling = true;
            }
        }

        if (isRefilling)
        {
            sprayUsed -= Time.deltaTime * refillSpeed;
            sprayUsed = Mathf.Clamp(sprayUsed, 0f, sprayCapacity);

            if (spraySlider != null)
                spraySlider.value = sprayCapacity - sprayUsed;

            if (sprayUsed <= 0f)
            {
                sprayBlocked = false;
                isRefilling = false;
                refillTimer = 0f;
            }
        }
    }

    /// <summary>
    /// Called at the start of spraying to set spray cost based on graffiti frames
    /// </summary>
    public void SetSprayCostPerFrame(int totalFrames)
    {
        sprayCostPerFrame = 1f / totalFrames;
    }

    /// <summary>
    /// Call once per painted frame. Returns false if spray is empty.
    /// </summary>
    public bool UseSprayPerFrame()
    {
        if (sprayBlocked) return false;

        sprayUsed += sprayCostPerFrame;

        if (spraySlider != null)
            spraySlider.value = sprayCapacity - sprayUsed;

        if (sprayUsed >= sprayCapacity)
        {
            sprayBlocked = true;
            refillTimer = 0f;
            return false;
        }

        return true;
    }

    public void ResetSpray()
    {
        sprayUsed = 0f;
        sprayBlocked = false;
        isRefilling = false;
        refillTimer = 0f;

        if (spraySlider != null)
            spraySlider.value = sprayCapacity;
    }
}
