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
    private Animator animator;

    private AnimatorStateInfo stateInfo;
    void Awake()
    {
        source = transform.GetComponent<AudioSource>();
        animator = shotgun.GetComponent<Animator>();
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
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
                if (stateInfo.IsName("New State"))
                {
                    source.Play();
                    animator.Play("ShotgunFire");

                }
                else
                {
                    canShoot = true;
                    animator.Play("New State");
                }


            }
        }
    }
    
    private void OnGameStateChanged(GameState newGameState)
    {
    
        paused = !(newGameState == GameState.Gameplay);
        enabled = !paused;
        if (paused)
        {
            source.Pause();
            animator.speed = 0;

        }
        else
        {
            source.UnPause();
            animator.speed = 1;
    

        }
    }
}
