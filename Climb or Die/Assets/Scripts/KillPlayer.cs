using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class KillPlayer : MonoBehaviour
{
    Rigidbody rb;
    public GameObject deathScreen;
    public GameObject crosshairAndStamina;
    public AudioMixer audioMixer;
    public Button button;

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
        deathScreen.SetActive(true);
        crosshairAndStamina.SetActive(false);
        audioMixer.SetFloat("volume", -80);
        button.enabled = true;
        button.image.color = new Color(1, 0, 0, 0);
        StartCoroutine(fadeButton());

    }
    IEnumerator fadeButton()
    {
        while(button.image.color.a < 1)
        {
            button.image.color = new Color(1, 0, 0, button.image.color.a + 0.1f);
            Debug.Log(button.image.color.a);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
