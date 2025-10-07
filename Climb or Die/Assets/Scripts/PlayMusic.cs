using System.Collections;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClips;

    void Start() {
        audioSource = transform.GetComponent<AudioSource>();
    }

    public void Update() 
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
