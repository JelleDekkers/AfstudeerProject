using UnityEngine;
using System.Collections;

public class EquippedItem {

    public ItemData Item;
    public ItemType Type;

    public EquippedItem(ItemType type, ItemData item) {
        Type = type;
        Item = item;
    }
}
