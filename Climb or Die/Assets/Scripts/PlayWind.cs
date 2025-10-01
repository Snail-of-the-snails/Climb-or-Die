using UnityEngine;

public class PlayWind : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.deltaTime > 0 && !transform.GetComponent<AudioSource>().isPlaying)
        {
            transform.GetComponent<AudioSource>().Play();
        }
        else if(Time.deltaTime == 0)
        {
            transform.GetComponent<AudioSource>().Pause();
        }
    }
}
