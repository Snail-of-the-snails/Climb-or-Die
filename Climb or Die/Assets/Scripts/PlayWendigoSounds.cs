using System.Collections;
using UnityEngine;

public class PlayWendigoSounds : MonoBehaviour
{
    public AudioClip[] audioClips;
    AudioSource audioSource;

    void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
    }

    void Update()
    {
        StartCoroutine(playSound());
    }

    IEnumerator playSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.Play();
        }

        yield return new WaitUntil(() => !audioSource.isPlaying);
    }
}