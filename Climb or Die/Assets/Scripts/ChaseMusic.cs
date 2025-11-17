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
    
    public IEnumerator Play()
    {
        volume = 1f;
        audioSource.loop = false;
        audioSource.clip = startClip;
        audioSource.Play();

        yield return new WaitUntil(() => !audioSource.isPlaying);

        audioSource.clip = endClip;
        audioSource.loop = true;
        audioSource.Play();

        stopPlaying = false;
        yield return new WaitUntil(() => stopPlaying);

        while (volume > 0.001f)
        {
            volume -= 0.001f;

            yield return null;
        }

        volume = 0f;

        audioSource.Stop();

        stopPlaying = false;
    }
}
