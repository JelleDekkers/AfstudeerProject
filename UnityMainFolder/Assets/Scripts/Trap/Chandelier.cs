using UnityEngine;
using System.Collections.Generic;

public class Chandelier : MonoBehaviour, ITrap {

    [SerializeField] private float playerDamage;
    [SerializeField] private float aIDamage;

    private List<Collider> triggers = new List<Collider>();

    private void Start() {
        foreach (Collider col in GetComponents<Collider>()) {
            if (col.isTrigger) {
                triggers.Add(col);
                col.enabled = false;
            }
        }
    }

    public void OnTriggered(Actor actor) {
        if (actor.GetType() == typeof(Player))
            actor.TakeDamage(playerDamage, gameObject);
        else
            actor.TakeDamage(aIDamage, gameObject);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            Fall();
        }
    }

    public void Fall() {
        foreach(Collider col in triggers) {
            col.enabled = true;
        }
        Rigidbody rBody = GetComponent<Rigidbody>();
        rBody.useGravity = true;
        rBody.constraints = RigidbodyConstraints.None;
    }
}
