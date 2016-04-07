using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {

    [SerializeField] private Vector3 direction = Vector3.forward;

	public void OnTriggerEnter(Collider col) {
        if (col.GetComponent<Actor>()) {
            transform.parent.GetComponent<Door>().OpenDoor(direction);
        }
    }
}
