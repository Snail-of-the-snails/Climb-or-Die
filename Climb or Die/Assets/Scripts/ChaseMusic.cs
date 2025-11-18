using System.Collections;
using UnityEngine;

public class ChaseMusic : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip startClip;
    public AudioClip endClip;
    private bool stopPlaying = false;
    private float volume = 1f;

    private void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
    }

    private void Update() {
        if (Time.timeScale == 0)
        {
            audioSource.volume = volume / 0.5f;
        }
        else
        {
            audioSource.volume = volume;
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
