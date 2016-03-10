using UnityEngine;
using System.Collections;

public class Player : Actor {

    public static Player Instance;

    private void Start() {
        if (Instance == null)
            Instance = this;

        Inventory.OnEquipmentChanged += UpdateStats;
    }

    public override void Update() {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Space)) {
            TakeDamage(1, null);
        }
    }
}
