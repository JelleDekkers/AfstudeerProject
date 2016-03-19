using UnityEngine;

public class DestructableObject : MonoBehaviour, IHittable {

    [SerializeField] private float breakingThreshold = 1;

    private bool isBroken;
    private GameObject brokenObject;

    private void Start() {
        brokenObject = transform.GetChild(0).gameObject;
    }

    //direction? force?
    private void OnCollisionEnter(Collision col) {
        if (col.relativeVelocity.magnitude > breakingThreshold)
            Break();
    }

    private void Break() {
        if (isBroken)
            return;

        isBroken = true;
        //play sound
        brokenObject.SetActive(true);
        brokenObject.transform.parent = null;
        Destroy(gameObject);
    }

    private void Break(Vector3 forceDirection, float forceAmount) {
        if (isBroken)
            return;

        isBroken = true;
        //play sound
        brokenObject.SetActive(true);

        foreach (Rigidbody r in brokenObject.GetComponentsInChildren<Rigidbody>()) {
            Vector3 direction = new Vector3(forceDirection.x + Random.Range(-0.5f, 0.5f), 
                                            forceDirection.y + Random.Range(-0.5f, 0.5f), 
                                            forceDirection.z + Random.Range(-0.5f, 0.5f));
            r.AddForce(direction * forceAmount, ForceMode.Impulse);
        }

        brokenObject.transform.parent = null;
        Destroy(gameObject);
    }

    public void Hit(Actor actor, Vector3 direction, float force) {
        if (force > breakingThreshold)
            Break(direction, force);
    }
}
