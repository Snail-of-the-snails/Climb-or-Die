using UnityEngine;

public class CanvasWake : MonoBehaviour
{
    public Canvas canvas; // Reference to the Canvas component
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //canvas.getComponent<CanvasGroup>().alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //canvas.getComponent<CanvasGroup>().alpha += 0.05;
    }
}
