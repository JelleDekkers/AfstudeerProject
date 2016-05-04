using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {

    /// <summary>
    /// the direction to open
    /// </summary>
    [SerializeField] private Vector3 direction = Vector3.forward;

	public void OnTriggerEnter(Collider col) {
        if (col.GetComponent<Actor>()) {
            if(transform.parent.GetComponent<Door>() != null && transform.parent.GetComponent<Door>().enabled)
            transform.parent.GetComponent<Door>().DoorTriggered(col.GetComponent<Actor>(), direction);
        }
    }
}
