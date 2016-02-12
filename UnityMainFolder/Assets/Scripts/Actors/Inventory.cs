using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class Inventory {

    public List<ItemData> Items;// { get; private set; }
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

    public event Action OnEquipmentChanged;

    public Inventory(ItemData[] items) {
        Items = items.ToList();
    }

    public Inventory() {
        Items = new List<ItemData>();
    }

    public void AddItem(ItemData item) {
        Items.Add(item);
    }

    public void RemoveItem(ItemData item) {
        Debug.Log("remove:  " + item.Name);
        Items.Remove(item);
    }

    public float GetTotalArmorPoints() {
        float points = 0;
        foreach (EquippedItem i in equippedItems)
            if (i.Item != null && (int)i.Type < 6) // all armortype enums to int
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

    public List<ItemData> GetAllType(Type type) {
        //var items = Items.OfType<type>();
        return null;
    }

    public void EquipItem(ItemData item) {
        Debug.Log("equip item from inventory");
        foreach(EquippedItem i in equippedItems) {
            if (i.Type == item.Type) {
                i.Item = item;
                OnEquipmentChanged();
                return;
            }
        }
    }

    public void UnequipItem(ItemData item) {
       foreach(EquippedItem i in equippedItems) {
            if (i.Item == item) {
                i.Item = null;
                OnEquipmentChanged();
                return;
            }
        }
    }
}
