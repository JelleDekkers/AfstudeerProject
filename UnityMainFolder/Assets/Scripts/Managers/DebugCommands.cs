using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugCommands : MonoBehaviour {

    private const KeyCode RESTART_GAME = KeyCode.F1;

	void Update () {
	    if(Input.GetKeyDown(RESTART_GAME)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
	}
}
