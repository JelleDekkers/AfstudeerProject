using UnityEngine;
using System;

public enum playerState {
    InGame = 0,
    InInventory = 1,
    InMenu = 2,
    Dead = 3
}

public class PlayerState : MonoBehaviour {
    public static playerState State = playerState.InGame;
    public static event Action<playerState> OnStateChanged;
    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
        OnStateChanged += StateChange;
    }

    public static void SetState(playerState state) {
        State = state;
        OnStateChanged(state);
    }

    private void StateChange(playerState state) {
        anim.SetInteger("GameState", (int)state);
    }
}
