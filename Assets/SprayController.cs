/*
using UnityEngine;

public class SprayController : MonoBehaviour
{
    public KeyCode sprayKey = KeyCode.E;
    private bool isSpraying = false;

    void Update()
    {
        if (Input.GetKey(sprayKey))
        {
            if (!isSpraying)
            {
                isSpraying = true;
                Debug.Log("🎨 Spraying started...");
            }
        }
        else
        {
            if (isSpraying)
            {
                isSpraying = false;
                Debug.Log("🛑 Spraying stopped.");
            }
        }
    }
}
*/
using UnityEngine;

public class SprayController : MonoBehaviour
{
    public KeyCode sprayKey = KeyCode.E;
    public GameObject sprayProgressBar; // 🔧 Assign in Inspector (keep disabled in Hierarchy)

    private bool isSpraying = false;

    void Start()
    {
        if (sprayProgressBar != null)
        {
            sprayProgressBar.SetActive(true); // 🧪 TEMP: Force ON for debug — remove later
            Debug.Log("✅ Forced SprayProgressBar ON at start (for test).");
        }
        else
        {
            Debug.LogError("❌ SprayProgressBar NOT assigned in SprayController!");
        }
    }

    void Update()
    {
        if (Input.GetKey(sprayKey))
        {
            if (!isSpraying)
            {
                isSpraying = true;
                Debug.Log("🎨 Spraying started...");
            }

            if (sprayProgressBar != null && !sprayProgressBar.activeSelf)
            {
                sprayProgressBar.SetActive(true);
                Debug.Log("✅ SprayProgressBar shown.");
            }
        }
        else
        {
            if (isSpraying)
            {
                isSpraying = false;
                Debug.Log("🛑 Spraying stopped.");
            }

            if (sprayProgressBar != null && sprayProgressBar.activeSelf)
            {
                sprayProgressBar.SetActive(false);
                Debug.Log("❌ SprayProgressBar hidden.");
            }
        }
    }
}
