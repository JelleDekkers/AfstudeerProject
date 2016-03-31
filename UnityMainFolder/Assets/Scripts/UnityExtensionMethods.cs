using UnityEngine;
using System.Collections;

public static class UnityExtensionMethods {

    public static void LookAtWithoutYAxis(this Transform t, Transform target) {
        Vector3 point = target.position;
        point.y = t.position.y;
        t.LookAt(point);
    }
}
