using UnityEngine;
using UnityEngine.Audio;
public class Gun : MonoBehaviour
{
    public GameObject shotgun;
    private bool paused = false;

    void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
    void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && shotgun.activeSelf && !paused)
        {
            transform.GetComponent<AudioSource>().Play();

        }
    }
    private void OnGameStateChanged(GameState newGameState)
    {
        AudioSource source = transform.GetComponent<AudioSource>();
        paused = !(newGameState == GameState.Gameplay);

        if (paused)
        {
            source.Pause();
        }
        else
        {
            source.UnPause();
        }
    }
}
