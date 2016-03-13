using UnityEngine;
using System.Collections;

public class TimeController {

    public static IEnumerator SlowTimeForSeconds(float timeScale, float seconds) {
        Debug.Log("called");
        Time.timeScale = timeScale;
        yield return new WaitForSeconds(seconds);
        while (Time.timeScale < 1) {
            Time.timeScale += 0.1f;
        }
        Time.timeScale = 1;
    }
}
