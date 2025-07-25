using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    public static DroneManager Instance;

    private List<DroneMovement> allDrones = new List<DroneMovement>();
    private bool isBoosted = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RegisterDrone(DroneMovement drone)
    {
        allDrones.Add(drone);
    }

    public void BoostAllDrones(float boostMultiplier, float duration)
    {
        if (isBoosted) return;
        isBoosted = true;

        foreach (var drone in allDrones)
        {
            drone.SetSpeedBoost(boostMultiplier);
        }

        StartCoroutine(ResetDroneSpeed(duration));
    }

    private IEnumerator ResetDroneSpeed(float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (var drone in allDrones)
        {
            drone.ResetSpeed();
        }
        isBoosted = false;
    }
}
