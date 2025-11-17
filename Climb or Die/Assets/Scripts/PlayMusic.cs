using System.Collections;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClips;
    private bool isPlaying = true;
    private float volume = 1f;

    void Start() {
        audioSource = transform.GetComponent<AudioSource>();
    }

    public void Update() 
    {
        if (isPlaying == true) {
            StartCoroutine(PlaySound());
        }

        if (Time.timeScale == 0)
        {
            audioSource.volume = volume / 0.5f;
        }
        else
        {
            audioSource.volume = volume;
        }
    }

    IEnumerator PlaySound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.Play();
        }

        yield return new WaitUntil(() => !audioSource.isPlaying);
    }

    public IEnumerator StopMusic()
    {
        isPlaying = false;

        while (volume > 0.001f)
        {
            volume -= 0.001f;

            yield return null;
        }

        audioSource.Stop();
    }

    public void StartMusic()
    {
        isPlaying = true;
        volume = 1f;
        StartCoroutine(PlaySound());
    }
}
