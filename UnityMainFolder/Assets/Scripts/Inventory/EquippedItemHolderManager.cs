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

    public void DropAndApplyForceToEquippedWeapons(Vector3 forceDirection, float forceAmount) {
        //is nog niet uit inventory verwijderd!!!
        AddRigidbodyAndForceToItem(WeaponHolder.Item.gameObject, forceDirection, forceAmount);
        AddRigidbodyAndForceToItem(ShieldHolder.Item.gameObject, forceDirection, forceAmount);
    }

    private void AddRigidbodyAndForceToItem(GameObject item, Vector3 forceDirection, float forceAmount) {
        float forceMultiplier = 1.5f;
        float torqueForce = 100;
        float itemMass = 8;
        float itemDrag = 2;
        Vector3 randomTorqueDirection = new Vector3(Random.Range(0.5f, 1), Random.Range(0.5f, 1), Random.Range(0.5f, 1));

        item.layer = 0;
        item.GetComponent<Collider>().enabled = true;
        Rigidbody itemRigidbody = item.AddComponent<Rigidbody>();
        itemRigidbody.mass = itemMass;
        itemRigidbody.drag = itemDrag;
        itemRigidbody.AddForce(forceDirection * (forceAmount * forceMultiplier), ForceMode.Impulse);
        itemRigidbody.AddTorque(randomTorqueDirection * torqueForce, ForceMode.Impulse);
    }
}