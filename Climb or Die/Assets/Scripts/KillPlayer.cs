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
    public float volume = MainMenuScript.volume;
    public static bool isDead = false;
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        button.enabled = false;
        deathScreen.SetActive(false);
        text.enabled = false;
        button.interactable = false;
        audioMixer.SetFloat("volume",volume);
        Debug.Log(volume);
        optionsMenu.SetActive(false);
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
        GameStateManager.Instance.SetState(GameState.Paused);
        isDead = true;
        jeff.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        deathScreen.SetActive(true);
        crosshairAndStamina.SetActive(false);
        audioMixer.SetFloat("volume", -80);
        button.image.color = new Color(1, 0, 0, 0);
        text.color = new Color(.196f, .196f, .196f, 0f);
        text.enabled = true;
        button.enabled = true;
        
        StartCoroutine(fadeButton());
        
    }
    IEnumerator fadeButton()
    {
        yield return new WaitForSeconds(1);
        while (button.image.color.a < 1)
        {
            text.color = new Color(.196f, .196f, .196f, button.image.color.a + 0.1f);
            button.image.color = new Color(1, 0, 0, button.image.color.a + 0.1f);
            yield return new WaitForSeconds(0.1f);
            
        }
        button.interactable = true;

    }
    public void RestartLevel()
    {
        isDead = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

    }
    public void OptionsMenu()
    {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }
}
