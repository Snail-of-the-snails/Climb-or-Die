using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Canvas startCanvas;
    [SerializeField] private Canvas optionsCanvas;
    [SerializeField] private Canvas creditsCanvas;
    public AudioMixer audioMixer;
    public static float volume;

    public void Start()
    {

        startCanvas.enabled = true;
        optionsCanvas.enabled = false;
        creditsCanvas.enabled = false;


    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // Add logic to start the game, e.g., load the first level
    }
    public void QuitGame()
    {
        Application.Quit();
        // Add logic to quit the game, e.g., close the application
    }
    public void LoadOptions()
    {
        optionsCanvas.enabled = true;
        startCanvas.enabled = false;

        // Add logic to load the settings menu, e.g., open a settings panel


    }
    public void setVolume(float vol)
    {
        audioMixer.SetFloat("volume", vol);
        volume = vol;
    }
    public void setQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
    public void setFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void OptionsBack()
    {
        optionsCanvas.enabled = false;
        startCanvas.enabled = true;
        creditsCanvas.enabled = false;

        // Add logic to load the settings menu, e.g., open a settings panel


    }
    public void creditsPressed()
    {
        optionsCanvas.enabled = false;
        startCanvas.enabled = false;
        creditsCanvas.enabled = true;

        // Add logic to load the settings menu, e.g., open a settings panel
    }
}