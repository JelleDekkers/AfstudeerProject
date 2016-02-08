using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInteractions : MonoBehaviour {

    //[SerializeField]
    private float interactionMaxRange = 4;
    public InteractableObject nearestItem = null;
    public Collider[] nearbyItemColliders;
    private RaycastHit[] hits;
    private LayerMask layerMask;
    private float curItemDistance = 0;
    private Inventory playerInventory;

    private void Start() {
        // Set layerMask to InteractableObject layer
        layerMask = 1 << 14;
        playerInventory = GetComponent<Actor>().Inventory;
    }

    private void Update() {
        //TODO: block rays against foundation layer
        hits = Physics.RaycastAll(transform.position, transform.forward, 100.0F);
        nearbyItemColliders = Physics.OverlapSphere(transform.position, interactionMaxRange, layerMask);

        if (nearbyItemColliders.Length > 0) {
            nearestItem = GetNearestItem();
            //Highlight nearestItem
            if (Input.GetKeyDown(KeyCode.E)) {
                Interact(nearestItem);
            }
        } else {
            nearestItem = null;
        }
    }

    private InteractableObject GetNearestItem() {
        InteractableObject nearestItem = null;
        float closestDistanceSqr = Mathf.Infinity;
        foreach (Collider col in nearbyItemColliders) {
            Vector3 directionToTarget = col.gameObject.transform.position - transform.position;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr) {
                closestDistanceSqr = dSqrToTarget;
                nearestItem = col.GetComponent<InteractableObject>();
            }
        }
        return nearestItem;
    }

    private void Interact(InteractableObject item) {
        Debug.Log("Interacting with: " + item);
        item.InteractWith();

        Debug.Log(item.GetType());
        if (item.GetComponent<Item>()) {
            playerInventory.Add(item.GetComponent<Item>());
            Destroy(item.gameObject);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactionMaxRange);
    }

    private void OnGUI() {
        if (nearestItem == null)
            return;

        GUI.Label(new Rect(Screen.width / 2 - 30, Screen.height - 30, 1000, 20), nearestItem.Name);
    }
}
