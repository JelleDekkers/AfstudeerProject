﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ScreenManager : MonoBehaviour {

    [SerializeField] private GameObject inventoryScreen;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private bool fadeInOnStart;
    [SerializeField] private bool hideCursor;

    public static bool GameOverFadeComplete;

    private void Start() {
        //Cursor.visible = false;
        if(fadeInOnStart)
            Fade.Instance.FadeIn();
        if (hideCursor)
            Cursor.visible = false;
    }

    private void Update() {
        // Inventory:
        if (Input.GetKeyDown(KeyCode.I)) {
            if (PlayerState.State == playerState.InGame)
                OpenInventory();
            else if(PlayerState.State == playerState.InInventory)
                CloseInventory();
        }

        if(GameOverFadeComplete && Player.Instance.CurrentState == State.Dead) {
            if (Input.anyKey) {
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void OpenInventory() {
        //Cursor.visible = true;
        PlayerState.SetState(playerState.InInventory);
        inventoryScreen.SetActive(true);
        gameUI.SetActive(false);
        Player.Instance.OnDamageTaken += CloseInventory;
    }

    private void CloseInventory() {
        //Cursor.visible = false;
        PlayerState.SetState(playerState.InGame);
        inventoryScreen.SetActive(false);
        gameUI.SetActive(true);
    }

    private void CloseInventory(GameObject damage) {
        CloseInventory();
        Player.Instance.OnDamageTaken -= CloseInventory;
    }
}
