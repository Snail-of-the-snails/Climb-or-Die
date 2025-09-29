using System.Collections;
using UnityEngine;

public class PlayFootsteps : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    void Start() {
        audioSource = transform.GetComponent<AudioSource>();
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
