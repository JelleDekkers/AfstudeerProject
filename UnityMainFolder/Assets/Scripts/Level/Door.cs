using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    private bool isOpen;
    private GameObject leftDoor, rightDoor;
    private Vector3 openRotation;
    private float speed = 200;
    private float duration = 0.7f;
    private Vector3 targetAngle = new Vector3(0, 190, 0);
    private Animator animator;

    private void Start() {
        leftDoor = transform.FindChild("LeftHinge").gameObject;
        rightDoor = transform.FindChild("RightHinge").gameObject;
        //animator = GetComponent<Animator>();
        //animator.speed = Random.Range(0.8f, 1.2f);
        speed = Random.Range(speed - 20, speed + 20);
        duration = Random.Range(duration - 0.1f, duration + 0.1f);
    }

    private void OnTriggerEnter(Collider col) {
        if(col.GetComponent<Actor>()) {
            Vector3 dir = (transform.position - col.transform.position).normalized;
            if (!isOpen) {
                StartCoroutine(Open(dir));
                isOpen = true;
            }
            //if (!isOpen) {
            //    animator.SetTrigger("Open");
            //    isOpen = true;
            //}
        }
    }

    private IEnumerator Open(Vector3 direction) {
        float elapsed = 0f;
        Vector3 dir = direction;
        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            leftDoor.transform.Rotate(-Vector3.up, speed * Time.deltaTime);
            rightDoor.transform.Rotate(Vector3.up, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
