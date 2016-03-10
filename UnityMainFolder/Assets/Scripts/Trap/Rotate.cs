using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector3 rotationDirection;

    private void Update() {
        transform.Rotate(rotationDirection * Time.deltaTime * rotationSpeed);
    }
}
