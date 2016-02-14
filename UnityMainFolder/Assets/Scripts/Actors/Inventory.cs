using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class Inventory {

    public List<ItemData> Items;// { get; private set; }
    public List<List<ItemData>> itemLists = new List<List<ItemData>>();

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
        Debug.Log("adding item");
        // Check if a similar item already exists in the inventory:
        foreach(List<ItemData> list in itemLists) {
            // check if list is empty:
            if (!list.Any()) {
                list.Add(item);
            } else if (CompareItems(list[0], item)) {
                list.Add(item);
                return;
            }
        }
        // else, create a new one:
        List<ItemData> newUniqueItem = new List<ItemData>();
        newUniqueItem.Add(item);
        itemLists.Add(newUniqueItem);
    }

    public void RemoveItem(ItemData item) {
        //Items.Remove(item);
        foreach (List<ItemData> list in itemLists) {
            if (CompareItems(list[0], item)) {
                list.RemoveAt(0);
                return;
            }
        }
        Debug.LogError("No item found in inventory: + " + item.Name);
    }

    public int GetTotalAmountOfItems() {
        int amount = 0;
        foreach(List<ItemData> list in itemLists) {
            amount += list.Count();
        }
        return amount;
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
