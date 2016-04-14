using UnityEngine;
using System.Collections;

public static class UnityExtensionMethods {

    public static void LookAtWithoutYAxis(this Transform t, Transform target) {
        Vector3 point = target.position;
        point.y = t.position.y;
        t.LookAt(point);
    }

    public static void SlowLookat(this Transform t, Transform target, float rotateSpeed) {
        var targetRotation = Quaternion.LookRotation(target.transform.position - t.position);
        t.rotation = Quaternion.Slerp(t.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    public static void SlowLookat(this Transform t, Vector3 target, float rotateSpeed) {
        var targetRotation = Quaternion.LookRotation(target - t.position);
        t.rotation = Quaternion.Slerp(t.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}
