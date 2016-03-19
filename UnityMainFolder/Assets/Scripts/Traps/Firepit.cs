using UnityEngine;

public class Firepit : MonoBehaviour, ITrap, IHittable {

    [SerializeField] private float playerDamage;
    [SerializeField] private float aiDamage;

    private GameObject mainFire;
    private float force = 1000;

    private void Start() {
        mainFire = transform.GetChild(0).gameObject;
    }

    public void OnTriggered(Actor actor) {
        //spawn fire particles on ground in front
    }

    public void Hit(Actor actor, Vector3 direction, float force) {
        if (actor.GetType() != typeof(Player))
            return;

        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddTorque(direction * this.force, ForceMode.Impulse);
    }
}
