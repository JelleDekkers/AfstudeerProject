using UnityEngine;
using System.Collections;

public class Player : Actor {

    public static Player Instance;

    private void Start() {
        if (Instance == null)
            Instance = this;

        Inventory.OnEquipmentChanged += UpdateStats;
        OnDeath += GameOver;
    }

    public override void Update() {
        base.Update();

        if (CurrentHealthPoints < MaxHealthPoints && Input.GetKey(PlayerInput.UsePotionButton))
            UseHealthPotion();
    }

    private void GameOver() {
        float timeScale = 0.5f;
        float seconds = 1;
        StartCoroutine(TimeController.SlowTimeForSeconds(timeScale, seconds));
        Fade.Instance.FadeOut(true);
    }
}
