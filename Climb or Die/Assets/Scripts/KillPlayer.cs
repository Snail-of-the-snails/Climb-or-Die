using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KillPlayer : MonoBehaviour
{
    Rigidbody rb;
    public GameObject deathScreen;
    public GameObject crosshairAndStamina;
    public AudioListener audioListener;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        int layerNum = collision.gameObject.layer;

        if (layerNum == LayerMask.NameToLayer("Jeff"))
        {
            ActivateDeathScreen();
        }
    }

    void ActivateDeathScreen()
    {
        deathScreen.active = true;
        crosshairAndStamina.active = false;
        audioListener.enabled = false;
    }
}
