using UnityEngine;
using System.Collections;

public class Player : Actor {

    public static Player Instance;

    public uint Potions;

    private void Start() {
        if (Instance == null)
            Instance = this;

        Inventory.OnEquipmentChanged += UpdateStats;
        OnDeath += GameOver;
    }

    public override void Update() {
        base.Update();
    }

    private void GameOver() {
        float timeScale = 0.5f;
        float seconds = 1;
        StartCoroutine(TimeController.SlowTimeForSeconds(timeScale, seconds));
        Fade.Instance.FadeOut(true);
    }
}
