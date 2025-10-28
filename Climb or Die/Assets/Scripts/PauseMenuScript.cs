using UnityEngine;
using UnityEngine.Audio;
public class PauseMenuScript : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject pauseMenu;
    public AudioMixer audioMixer;
    public static float volume = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResumeGame()
    {
        GameStateManager.Instance.SetState(GameState.Gameplay);
    }
    public void OpenOptionsMenu()
    {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }
    public void ExitOptionsMenu()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
    public void setVolume(float vol)
    {
        audioMixer.SetFloat("volume", vol);
        volume = vol;
        Debug.Log(volume);
    }
    public void setQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
    public void setFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
