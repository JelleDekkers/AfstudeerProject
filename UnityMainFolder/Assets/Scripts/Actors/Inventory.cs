using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class Inventory {

    public List<ItemData> Items;// { get; private set; }

	public Inventory(ItemData[] items) {
        Items = items.ToList();
    }

    public Inventory() {
        Items = new List<ItemData>();
    }

    public void AddItem(ItemData item) {
        Items.Add(item);
        //OnInventoryWasChanged();
    }

    public void RemoveItem(ItemData item) {
        Items.Remove(item);
        //OnInventoryWasChanged();
    }

    //checken of uberhaupt werkt? misschien via properties? misschien via zelf als parameter invullen welke type?
    public List<ItemData> GetAllType(Type type) {
        //var items = Items.OfType<type>();
        return null;
    }
}
