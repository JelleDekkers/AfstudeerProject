using UnityEngine;
using System.Collections;

public class TestActor : MonoBehaviour {

    public float fovAngle;
    public float speed;

    private Seeker seeker;

    [SerializeField]
    private GameObject target;

    private void Start() {
        seeker = new Seeker();
    }

    private void MoveToTarget(GameObject target) {
        transform.LookAt(target.transform);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
}
