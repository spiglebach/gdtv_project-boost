using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketCollision : MonoBehaviour {
    private AudioSource _audioSource;
    
    [SerializeField] private float levelEndDelay = 1f;
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private AudioClip levelCompleteClip;
    
    private bool gameOver;

    private void Start() {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) {
        if (gameOver) return;
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
        StopAudioAndPlayOneShot(levelCompleteClip);
        // todo display level complete screen
        // todo flash particle effects
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelEndDelay);
    }

    private void LoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ExplodeRocket() {
        gameOver = true;
        StopAudioAndPlayOneShot(explosionClip);
        // todo display game over screen
        // todo explode rocket
        GetComponent<Movement>().enabled = false;
        Invoke("RestartLevel", levelEndDelay);
    }

    private void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void StopAudioAndPlayOneShot(AudioClip clip) {
        _audioSource.Stop();
        _audioSource.PlayOneShot(clip);
    }
}
