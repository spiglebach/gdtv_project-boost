using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketCollision : MonoBehaviour {
    [SerializeField] private float levelEndDelay = 1f;

    private bool gameOver = false;
    
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
        // todo display level complete screen
        // todo flash particle effects
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
        // todo display game over screen
        // todo explode rocket
        // todo add sfx
        Invoke("RestartLevel", levelEndDelay);
    }

    private void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
