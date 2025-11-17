using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    [SerializeField, Range(0, 24)] private float TimeOfDay;
    [HideInInspector] public bool isNight;
    private bool isPaused = false;
    private void awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
    
    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying && !isPaused)
        {
            TimeOfDay += Time.deltaTime / 24f;
            TimeOfDay %= 24;
            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }

        if (TimeOfDay <= 4.3f || TimeOfDay >= 19.7f)
        {
            isNight = true;
        }
        else
        {
            isNight = false;
        }
    }


    private void UpdateLighting(float timePercent)
    {
        if (!isPaused)
        {
            RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
            RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);
            RenderSettings.skybox = UpdateSkybox();
            DirectionalLight.GetComponent<Light>().enabled = !isNight;

            if (DirectionalLight != null)
            {
                DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

                DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
            }
            //Debug.Log(DirectionalLight.transform.localRotation);
        }
    }


    private Material UpdateSkybox() {
        Material skybox;

        if (isNight)
        {
            skybox = Preset.NightSkybox;
        }
        else
        {
            skybox = Preset.DaySkybox;
        }

        return skybox;
    }
    private void OnGameStateChanged(GameState newGameState)
    {
        isPaused = newGameState == GameState.Paused;
        Debug.Log(isPaused);
        enabled = newGameState == GameState.Gameplay;
    }
}