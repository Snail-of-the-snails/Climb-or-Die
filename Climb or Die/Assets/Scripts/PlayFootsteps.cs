using System.Collections;
using UnityEngine;

public class PlayFootsteps : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    [HideInInspector] public bool playedFootstep;

    void Start() {
        audioSource = transform.GetComponent<AudioSource>();
    }

    public void Footstep()
    {
        StartCoroutine(playSound());
    }

    IEnumerator playSound()
    {
        if (!audioSource.isPlaying && !playedFootstep)
        {
            audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.Play();
        }

        yield return new WaitUntil(() => !audioSource.isPlaying);
    }
}
