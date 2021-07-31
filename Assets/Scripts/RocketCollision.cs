using System;
using System.Collections;
using UnityEngine;

public class RocketCollision : MonoBehaviour {
    private AudioSource _audioSource;
    private LevelLoader _levelLoader;
    
    [SerializeField] private float levelEndDelay = 1f;
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private AudioClip levelCompleteClip;
    
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private ParticleSystem levelCompleteParticles;
    
    private bool gameOver;
    private bool detectCollisions = true;

    private void Start() {
        _audioSource = GetComponent<AudioSource>();
        _levelLoader = FindObjectOfType<LevelLoader>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            detectCollisions = !detectCollisions;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (gameOver || !detectCollisions) return;
        switch (other.gameObject.tag) {
            case "Friendly":
                // Do not harm player
                break;
            case "Finish":
                LevelComplete();
                break;
            default:
                ExplodeRocket();
                break;
        }
    }

    private void LevelComplete() {
        gameOver = true;
        GetComponent<Movement>().DisableMovement();
        StopAudioAndPlayOneShot(levelCompleteClip);
        // todo display level complete screen
        levelCompleteParticles.Play();
        Invoke("LoadNextLevel", levelEndDelay);
    }

    private void LoadNextLevel() {
        _levelLoader.LoadNextLevel();
    }


    private void ExplodeRocket() {
        gameOver = true;
        GetComponent<Movement>().DisableMovement();
        StopAudioAndPlayOneShot(explosionClip);
        // todo display game over screen
        explosionParticles.Play();
        Invoke("RestartLevel", levelEndDelay);
    }

    private void RestartLevel() {
        _levelLoader.RestartLevel();
    }

    private void StopAudioAndPlayOneShot(AudioClip clip) {
        _audioSource.Stop();
        _audioSource.PlayOneShot(clip);
    }
}
