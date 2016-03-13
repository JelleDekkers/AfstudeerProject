using UnityEngine;
using System.Collections;

public class CameraTarget : MonoBehaviour {

    [SerializeField] private Transform target;
    
	private void FixedUpdate() {
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
	}
}
