using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
public class Gun : MonoBehaviour
{
    public GameObject shotgun;
    private bool paused = false;
    private bool canShoot = true;
    private AudioSource source;
    void Awake()
    {
        source = transform.GetComponent<AudioSource>();

        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
    void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && shotgun.activeSelf && !paused)
        {
            if (canShoot)
            {
                canShoot = false;
                StartCoroutine(fireGun());
                
            }

        }
    }
    IEnumerator fireGun()
    {
        transform.GetComponent<AudioSource>().Play();
        shotgun.GetComponent<Animator>().Play("ShotgunFire");
        if (!paused)
        {
            yield return new WaitForSeconds(0.5f);
            shotgun.GetComponent<Animator>().Play("New State");
        }
        
        canShoot = true;

    }
    private void OnGameStateChanged(GameState newGameState)
    {
       
        paused = !(newGameState == GameState.Gameplay);
        enabled = !paused;
        if (paused)
        {
            source.Pause();
            shotgun.GetComponent<Animator>().speed = 0;

        }
        else
        {
            source.UnPause();
            shotgun.GetComponent<Animator>().speed = 1;
     

        }
    }
}
