﻿using UnityEngine;
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

    public bool CanSpray => currentValue > 0f;
    public float CurrentValue => currentValue;

    void Start()
    {
        spraySlider.value = currentValue;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && currentValue > 0f)
        {
            isSpraying = true;
            isRefilling = false;
            refillTimer = 0f;

            currentValue -= Time.deltaTime / sprayTime;
            currentValue = Mathf.Clamp01(currentValue);
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
            {
                isRefilling = true;
            }
        }

        if (isRefilling)
        {
            currentValue += Time.deltaTime * refillSpeed;
            currentValue = Mathf.Clamp01(currentValue);
            spraySlider.value = currentValue;

            if (currentValue >= 1f)
            {
                isRefilling = false;
                refillTimer = 0f;
            }
        }
    }
}
