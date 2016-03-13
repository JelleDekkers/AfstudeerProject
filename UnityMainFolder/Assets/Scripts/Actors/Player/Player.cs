using UnityEngine;
using System.Collections;

public class Player : Actor {

    public static Player Instance;

    private void Start() {
        if (Instance == null)
            Instance = this;

        Inventory.OnEquipmentChanged += UpdateStats;
        OnDeath += SlowTimeOnDeath; // + game over screen
    }

    public override void Update() {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Space)) {
            TakeDamage(1, null);
        }
    }

    private void SlowTimeOnDeath() {
        float timeScale = 0.5f;
        float seconds = 1;
        StartCoroutine(TimeController.SlowTimeForSeconds(timeScale, seconds));
    }
}
