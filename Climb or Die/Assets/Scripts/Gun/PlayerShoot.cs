using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action shootInput;
    public static Action reloadInput;

    [SerializeField] private KeyCode reloadKey = KeyCode.R;

    public void Update()
    {
        if (Input.GetMouseButton(0)) {
            shootInput?.Invoke();
        }

        if (Input.GetKeyDown(reloadKey)) {
            reloadInput?.Invoke();
        }
    }
}