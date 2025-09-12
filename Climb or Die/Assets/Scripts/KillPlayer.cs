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
    Button button;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        button.enabled = false;

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
        button.enabled = false;
        for (float i = 0; i <= 1; i += 0.1f)
        {
            button.image.color = new Color(1, 1, 1, i);
            StartCoroutine(DelayedAction());
        }

    }
    IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(.2f); 
    }
}
