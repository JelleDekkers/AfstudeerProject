using UnityEngine;
using System.Collections;

public class RagdollController : MonoBehaviour {

    private Rigidbody[] rigidbodies;

    private void Start() {
        rigidbodies = transform.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in rigidbodies) {
            if (rb.transform != transform) {
                rb.isKinematic = true;
                rb.GetComponent<Collider>().enabled = false;
            }
        }
    }

    public void ActivateRagDoll() {
        GetComponent<Animator>().enabled = false;
        //GetComponent<Animator>().SetTrigger("Death");
        int rndIndex = Random.Range(0, rigidbodies.Length);
        transform.GetComponent<CapsuleCollider>().enabled = false;
        foreach (Rigidbody rb in rigidbodies) {
            if (rb.transform != transform) {
                rb.isKinematic = false;
                rb.GetComponent<Collider>().enabled = true;
            }
        }
    }

    public void ActivateRagDoll(Vector3 forceDirection, float forceAmount) {
        ActivateRagDoll();
        AddForceRandomRigidbody(forceDirection, forceAmount);
    }

    private void AddForceRandomRigidbody(Vector3 forceDirection, float forceAmount) {
        forceAmount = Random.Range(forceAmount - 10, forceAmount + 10);
        float forceY = Random.Range(0, 1);
        forceDirection = new Vector3(forceDirection.x, forceY, forceDirection.z);
        int rndIndex = Random.Range(0, rigidbodies.Length);
        rigidbodies[rndIndex].AddForce(forceDirection * forceAmount, ForceMode.Impulse);
    }
}
