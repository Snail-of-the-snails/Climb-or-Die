using UnityEngine;

public class FlashlightSway : MonoBehaviour
{
    private Vector3 vectorOffset;
    public GameObject playerCamera;
    [SerializeField] private float speed = 3.0f;
    private bool doSway = true;

    void Start()
    {
        vectorOffset = transform.transform.position - playerCamera.transform.position;

        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    void Update()
    {
        if (doSway)
        {
            transform.position = playerCamera.transform.position + vectorOffset;
            transform.rotation = Quaternion.Slerp(transform.transform.rotation, playerCamera.transform.rotation, speed * Time.deltaTime);
        }
    }
    private void OnGameStateChanged(GameState newGameState)
    {
        doSway = newGameState == GameState.Gameplay;
    }
    void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }
}
