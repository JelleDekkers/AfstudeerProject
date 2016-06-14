using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {

	private void OnTriggerEnter(Collider col) {
        if (col.gameObject == Player.Instance.gameObject)
            LevelCompleted();
    }

    private void LevelCompleted() {
        Fade.Instance.FadeOut("Level Finished");
    }
}

