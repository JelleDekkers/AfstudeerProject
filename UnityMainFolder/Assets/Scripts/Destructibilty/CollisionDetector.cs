using UnityEngine;
using System.Collections;

public class CollisionDetector : MonoBehaviour {

	private void OnCollisionEnter(Collision col) {
        //Debug.Log("OncollisionEnter + magn: " + col.relativeVelocity.magnitude);
        transform.GetComponentInParent<DestructableObject>().OnCollisionDetected(col.relativeVelocity.magnitude);
    }
}
