using UnityEngine;
using System.Collections;

/// <summary>
/// Type of item, 0 to 6 is armor.
/// </summary>
public enum ItemType {
    Helmet = 0,
    Cuirass = 1,
    LeftPauldron = 2,
    RightPauldron = 3,
    LeftGreave = 4,
    RightGreave = 5,
    Weapon = 6,
    Shield = 7,
    Quest = 8,
    Bow = 9
}

[System.Serializable]
public class ItemData {
    public string Name { get; private set; }
    public ItemType Type { get; private set; }
    public string MeshName { get; private set; }
    public Sprite Sprite { get; private set; }
    public float Points { get; private set; }

    public float WeaponLength { get; private set; }
    public float AttackAngle { get; private set; }

    public ItemData(string name, ItemType type, string meshName, Sprite sprite, float points, float weaponLength, float attackAngle) {
        Name = name;
        Type = type;
        MeshName = meshName;
        Sprite = sprite;
        Points = points;
        WeaponLength = weaponLength;
        AttackAngle = attackAngle;
    }
}
