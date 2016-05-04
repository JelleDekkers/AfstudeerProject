using UnityEngine;
using System.Collections;

public class Common : MonoBehaviour {

	public static Vector3 GetDirection(Vector3 firstPos, Vector3 secondPos) {
        Vector3 heading = secondPos - firstPos;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;
        return direction;
    }

    public static void SetLayerRecursively(int layerInt, Transform trans) {
        trans.gameObject.layer = layerInt;
        foreach (Transform child in trans) {
            child.gameObject.layer = layerInt;
            if (child.childCount > 0) {
                SetLayerRecursively(layerInt, child.transform);
            }
        }
    }

    public static Vector3 GetPosInRandomCircle(Vector3 center, float radius) {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
}
