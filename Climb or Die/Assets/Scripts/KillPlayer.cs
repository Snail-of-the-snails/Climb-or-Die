using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class KillPlayer : MonoBehaviour
{
    Rigidbody rb;
    public GameObject deathScreen;
    public GameObject crosshairAndStamina;
    public AudioMixer audioMixer;
    public Button button;
    public TextMeshProUGUI text;
    public GameObject jeff;
    public FirstPersonController FirstPersonControllerScript;


    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        button.enabled = false;
        deathScreen.SetActive(false);
        text.enabled = false;
        crosshairAndStamina.SetActive(true);
        audioMixer.SetFloat("volume",MainMenuScript.volume);
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

        FirstPersonControllerScript.enabled = false;
        jeff.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        deathScreen.SetActive(true);
        crosshairAndStamina.SetActive(false);
        audioMixer.SetFloat("volume", -80);
        button.image.color = new Color(1, 0, 0, 0);
        text.color = new Color(.196f, .196f, .196f, 0f);
       
        StartCoroutine(fadeButton());
    }
    IEnumerator fadeButton()
    {
        yield return new WaitForSeconds(1);
        while (button.image.color.a < 1)
        {
            text.color = new Color(.196f, .196f, .196f, button.image.color.a + 0.1f);
            button.image.color = new Color(1, 0, 0, button.image.color.a + 0.1f);
            Debug.Log(button.image.color.a);
            yield return new WaitForSeconds(0.2f);
        }
        text.enabled = true;
        button.enabled = true;
    }
    public void RestartLevel()
    {
        Debug.Log("Restarting Level");
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
