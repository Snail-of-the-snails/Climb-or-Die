using UnityEngine;
using System.Collections;

public class SpawnSounds : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClips;

    void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
    }

    public void PlaySpawnSound()
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
    }

    
}
