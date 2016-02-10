using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenManager : MonoBehaviour {

    [SerializeField] private GameObject inventoryScreen;
    [SerializeField] private GameObject gameUI;


    private void Start() {
        //Cursor.visible = false;
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
        //Cursor.visible = true;
        PlayerState.SetState(playerState.InInventory);
        inventoryScreen.SetActive(true);
        gameUI.SetActive(false);
    }

    private void CloseInventory() {
        //Cursor.visible = false;
        PlayerState.SetState(playerState.InGame);
        inventoryScreen.SetActive(false);
        gameUI.SetActive(true);
    }
}
