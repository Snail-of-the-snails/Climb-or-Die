using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private float flickerRange;
    [SerializeField] private float brightness;
    private bool flashlightEnabled;
    private Light flashlight;
    private AudioSource audioSource;
    private bool paused = false;

    void Start() {
        flashlight = transform.GetComponent<Light>();
        audioSource = transform.GetComponent<AudioSource>();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0) && gameObject.activeInHierarchy && !paused)
        {
            if (flashlightEnabled)
            {
                flashlightEnabled = false;
            }
            else
            {
                flashlightEnabled = true;
            }

            audioSource.Play();
        }

        HandleFlashlight();
    }

    void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void HandleFlashlight()
    {
        flashlight.enabled = flashlightEnabled;

        if (flashlightEnabled)
        {
            if (flashlight.intensity <= 500)
            {
                if (flashlight.intensity < (brightness - flickerRange))
                {
                    flashlight.intensity += Random.Range(0, flickerRange);
                }
                else if (flashlight.intensity > (brightness + flickerRange))
                {
                    flashlight.intensity -= Random.Range(0, flickerRange);
                }
                else
                {
                    flashlight.intensity += Random.Range(0 - flickerRange, flickerRange);
                }
            }
            else
            {
                flashlight.intensity = brightness;
            }

        }
    }
    
    private void OnGameStateChanged(GameState newGameState)
    {
        paused = !(newGameState == GameState.Gameplay);
        if (paused)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }
}