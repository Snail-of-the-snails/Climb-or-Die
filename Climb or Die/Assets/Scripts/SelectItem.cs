using UnityEngine;
using UnityEditor;

public class SelectItem : MonoBehaviour
{
    public GameObject gunModel;
    public GameObject flashLightModel;

    private void Start()
    {
        flashLightModel.SetActive(true);
        gunModel.SetActive(false);
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            flashLightModel.SetActive(true);
            gunModel.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            flashLightModel.SetActive(false);
            gunModel.SetActive(true);
        }
    }
}
