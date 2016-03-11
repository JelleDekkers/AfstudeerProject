using UnityEngine;
using System.Collections;

public class Common : MonoBehaviour {

	public static Vector3 GetDirection(Vector3 firstPos, Vector3 secondPos) {
        Vector3 heading = secondPos - firstPos;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;
        return direction;
    }
}
