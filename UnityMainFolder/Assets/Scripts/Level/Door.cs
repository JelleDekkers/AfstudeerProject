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
        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            if (direction.x < 0) {
                leftDoor.transform.Rotate(Vector3.forward, speed * Time.deltaTime);
                rightDoor.transform.Rotate(-Vector3.forward, speed * Time.deltaTime);
            } else {
                leftDoor.transform.Rotate(-Vector3.forward, speed * Time.deltaTime);
                rightDoor.transform.Rotate(Vector3.forward, speed * Time.deltaTime);
            }
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
