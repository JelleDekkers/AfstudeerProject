using UnityEngine;
using System.Collections;

public class InventoryItem {

    public ItemData Item { get; private set; }
    public int Count { get; set; }

    public InventoryItem(ItemData item) {
        Item = item;
        Count = 1;
    }
}
