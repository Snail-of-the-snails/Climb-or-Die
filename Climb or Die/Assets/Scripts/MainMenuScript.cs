using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{
    private Canvas startCanvas;
    private Canvas optionsCanvas;

    public void Start()
    {
        
        startCanvas = GetComponent<Canvas>();
        startCanvas.enabled = true;
        optionsCanvas = GetComponent<Canvas>();
        optionsCanvas.enabled = false;

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
}