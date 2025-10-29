using System.Diagnostics;
using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    public GameObject player;
    string objectName;
    bool hitGround;
    GameObject terrain;

    void Start()
    {
        objectName = gameObject.name;
       
    }
    void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }
    void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    void Update()
    {
        terrain = FPSController.terrain;
        hitGround = FPSController.hitGround;
        transform.GetComponent<TerrainCollider>().enabled = hitGround && (terrain.name == objectName);
    }
}
