using UnityEngine;
using System.Collections.Generic;

public class OpenDoorWhenWeaponsEquipped : MonoBehaviour {

    [SerializeField]
    private Door doorToOpen;

    private bool allEquipped;

    private void Update() {

        if (Player.Instance.Inventory.Weapon != null && Player.Instance.Inventory.Shield != null) { 
            doorToOpen.ForceOpenDoor(Vector3.forward);
            Destroy(this);
        }
    }
}
