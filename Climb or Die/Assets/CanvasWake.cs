using UnityEngine;
using UnityEngine.UI;

public class CanvasWake : MonoBehaviour
{
    public Canvas canvas; // Reference to the Canvas component
    public CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    public void UpdateCanvasGroup()
    {
        if (canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += 0.01f;
        }
    }
}
