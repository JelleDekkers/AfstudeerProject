﻿using UnityEngine;
using System.Collections;
using AfstudeerProject.UI;

[RequireComponent(typeof(EquippedItemHolderManager))]
public class EquipItemsOnStart : MonoBehaviour {

    public ItemGameObject[] itemsToEquip;

    private void Start() {
        EquipItems();
    }

    public void EquipItems() {
        //EquippedItemHolderManager manager = GetComponent<EquippedItemHolderManager>();
        Actor actor = GetComponent<Actor>();
        foreach (ItemGameObject i in itemsToEquip) {
            if (i != null) {
                ItemData item = new ItemData(i.Name, i.Type, i.MeshName, i.Sprite, i.Points, i.WeaponLength, i.AttackAngle);
                actor.Inventory.EquipItem(item);
            }
        }
    }
}
