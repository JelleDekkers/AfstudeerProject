using UnityEngine;
using System.Collections;
using AfstudeerProject.Triggers;

public class Rope : MonoBehaviour, IHittable {

    [SerializeField] private TriggerEvent tiedObject;

    public void Hit(Actor actor, Vector3 direction, float force) {
        Break();
    }

    private void Break() {
        tiedObject.ActivateEvent();
        Destroy(gameObject);
    }
}
