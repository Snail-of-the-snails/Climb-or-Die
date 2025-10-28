using UnityEngine;
using UnityEngine.Audio;
public class Gun : MonoBehaviour
{
    public GameObject shotgun;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
    void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && shotgun.activeSelf)
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
