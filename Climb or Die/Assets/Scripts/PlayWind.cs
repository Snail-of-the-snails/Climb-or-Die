using UnityEngine;

public class PlayWind : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if(!transform.GetComponent<AudioSource>().isPlaying)
        {
            transform.GetComponent<AudioSource>().Play();
        }
    }
    private void OnGameStateChanged(GameState newGameState)
    {    
        transform.GetComponent<AudioSource>().Pause();
        enabled = newGameState == GameState.Gameplay;
    }


}
