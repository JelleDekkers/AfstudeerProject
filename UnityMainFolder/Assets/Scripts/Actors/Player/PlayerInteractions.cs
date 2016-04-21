using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerInteractions : MonoBehaviour {

    private float interactionMaxRange = 4;
    private InteractableObject nearestItem = null;
    private Collider[] nearbyItemColliders;
    private AudioSource audioSource;

    [SerializeField] private LayerMask layerMask;

    public static event Action<InteractableObject> OnNearbyItemSelectable;
    public static event Action OnNoNearbyItemSelectable;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        //TODO: block rays against foundation layer
        nearbyItemColliders = Physics.OverlapSphere(transform.position, interactionMaxRange, layerMask);

        if (nearbyItemColliders.Length > 0) {
            if (nearestItem != GetNearestItem()) {
                nearestItem = GetNearestItem();
                OnNearbyItemSelectable(nearestItem);
            }
            if (Input.GetKeyDown(PlayerInput.InteractButton)) {
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
        //pickup:
        if (item.GetComponent<ItemGameObject>()) 
            PickUpItem(item.GetComponent<ItemGameObject>());
        
       // nearestItem = null;
       // nearestItem = GetNearestItem();
    }

    private void PickUpItem(ItemGameObject item) {
        audioSource.PlayOneShotWithRandomPitch(AudioManager.Instance.PickUpObjectClip, 0.2f);

        ItemGameObject i = item.GetComponent<ItemGameObject>();
        if (i.Type == ItemType.Potion)
            Player.Instance.Potions++;
        else
            Player.Instance.Inventory.AddItem(new ItemData(i.Name, i.Type, i.MeshName, i.Sprite, i.Points, i.WeaponLength, i.AttackAngle));
        Destroy(item.gameObject);
    }

    //private void OnDrawGizmos() {
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, interactionMaxRange);
    //}
}
