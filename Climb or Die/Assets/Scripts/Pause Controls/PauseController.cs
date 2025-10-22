using UnityEngine;

public class PauseController : MonoBehaviour
{
    public GameObject OptionsMenu;
    public GameObject PauseMenu;
    public static bool isDead = false;
    void Update()
    {
        isDead = KillPlayer.isDead;
        if(!isDead)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (OptionsMenu.activeSelf == false)
                {
                    GameState currentGameState = GameStateManager.Instance.CurrentGameState;
                    GameState newGameState = currentGameState == GameState.Gameplay
                        ? GameState.Paused
                        : GameState.Gameplay;

                    GameStateManager.Instance.SetState(newGameState);
                }
                else
                {
                    OptionsMenu.SetActive(false);
                    PauseMenu.SetActive(true);
                }

            }
        }

    }
}