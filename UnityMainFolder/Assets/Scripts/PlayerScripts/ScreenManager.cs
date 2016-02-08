using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenManager : MonoBehaviour {

    [SerializeField]
    private GameObject inventoryScreen;
    [SerializeField]
    private GameObject gameUI;

    private void Start() {
        //CloseInventory();
    }

    private void Update() {
        // Inventory:
        if (Input.GetKeyDown(KeyCode.I)) {
            if (PlayerState.State == playerState.InGame)
                OpenInventory();
            else if(PlayerState.State == playerState.InInventory)
                CloseInventory();
        }
    }

    private void OpenInventory() {
        PlayerState.SetState(playerState.InInventory);
        inventoryScreen.SetActive(true);
        gameUI.SetActive(false);
    }

    private void CloseInventory() {
        PlayerState.SetState(playerState.InGame);
        inventoryScreen.SetActive(false);
        gameUI.SetActive(true);
    }
}
