using UnityEngine;

public class EquippedItemHolderManager : MonoBehaviour {

    [SerializeField]
    private EquippedItemHolder[] equippedItemHolders;

    public EquippedItemHolder WeaponHolder { get; private set; }
    public EquippedItemHolder ShieldHolder { get; private set; }

    private void Start() {
        equippedItemHolders = GetComponentsInChildren<EquippedItemHolder>();
        Player.Inventory.OnEquipmentChangedTo += UpdateEquipment;

        foreach(EquippedItemHolder holder in equippedItemHolders) {
            if (holder.Type == ItemType.Weapon)
                WeaponHolder = holder;
            if (holder.Type == ItemType.Shield)
                ShieldHolder = holder;
        }
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