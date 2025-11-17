using System.Collections;
using UnityEngine;

public class ChaseMusic : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip startClip;
    public AudioClip endClip;
    public bool stopPlaying = false;
    private float volume = 1f;

    private void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartPlaying();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            StopPlaying();
        }

        audioSource.volume = volume;
    }

    public void StartPlaying()
    {
        StartCoroutine(Play());
    }

    public void StopPlaying()
    {
        stopPlaying = true;
    }
    
    private IEnumerator Play()
    {
        Debug.Log("Coroutine started");

        volume = 100f;
        audioSource.loop = false;
        audioSource.clip = startClip;
        audioSource.Play();

        yield return new WaitUntil(() => !audioSource.isPlaying);

        Debug.Log("Start clip finished");

        audioSource.clip = endClip;
        audioSource.loop = true;
        audioSource.Play();

        Debug.Log("Waiting for stopPlaying...");

        stopPlaying = false;
        yield return new WaitUntil(() => stopPlaying);

        Debug.Log("stopPlaying was set to TRUE, starting fade...");

        while (volume > 0.05f)
        {
            Debug.Log("Volume now: " + volume);

            volume -= 0.05f;

            yield return null;
        }

        audioSource.Stop();

        stopPlaying = false;
        Debug.Log("Audio stopped.");
    }
}
