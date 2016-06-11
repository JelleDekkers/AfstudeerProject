using UnityEngine;
using System;

public class PlayerInteractions : MonoBehaviour {

    private float interactionMaxRange = 4;
    private GameObject nearestInteractableItem = null;
    private Collider[] nearbyItemColliders;
    private AudioSource audioSource;

    [SerializeField] private LayerMask layerMask;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        //TODO: block rays against foundation layer
        nearbyItemColliders = Physics.OverlapSphere(transform.position, interactionMaxRange, layerMask);

        if (nearbyItemColliders.Length > 0) {
            if (nearestInteractableItem != GetNearestItem()) {
                OnNearestItemChanged(nearestInteractableItem, GetNearestItem());
                nearestInteractableItem = GetNearestItem();
            }
            if (Input.GetKeyDown(PlayerInput.InteractButton)) {
                Interact(nearestInteractableItem);
                //nearestInteractableItem = GetNearestItem();
                //nearestInteractableItem = null;
            }
        } else {
            if (nearestInteractableItem != null) {
                OnNearestItemChanged(nearestInteractableItem, null);
                nearestInteractableItem = null;
            }
        }
    }

    private GameObject GetNearestItem() {
        if (nearbyItemColliders.Length == 0) {
            return null;
        }
        GameObject nearestItem = null;
        float closestDistanceSqr = Mathf.Infinity;
        foreach (Collider col in nearbyItemColliders) {
            Vector3 directionToTarget = col.gameObject.transform.position - transform.position;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr) {
                closestDistanceSqr = dSqrToTarget;
                nearestItem = col.gameObject;
            }
        }
        return nearestItem;
    }

    private void Interact(GameObject item) {
        item.GetComponent<IInteractable>().Interact();

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

    private void OnNearestItemChanged(GameObject from, GameObject to) {
        if (from != null)
            OutlineMaterialManager.ChangeMatsToNormalColor(from);
        if (to != null)
            OutlineMaterialManager.ChangeMatsToSelectionColor(to);
    }
}
