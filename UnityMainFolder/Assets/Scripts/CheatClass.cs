using UnityEngine;
using System.Collections;

public class CheatClass : MonoBehaviour {

    [SerializeField]
    private ItemGameObject weapon;
    [SerializeField]
    private ItemGameObject shield;

    void Start() {
        ItemData item = new ItemData(weapon.Name, weapon.Type, weapon.MeshName, weapon.Sprite, weapon.Points);
        Player.Inventory.EquipItem(item);
        item = new ItemData(shield.Name, shield.Type, shield.MeshName, shield.Sprite, shield.Points);
        Player.Inventory.EquipItem(item);
    }
}
