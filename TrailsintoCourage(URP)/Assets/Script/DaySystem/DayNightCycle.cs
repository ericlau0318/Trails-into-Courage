using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Gradients")]
    [SerializeField] private Gradient fogGradient;
    [SerializeField] private Gradient ambientGradient;
    [SerializeField] private Gradient directionLightGradient;
    [SerializeField] private Gradient skyboxTintGradient;
    [Header("Environmental Assets")]
    [SerializeField] private Light directionalLight;
    [SerializeField] private Material daySkyboxMaterial;

    [Header("Variables")]
    [SerializeField] private float dayDurationInSeconds = 60f;
    [SerializeField] private float rotationSpeed = 1f;

    public float currentTime = 0;
    private Material currentSkyboxMaterial;

    private void Start()
    {
        currentSkyboxMaterial = daySkyboxMaterial; // Initialize with the day skybox
        RenderSettings.skybox = currentSkyboxMaterial;
    }

    private void Update()
    {
        UpdateTime();
        UpdateDayNightCycle();
        RotateSkybox();
    }

    private void UpdateTime()
    {
        float timeMultiplier = GetTimeMultiplier(currentTime);
        currentTime += (Time.deltaTime / dayDurationInSeconds) * timeMultiplier;
        currentTime = Mathf.Repeat(currentTime, 1f);
    }
    private float GetTimeMultiplier(float currentTime)
    {
        // Here you can customize which parts of the day should move slower or faster
        if (currentTime >= 0.2f && currentTime <= 0.4f) // Example: slow down the morning to noon transition
        {
            return 0.1f; // Slows down time change to half speed
        }
        else if (currentTime >= 0.6f && currentTime <= 0.8f) // Example: slow down the evening to night transition
        {
            return 0.1f;
        }
        return 1f; // Normal time progression speed
    }
    private void UpdateDayNightCycle()
    {
        float sunPosition = Mathf.Repeat(currentTime + 0.25f, 1f);
        directionalLight.transform.rotation = Quaternion.Euler(sunPosition * 360f, 0f, 0f);

        RenderSettings.fogColor = fogGradient.Evaluate(currentTime);
        RenderSettings.ambientLight = ambientGradient.Evaluate(currentTime);

        directionalLight.color = directionLightGradient.Evaluate(currentTime);
        // Use the current skybox material for tint adjustments
        currentSkyboxMaterial.SetColor("_Tint", skyboxTintGradient.Evaluate(currentTime));
    }
    private void RotateSkybox()
    {
        float currentRotation = currentSkyboxMaterial.GetFloat("_Rotation");
        float newRotation = currentRotation + rotationSpeed * Time.deltaTime;
        newRotation = Mathf.Repeat(newRotation, 360f);
        currentSkyboxMaterial.SetFloat("_Rotation", newRotation);
    }


    private void OnApplicationQuit()
    {
        // Reset to daySkyboxMaterial to avoid editor issues
        currentSkyboxMaterial.SetColor("_Tint", new Color(0.5f, 0.5f, 0.5f));
        RenderSettings.skybox = daySkyboxMaterial;
    }
}