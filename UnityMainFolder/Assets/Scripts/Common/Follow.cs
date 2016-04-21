using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	
    [SerializeField] private Transform target;

    private void Update() {
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
    }
}
