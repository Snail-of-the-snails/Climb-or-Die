using UnityEngine;

public class PauseController : MonoBehaviour
{
    public GameObject OptionsMenu;
    public GameObject PauseMenu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) )
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