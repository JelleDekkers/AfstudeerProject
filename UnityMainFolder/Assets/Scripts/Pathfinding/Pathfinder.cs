using UnityEngine;
using System.Collections;

public class Pathfinder : MonoBehaviour {

    public GameObject target;
    public float fovAngle;
    public float speed;
    public float rayLength = 10;

    private Vector3 movement;


    void Update() {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit, rayLength)) {
            //if(hit.collider.gameObject.layer == target.layer) {
               
            //}
        }
        MoveToTarget(target);
        Debug.DrawRay(transform.position, fwd * rayLength, Color.red);
    }

    private void MoveToTarget(GameObject target) {
        transform.LookAt(target.transform);
        //rigidbody.AddRelativeForce(Vector3.forward * speed, ForceMode.Force);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
}
