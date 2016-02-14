using UnityEngine;

public class EquippedItemManager : MonoBehaviour {

    [SerializeField]
    private EquippedItemHolder[] equippedItemHolders;

    private void Start() {
        equippedItemHolders = GetComponentsInChildren<EquippedItemHolder>();
        Debug.Log(Player.Inventory);
        Player.Inventory.OnEquipmentChangedTo += UpdateEquipment;
    }

    public void UpdateEquipment(ItemType type, ItemData item) {
        foreach (EquippedItemHolder holder in equippedItemHolders) {
            if (holder.Type == type) {
                holder.UpdateHolder(item);
                return;
            }
        }
        Debug.LogWarning("No EquippedItemHolder found with type: " + type + " for item: " + item.Name);
    }
}