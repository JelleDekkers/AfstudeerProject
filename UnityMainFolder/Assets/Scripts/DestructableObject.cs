using UnityEngine;
using System;
using System.Collections;

public class DestructableObject : MonoBehaviour {

    [SerializeField] private float breakingThreshold = 1;

    private bool isBroken;
    private GameObject completeObject;
    private GameObject brokenObject;

    private void Start() {
        try {
            completeObject = transform.GetChild(0).gameObject;
            brokenObject = transform.GetChild(1).gameObject;
        } catch {
            Debug.LogWarning("No child items found: " + gameObject.name);
        }
    }

    //direction? force?
    public void OnCollisionDetected(float force) {
        if (force > breakingThreshold)
            Break();
    }

    private void Break() {
        if (isBroken)
            return;

        isBroken = true;
        //play sound
        Destroy(completeObject);
        brokenObject.SetActive(true);
    }
}
