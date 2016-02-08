using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory {

    public List<Item> Items { get; private set; } 

	public Inventory(Item[] items) {
        Items = items.ToList();
    }

    public Inventory() {
        Items = new List<Item>();
    }

    public void AddItem(Item item) {
        Items.Add(item);
    }

    //checken of uberhaupt werkt? misschien via properties? misschien via zelf als parameter invullen welke type?
    public List<Armor> GetAllArmor() {
        var armors = Items.OfType<Armor>();
        return armors.ToList();
    }
}
