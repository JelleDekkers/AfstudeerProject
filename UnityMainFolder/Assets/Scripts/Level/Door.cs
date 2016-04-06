using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    private bool isOpen;
    private GameObject leftDoor, rightDoor;
    private float speed = 250;
    private float duration = 0.4f;

    private void Start() {
        leftDoor = transform.FindChild("LeftHinge").gameObject;
        rightDoor = transform.FindChild("RightHinge").gameObject;
        speed = Random.Range(speed - 20, speed + 20);
        duration = Random.Range(duration - 0.1f, duration + 0.1f);
    }

    private void OnTriggerEnter(Collider col) {
        if (!isOpen) {
            if (col.GetComponent<Actor>()) {
                Vector3 dir = (transform.position - col.transform.position).normalized;
                StartCoroutine(Open(dir));
                isOpen = true;
            }
        }
    }

    private IEnumerator Open(Vector3 direction) {
        float elapsed = 0f;
        Vector3 leftDoorDirection = Vector3.forward;
        Vector3 rightDoorDirection = -Vector3.forward;

        if (direction.x < 0 && Mathf.Abs(direction.x) > Mathf.Abs(direction.z)) {
            leftDoorDirection = Vector3.forward;
            rightDoorDirection = -Vector3.forward;
        } else if (direction.x > 0 && Mathf.Abs(direction.x) > Mathf.Abs(direction.z)) {
            leftDoorDirection = -Vector3.forward;
            rightDoorDirection = Vector3.forward;
        } else if (direction.z < 0 && Mathf.Abs(direction.z) > Mathf.Abs(direction.x)) {
            leftDoorDirection = Vector3.forward;
            rightDoorDirection = -Vector3.forward;
        } else if (direction.z > 0 && Mathf.Abs(direction.z) > Mathf.Abs(direction.x)) {
            leftDoorDirection = -Vector3.forward;
            rightDoorDirection = Vector3.forward;
        }

        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            leftDoor.transform.Rotate(leftDoorDirection, speed * Time.deltaTime);
            rightDoor.transform.Rotate(rightDoorDirection, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
