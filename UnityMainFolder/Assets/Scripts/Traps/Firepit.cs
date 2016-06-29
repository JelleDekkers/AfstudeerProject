using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Firepit : MonoBehaviour, IHittable {

    [SerializeField] private GameObject firePrefab;

    private GameObject mainFire;
    private float force = 1000;
    private List<GameObject> fires;
    private bool fallen;

    private void Start() {
        mainFire = transform.GetChild(0).gameObject;
    }

    private void Update() {
        //timer voor removen van alle fire
    }

    public IEnumerator DistributeFire(Vector3 colPoint) {
        int fireAmount = 7;
        float delayBetweenfireMin = 0.05f;
        float delayBetweenfireMax = 0.2f;
        float extinquishDelayMin = 4f;
        float extinquishDelayMax = 6f;
        int circleRadius = 2;

        fallen = true;
        fires = new List<GameObject>();
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 circle = colPoint + fwd;
        for (int i = 0; i < fireAmount; i++) {
            GameObject fire = Instantiate(firePrefab) as GameObject;
            Vector3 firePos = colPoint + fwd * i;
            firePos = circle + Random.insideUnitSphere * circleRadius;
            firePos.y = colPoint.y;
            fire.transform.position = firePos;

            fire.GetComponent<Fire>().ExtinquishAfterDelay(Random.Range(extinquishDelayMin, extinquishDelayMax));
            fires.Add(fire);

            float delay = Random.Range(delayBetweenfireMin, delayBetweenfireMax);
            if (i == 0)
                delay = 0;
            yield return new WaitForSeconds(delay);
        }

        Destroy(GetComponent<Firepit>());
    }

    public void Hit(Actor actor, Vector3 direction, float force) {
        if (actor.GetType() != typeof(Player) || fallen)
            return;

        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddTorque(direction * this.force, ForceMode.Impulse);
    }

}
