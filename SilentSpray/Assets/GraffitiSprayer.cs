using UnityEngine;

public class GraffitiSprayer : MonoBehaviour
{
    public GraffitiSelector graffitiSelector;
    public SprayProgress sprayProgress;
    public Transform sprayPoint;

    private bool hasSprayed = false;

    void Update()
    {
        // If bar is not empty and E is held
        if (Input.GetKey(KeyCode.E) && sprayProgress.CurrentValue > 0f)
        {
            hasSprayed = false; // allow spray again once refilled
        }

        // When bar is fully drained and we haven't sprayed yet
        if (!hasSprayed && sprayProgress.CurrentValue <= 0f)
        {
            SprayGraffiti();
            hasSprayed = true;
        }

        // Reset spray lockout when player releases E or bar refills
        if (Input.GetKeyUp(KeyCode.E) || sprayProgress.CurrentValue > 0.1f)
        {
            hasSprayed = false;
        }
    }

    void SprayGraffiti()
    {
        GameObject prefab = graffitiSelector.GetSelectedGraffiti();
        if (prefab != null)
        {
            Instantiate(prefab, sprayPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("❌ No graffiti prefab selected.");
        }
    }
}
