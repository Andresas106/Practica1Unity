using System;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    [SerializeField] private float timeMultiplier = 1.0f;
    [SerializeField] private float startHour = 6.0f;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Light sunLight;
    [SerializeField] private float sunriseHour = 6.0f;
    [SerializeField] private float sunsetHour = 18.0f;
    [SerializeField] private Color dayAmbientLight;
    [SerializeField] private Color nightAmbientLight;
    [SerializeField] private AnimationCurve lightChangeCurve;
    [SerializeField] private float maxSunLightIntensity = 1.0f;
    [SerializeField] private Light moonLight;
    [SerializeField] private float maxMoonLightIntensity = 0.5f;

    [SerializeField] private GameObject lightsToActivate; // Añade referencia al GameObject de las luces a activar a las 8 PM

    private DateTime currentTime;
    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;

    private bool lightsActivated = false; // Controla si las luces ya se activaron

    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);

        if (sunLight == null || moonLight == null)
        {
            Debug.LogError("Sunlight or Moonlight not assigned in the Inspector.");
        }
    }

    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();

        CheckAndActivateLights(); // Llama a la función para verificar y activar las luces
    }

    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

        if (timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm");
        }
    }

    private void RotateSun()
    {
        float sunLightRotation;

        if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;
            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;
            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    private void UpdateLightSettings()
    {
        if (sunLight == null || moonLight == null)
        {
            return;
        }

        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        float lightCurveValue = lightChangeCurve.Evaluate(dotProduct);
        sunLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightCurveValue);
        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightCurveValue);
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightCurveValue);
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }

    private void CheckAndActivateLights()
    {
        if (!lightsActivated && currentTime.Hour == 20) // Verifica si son las 8 PM y las luces aún no se han activado
        {
            lightsToActivate.SetActive(true); // Activa las luces
            lightsActivated = true; // Marca las luces como activadas
        }
        else if (lightsActivated && currentTime.Hour == sunriseHour) // Verifica si es la hora del amanecer y las luces están activadas
        {
            lightsToActivate.SetActive(false); // Desactiva las luces
            lightsActivated = false; // Marca las luces como desactivadas
        }
    }
}
