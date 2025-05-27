using UnityEngine;

public class DroneStateSwitcher : MonoBehaviour
{
    public GameObject dronePatrol;
    public GameObject droneSpotlight;
    public GameObject droneAlert;

    private float timer = 0f;
    private bool spotlightActive = false;
    private bool isAlert = false;

    void Start()
    {
        SetMode("patrol");
    }

    void Update()
    {
        if (isAlert) return;

        timer += Time.deltaTime;
        if (timer >= 3f)
        {
            spotlightActive = !spotlightActive;
            SetMode(spotlightActive ? "spotlight" : "patrol");
            timer = 0f;
        }
    }

    public void TriggerAlert()
    {
        isAlert = true;
        SetMode("alert");
    }

    void SetMode(string mode)
    {
        dronePatrol.SetActive(mode == "patrol");
        droneSpotlight.SetActive(mode == "spotlight");
        droneAlert.SetActive(mode == "alert");
    }
}
