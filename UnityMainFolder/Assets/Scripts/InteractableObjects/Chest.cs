using UnityEngine;
using System.Collections;

public class Chest : InteractableObject {

    private ItemGameObject itemInChest;
    private bool isOpen;

    private void Start() {
        Transform itemParent = transform.FindChild("ItemInChestParent");
        if (itemParent.childCount == 0)
            return;

        itemInChest = transform.FindChild("ItemInChestParent").GetChild(0).GetComponent<ItemGameObject>();
        itemInChest.gameObject.SetActive(false);
        itemInChest.transform.position = transform.position;
        Destroy(itemInChest.GetComponent<Rigidbody>());
        itemInChest.gameObject.layer = 0;
        itemInChest.GetComponent<Collider>().isTrigger = true;
        itemInChest.GetComponent<Collider>().enabled = false;
    }

    public override void Interact() {
        if (!isOpen)
            OpenChest();
    }

    private void OpenChest() {
        GetComponent<Animator>().SetTrigger("Open");
        gameObject.layer = 0;

        if (itemInChest != null)
            itemInChest.gameObject.SetActive(true);
    }

    public void MakeObjectPickable() {
        if (itemInChest == null)
            return;

        itemInChest.gameObject.layer = Layers.INTERACTABLE_OBJECT_LAYER;
        itemInChest.GetComponent<Collider>().enabled = true;
    }
}
