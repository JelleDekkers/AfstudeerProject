using UnityEngine;
using System.Collections;

public class IconManager : MonoBehaviour {

    [SerializeField] private Sprite armorIcon;
    [SerializeField] private Sprite weaponIcon;
    [SerializeField] private Sprite healthIcon;

    public static Sprite ArmorIcon { get { return instance.armorIcon; } }
    public static Sprite WeaponIcon { get { return instance.weaponIcon; } }
    public static Sprite HealthIcon { get { return instance.healthIcon; } }

    private static IconManager instance = null;

    private void Start() {
        if (instance == null)
            instance = this;
    }
}
