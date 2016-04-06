using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    [SerializeField] private Camera inventoryCam;

    private Camera mainCam;

    void Start() {
        mainCam = Camera.main;
        PlayerState.OnStateChanged += ShowCorrectCam;
        inventoryCam.gameObject.SetActive(false);
    }

    private void ShowCorrectCam(playerState state) {
        if (state == playerState.InInventory) {
            inventoryCam.gameObject.SetActive(true);
            mainCam.gameObject.SetActive(false);
        } else if(state == playerState.InGame){
            mainCam.gameObject.SetActive(true);
            inventoryCam.gameObject.SetActive(false);
        }
    }
}
