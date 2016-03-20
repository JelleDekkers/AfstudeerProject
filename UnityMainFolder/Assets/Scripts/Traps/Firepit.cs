using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Firepit : MonoBehaviour, IHittable {

    [SerializeField] private GameObject firePrefab;

    private GameObject mainFire;
    private float force = 1000;
    private List<GameObject> fires;

    private void Start() {
        mainFire = transform.GetChild(0).gameObject;
    }

    private void Update() {
        //timer voor removen van alle fire
    }

    public IEnumerator DistributeFire(Vector3 colPoint) {
        int fireAmount = 5;
        float distance = 1;
        float rndHorizontalDistance = 2;
        float delayBetweenfireMin = 0.05f;
        float delayBetweenfireMax = 0.2f;

        fires = new List<GameObject>();
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        for (int i = 0; i < fireAmount; i++) {
            GameObject fire = Instantiate(firePrefab) as GameObject;
            Vector3 firePos = colPoint + fwd * i;
            //firePos = new Vector3(firePos.x + Random.Range(-2, 2), firePos.y, firePos.z + Random.Range(-2, 2));
            fire.transform.position = firePos;

            fires.Add(fire);

            float delay = Random.Range(delayBetweenfireMin, delayBetweenfireMax);
            if (i == 0)
                delay = 0;
            yield return new WaitForSeconds(delay);
        }
    }

    public void Hit(Actor actor, Vector3 direction, float force) {
        if (actor.GetType() != typeof(Player))
            return;

        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddTorque(direction * this.force, ForceMode.Impulse);
    }
}
