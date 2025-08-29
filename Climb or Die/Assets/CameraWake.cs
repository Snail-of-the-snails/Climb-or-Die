using UnityEngine;

public class CameraWake : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 30f; // degrees per second
    private float OriginalSpeed;
    [SerializeField] private float targetAngle = 90f;

    private Transform cameraTransform;
    private float currentAngle = 0f;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        cameraTransform.rotation = Quaternion.Euler(0, 0, 0);
        currentAngle = cameraTransform.eulerAngles.x;
        OriginalSpeed = rotationSpeed;
    }

    void Update()
    {
        if (currentAngle > targetAngle)
        {
            float step = rotationSpeed * Time.deltaTime;
            currentAngle = Mathf.Max(currentAngle - step, targetAngle);
            cameraTransform.rotation = Quaternion.Euler(currentAngle, 0, 0);
            if(rotationSpeed > 0)   
                rotationSpeed -=0.05f; // Gradually decrease speed

        }
    }
}
