using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class Inventory {

    public List<InventoryItem> Items { get; private set; }

    public EquippedItem[] equippedItems = new EquippedItem[] {
        new EquippedItem(ItemType.Helmet, null),
        new EquippedItem(ItemType.Cuirass, null),
        new EquippedItem(ItemType.Weapon, null),
        new EquippedItem(ItemType.Shield, null),
        new EquippedItem(ItemType.LeftGreave, null),
        new EquippedItem(ItemType.RightGreave, null),
        new EquippedItem(ItemType.RightPauldron, null),
        new EquippedItem(ItemType.LeftPauldron, null)
    };

    public event Action OnEquipmentChanged = null;
    public event Action<ItemType, ItemData> OnEquipmentChangedTo;

    public const int WEAPON_SLOT_NR = 2;
    public const int SHIELD_SLOT_NR = 3;

    public ItemData GetWeapon { get { return equippedItems[WEAPON_SLOT_NR].Item; } }
    public ItemData GetShield { get { return equippedItems[SHIELD_SLOT_NR].Item; } }

    public Inventory() {
        Items = new List<InventoryItem>();
    }

    public void AddItem(ItemData item) {
        if(Items.Count == 0) {
            Items.Add(new InventoryItem(item));
            return;
        }

        // Check if a similar item already exists in the inventory, else add new
        for (int i = 0; i < Items.Count; i++) {
            if (Items[i] == null || Items[i].Count == 0) {
                Items[i] = new InventoryItem(item);
                return;
            } else if (CompareItems(Items[i].Item, item)) {
                Items[i].Count++;
                return;
            } 
        }
        Items.Add(new InventoryItem(item));
    }

    public void RemoveItem(ItemData item) {
        foreach (InventoryItem i in Items) {
            if (CompareItems(i.Item, item)) {
                if (i.Count > 1)
                    i.Count--;
                else {
                    Items.Remove(i);
                }
                return;
            }
        }
        Debug.LogError("No item found in inventory with name: + " + item.Name);
    }

    public float GetTotalArmorPoints() {
        int armorEnums = 6;
        float points = 0;
        foreach (EquippedItem i in equippedItems)
            if (i.Item != null && (int)i.Type < armorEnums)
                points += i.Item.Points;
        return points;
    }

    public float GetShieldPoints() {
        float points = 0;
        foreach (EquippedItem i in equippedItems)
            if (i.Item != null && i.Type == ItemType.Shield)
                points += i.Item.Points;
        return points;
    }

    public float GetWeaponAttackPoints() {
        float points = 0;
        foreach (EquippedItem i in equippedItems)
            if(i.Item != null && i.Type == ItemType.Weapon)
                points += i.Item.Points;
        return points;
    }

    public void EquipItem(ItemData item) {
        foreach(EquippedItem i in equippedItems) {
            if (i.Type == item.Type) {
                i.Item = item;
                OnEquipmentChangedTo(item.Type, item);
                if(OnEquipmentChanged != null)
                    OnEquipmentChanged();
                return;
            }
        }
    }

    public void UnequipItem(ItemType type) {
        foreach (EquippedItem i in equippedItems) {
            if (i.Item == null)
                continue;
            else if (i.Type == type) {
                i.Item = null;
                OnEquipmentChangedTo(type, null);
                if (OnEquipmentChanged != null)
                    OnEquipmentChanged();
                return;
            }
        }
    }

    private bool CompareItems(ItemData originalItem, ItemData itemToCompare) {
        if (originalItem.Name != itemToCompare.Name)
            return false;
        else if(originalItem.Type != itemToCompare.Type)
            return false;
        else if(originalItem.MeshName != itemToCompare.MeshName)
            return false;
        else if(originalItem.Sprite != itemToCompare.Sprite)
            return false;
        else if (originalItem.Points != itemToCompare.Points)
            return false;
        return true;
    }
}
