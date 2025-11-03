using UnityEngine;

public class SmokeParticlesLogic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public ParticleSystem smokeParticles;
    public float animLength;
    public bool isPlaying;
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
        if (isPlaying)
        {
            smokeParticles.Play();
        }
        else
        {
            smokeParticles.Stop();
        } 
    }
    private void OnGameStateChanged(GameState newGameState)
    {
        if(newGameState != GameState.Gameplay)
        {
            smokeParticles.Pause();
            

        }



        enabled = newGameState == GameState.Gameplay;
    }
}
