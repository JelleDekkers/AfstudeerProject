using UnityEngine;
using System.Collections;

public class IconManager : MonoBehaviour {

    [SerializeField] private Sprite armorIcon;
    [SerializeField] private Sprite weaponIcon;
    [SerializeField] private Sprite healthIcon;
    [SerializeField] private Sprite shieldIcon;

    public static Sprite ArmorIcon { get { return instance.armorIcon; } }
    public static Sprite WeaponIcon { get { return instance.weaponIcon; } }
    public static Sprite HealthIcon { get { return instance.healthIcon; } }
    public static Sprite ShieldIcon { get { return instance.shieldIcon; } }

    private static IconManager instance = null;

    private void Start() {
        if (instance == null)
            instance = this;
    }

    public static Sprite GetItemTypeIcon(ItemData item) {
        if ((int)item.Type < 6)
            return ArmorIcon;
        else if (item.Type == ItemType.Shield)
            return ShieldIcon;
        else if (item.Type == ItemType.Weapon)
            return WeaponIcon;
        else if (item.Type == ItemType.Potion)
            return HealthIcon;

        Debug.Log("No corresponding item type found, returning armor icon.");
        return ArmorIcon;
    }

    public static Sprite GetItemTypeIcon(ItemGameObject item) {
        if ((int)item.Type < 6)
            return ArmorIcon;
        else if (item.Type == ItemType.Shield)
            return ShieldIcon;
        else if (item.Type == ItemType.Weapon)
            return WeaponIcon;
        else if (item.Type == ItemType.Potion)
            return HealthIcon;

        Debug.Log("No corresponding item type found, returning armor icon.");
        return ArmorIcon;
    }
}
