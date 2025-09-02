using UnityEngine;
using UnityEngine.UI;

public class CanvasWake : MonoBehaviour
{
    public Canvas canvas; // Reference to the Canvas component
    public float alpha;
    public CanvasGroup canvasGroup; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        alpha = canvasGroup.alpha;
    }

    // Update is called once per frame
    void Update()
    {  
        if(alpha < 1)
        {
            alpha += Time.deltaTime*0.5f; // Increase alpha over time
        }
        canvasGroup.alpha = alpha;
        canvasGroup.interactable = false;
    }
}
