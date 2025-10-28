using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private float flickerRange;
    [SerializeField] private float brightness;
    private bool flashlightEnabled;
    private Light flashlight;
    private AudioSource audioSource;

    void Start() {
        flashlight = transform.GetComponent<Light>();
        audioSource = transform.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flashlightEnabled)
            {
                flashlightEnabled = false;
                audioSource.Play();
            }
            else
            {
                flashlightEnabled = true;
                audioSource.Play();
            }
        }

        HandleFlashlight();
    }

    private void HandleFlashlight()
    {
        flashlight.enabled = flashlightEnabled;

        if (flashlightEnabled)
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
    }
}