using UnityEngine;

public class EquippedItemHolderManager : MonoBehaviour {

    [SerializeField]
    private EquippedItemHolder[] equippedItemHolders;

    public EquippedItemHolder WeaponHolder { get; private set; }
    public EquippedItemHolder ShieldHolder { get; private set; }

    private void Start() {
        equippedItemHolders = GetComponentsInChildren<EquippedItemHolder>();

        GetComponent<Actor>().Inventory.OnEquipmentChangedTo += UpdateEquipment;

        foreach(EquippedItemHolder holder in equippedItemHolders) {
            if (holder.Type == ItemType.Weapon)
                WeaponHolder = holder;
            if (holder.Type == ItemType.Shield)
                ShieldHolder = holder;
        }
    }

    /// <summary>
    /// Updates the player model with models of the equipped items.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="item"></param>
    public void UpdateEquipment(ItemType type, ItemData item) {
        foreach (EquippedItemHolder holder in equippedItemHolders) {
            if (holder.Type == type) {
                holder.UpdateHolder(item, GetComponent<Actor>());
                return;
            }
        }
        Debug.LogError("No EquippedItemHolder found with type: " + type + " for item: " + item.Name);
    }

    public void DropEquippedWeapons() {
        //is nog niet uit inventory verwijderd!!!
        //TODO: meshcollider erop laten zitten bij het oppakken van item aan/uit zetten bij het droppen.
        // misschien doen na een korte random timer, voor beter effect.
        GameObject item = WeaponHolder.Item.gameObject;
        item.AddComponent<BoxCollider>();
        item.AddComponent<Rigidbody>();
        item.transform.parent = null;
    }
}