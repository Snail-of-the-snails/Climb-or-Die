using UnityEngine;

public class FlashlightSway : MonoBehaviour
{
    private Vector3 vectorOffset;
    private GameObject playerCamera;
    [SerializeField] private float speed = 3.0f;

    void Start()
    {
        playerCamera = Camera.main.gameObject;
        vectorOffset = transform.transform.position - playerCamera.transform.position;
    }

    void Update()
    {
        transform.position = playerCamera.transform.position + vectorOffset;
        transform.rotation = Quaternion.Slerp(transform.transform.rotation, playerCamera.transform.rotation, speed * Time.deltaTime);
    }
}
