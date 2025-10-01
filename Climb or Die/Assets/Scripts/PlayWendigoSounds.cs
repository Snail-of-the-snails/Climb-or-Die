using System.Collections;
using UnityEngine;

public class PlayWendigoSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private GameObject player;
    private AudioSource audioSource;
    [SerializeField] private int playedSound = 0;


    void Start()
    {
        playedSound = 0;
        audioSource = transform.GetComponent<AudioSource>();
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= 25 && playedSound == 0)
        {
            playedSound = 1;
            StartCoroutine(playSound());
        }
        else if (Vector3.Distance(player.transform.position, transform.position) > 25 && playedSound == 1)
        {
            playedSound = 0;
        }
        if (Time.deltaTime == 0)
        {
            audioSource.Pause();
        }
        else if(Time.deltaTime > 0 && !audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
    }

    IEnumerator playSound()
    {
        if (!audioSource.isPlaying && Time.deltaTime >0)
        {
            audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.Play();
        }

        yield return new WaitUntil(() => !audioSource.isPlaying);
    }
}