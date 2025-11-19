using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChaseVignette : MonoBehaviour
{
    public Image vignette;

    public IEnumerator FadeScreenIn()
    {
        while (vignette.color.a < 1f)
        {
            vignette.color = new Color(1, 1f, 1f, vignette.color.a + 0.01f);
            yield return null;
        }
    }

    public IEnumerator FadeScreenOut()
    {
        while (vignette.color.a > 0f)
        {
            vignette.color = new Color(1, 1f, 1f, vignette.color.a - 0.01f);
            yield return null;
        }
    }
}
