using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EquippedItemHolderManager))]
public class EquipItemsOnStart : MonoBehaviour {

    [SerializeField]
    private ItemGameObject[] itemsToEquip;

    private void Start() {
        EquippedItemHolderManager manager = GetComponent<EquippedItemHolderManager>();
        Actor actor = GetComponent<Actor>();
        foreach(ItemGameObject i in itemsToEquip) {
            if (i != null) {
                ItemData item = new ItemData(i.Name, i.Type, i.MeshName, i.Sprite, i.Points);
                actor.Inventory.EquipItem(item);
            }
        }
    }
}
