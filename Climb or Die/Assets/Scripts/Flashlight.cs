using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private float flickerRange;
    [SerializeField] private float brightness;
    private bool flashlightEnabled;
    private Light flashlight;
    public GameObject flashlightHolder;
    public GameObject gunHolder;

    void Start() {
        flashlight = transform.GetComponent<Light>();
        flashlightHolder.SetActive(false);
        gunHolder.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            flashlightHolder.SetActive(true);
            gunHolder.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            flashlightHolder.SetActive(false);
            gunHolder.SetActive(true);
            flashlightEnabled = false;
        }
        if (Input.GetKeyDown(KeyCode.F) && flashlightHolder.activeSelf == true)
        {
            if (flashlightEnabled)
            {
                flashlightEnabled = false;
            }
            else
            {
                flashlightEnabled = true;
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