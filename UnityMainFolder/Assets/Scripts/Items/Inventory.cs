using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory {

    private List<Item> items;

	public Inventory(Item[] items) {
        this.items = items.ToList();
    }

    public Inventory() {
    }

    public void Add(Item item) {
        Debug.Log("Adding " + item + " to inventory");
    }

    //checken of uberhaupt werkt? misschien via properties? misschien via zelf als parameter invullen welke type?
    public List<Armor> GetAllArmor() {
        var armors = items.OfType<Armor>();
        return armors.ToList();
    }
}
