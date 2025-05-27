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
