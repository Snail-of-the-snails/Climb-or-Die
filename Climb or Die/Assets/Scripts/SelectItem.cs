using UnityEngine;
using UnityEditor;

public class SelectItem : MonoBehaviour
{
    public GameObject gunModel;
    public GameObject flashLightModel;
    public GameObject flashLightLight;

    private void Start()
    {
        flashLightLight.SetActive(true);
        flashLightModel.SetActive(true);
        gunModel.SetActive(false);
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gunModel.activeInHierarchy)
        {
            flashLightLight.SetActive(true);
            flashLightModel.SetActive(true);
            gunModel.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && flashLightModel.activeInHierarchy)
        {
            flashLightLight.SetActive(false);
            flashLightModel.SetActive(false);
            gunModel.SetActive(true);
        }
    }
}
