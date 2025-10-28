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

    }

    void Update()
    {

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
            if(flashlight.intensity <= 500){
                if (flashlight.intensity < (brightness - flickerRange) )
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
}