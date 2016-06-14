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

    public override void TakeDamage(GameObject sender, float amount) {
        base.TakeDamage(sender, amount);
        float shakeModifier = 20;

        if (sender != null) {
            // prevent fire damage to continue shaking camera
            if (sender.GetComponent<Fire>() != null)
                PlayerCamera.Instance.Shake(0.2f, 3, 3);
            else
                PlayerCamera.Instance.Shake(0.2f, amount / shakeModifier, 3);
        }
    }

    public override void Block(GameObject killer, float attackPoints) {
        base.Block(killer, attackPoints);
        PlayerCamera.Instance.Shake(0.2f, 3, 4);
    }

    protected override void AttackProp(GameObject objectHit, Vector3 forceDirection, float forceAmount) {
        base.AttackProp(objectHit, forceDirection, forceAmount);
        PlayerCamera.Instance.Shake(0.2f, 2, 4);
    }

    private void GameOver() {
        float timeScale = 0.5f;
        float seconds = 1;
        StartCoroutine(TimeController.SlowTimeForSeconds(timeScale, seconds));
        Fade.Instance.FadeOut("Game Over");
    }
}
