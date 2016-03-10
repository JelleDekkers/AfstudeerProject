using UnityEngine;
using System.Collections;

public class CheatClass : MonoBehaviour {

    [SerializeField]
    private ItemGameObject weapon;
    [SerializeField]
    private ItemGameObject shield;

    void Start() {
        ItemData item = new ItemData(weapon.Name, weapon.Type, weapon.MeshName, weapon.Sprite, weapon.Points, weapon.WeaponLength, weapon.AttackAngle);
        Player.Instance.Inventory.EquipItem(item);
        item = new ItemData(shield.Name, shield.Type, shield.MeshName, shield.Sprite, shield.Points, shield.WeaponLength, shield.AttackAngle);
        Player.Instance.Inventory.EquipItem(item);
    }
}
