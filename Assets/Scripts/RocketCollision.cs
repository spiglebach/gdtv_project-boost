using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketCollision : MonoBehaviour {
    [SerializeField] private float levelEndDelay = 1f;
    
    private void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag) {
            case "Friendly":
                // Do not harm player
                break;
            case "Finish":
                StartCoroutine(LoadNextLevel());
                break;
            default:
                StartCoroutine(RestartLevel());
                break;
        }
    }

    private IEnumerator LoadNextLevel() {
        // todo display level complete screen
        yield return new WaitForSeconds(levelEndDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator RestartLevel() {
        // todo display game over screen
        yield return new WaitForSeconds(levelEndDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
