using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ScreenManager : MonoBehaviour {

    [SerializeField] private GameObject inventoryScreen;
    [SerializeField] private GameObject inventoryCamera;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private bool fadeInOnStart;
    [SerializeField] private bool hideCursor;

    public static bool fadeComplete;

    private void Start() {
        //Cursor.visible = false;
        if(fadeInOnStart)
            Fade.Instance.FadeIn();
        if (hideCursor)
            Cursor.visible = false;
    }

    private void Update() {
        // Inventory:
        if (Input.GetKeyDown(PlayerInput.InventoryButton)) {
            if (PlayerState.State == playerState.InGame)
                OpenInventory();
            else if(PlayerState.State == playerState.InInventory)
                CloseInventory();
        }

        if(fadeComplete) {
            if (Input.anyKey) {
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                fadeComplete = false;
            }
        }
    }

    private void OpenInventory() {
        //Cursor.visible = true;
        PlayerState.SetState(playerState.InInventory);
        inventoryScreen.SetActive(true);
        gameUI.SetActive(false);
        Player.Instance.OnDamageTaken += CloseInventory;
        inventoryCamera.SetActive(true);
        Time.timeScale = 0;
    }

    private void CloseInventory() {
        //Cursor.visible = false;
        PlayerState.SetState(playerState.InGame);
        inventoryScreen.SetActive(false);
        gameUI.SetActive(true);
        inventoryCamera.SetActive(false);
        Time.timeScale = 1;
    }

    private void CloseInventory(GameObject damage) {
        CloseInventory();
        Player.Instance.OnDamageTaken -= CloseInventory;
    }
}
