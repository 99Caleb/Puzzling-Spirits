using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string sceneToLoad;

    private bool _isPlayerNear = false;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            _isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            _isPlayerNear = false;
        }
    }

    private void Update()
    {
        if (_isPlayerNear && InputManager.Instance.interactPressed)
        {
            // Load the new scene when the player presses 'E'
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}