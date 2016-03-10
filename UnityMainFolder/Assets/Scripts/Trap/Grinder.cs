using UnityEngine;
using System.Collections;

public class Grinder : Trap {

    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector3 rotationDirection;
    [SerializeField] private GameObject spinner;

    public bool IsBroken { get; private set; }

    private void Start() {
        IsBroken = false;
    }

    private void Update() {
        if (IsBroken)
            return;

        spinner.transform.Rotate(rotationDirection * Time.deltaTime * rotationSpeed);
    }
}
