using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerInteractions : MonoBehaviour {

    //[SerializeField]
    private float interactionMaxRange = 4;
    public InteractableObject nearestItem = null;
    public Collider[] nearbyItemColliders;
    private LayerMask layerMask;

    public static event Action<InteractableObject> OnNearbyItemSelectable;
    public static event Action OnNoNearbyItemSelectable;

    private void Start() {
        // Sets layerMask to InteractableObject layer (14):
        layerMask = 1 << 14;
    }

    private void Update() {
        //TODO: block rays against foundation layer
        nearbyItemColliders = Physics.OverlapSphere(transform.position, interactionMaxRange, layerMask);

        if (nearbyItemColliders.Length > 0) {
            if (nearestItem != GetNearestItem()) {
                nearestItem = GetNearestItem();
                OnNearbyItemSelectable(nearestItem);
            }
            if (Input.GetKeyDown(KeyCode.E)) {
                Interact(nearestItem);
                nearestItem = GetNearestItem();
                OnNoNearbyItemSelectable();
                nearestItem = null;
            }
        } else {
            if (nearestItem != null) {
                OnNoNearbyItemSelectable();
                nearestItem = null;
            }
        }
    }

    private InteractableObject GetNearestItem() {
        if (nearbyItemColliders.Length == 0) {
            OnNoNearbyItemSelectable();
            return null;
        }
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
        item.Interact();
        if (item.GetComponent<ItemGameObject>()) {
            ItemGameObject i = item.GetComponent<ItemGameObject>();
            Player.Inventory.AddItem(new ItemData(i.Name, i.Type, i.MeshName, i.Sprite, i.Points));
            Destroy(item.gameObject);
        }
       // nearestItem = null;
       // nearestItem = GetNearestItem();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactionMaxRange);
    }
}
