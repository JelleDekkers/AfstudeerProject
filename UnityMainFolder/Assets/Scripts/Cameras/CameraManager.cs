using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    private Camera mainCam;

    void Start() {
        mainCam = Camera.main;
        PlayerState.OnStateChanged += ShowCorrectCam;
    }

    private void ShowCorrectCam(playerState state) {
        if (state == playerState.InInventory) {
            mainCam.gameObject.SetActive(false);
        } else if(state == playerState.InGame){
            mainCam.gameObject.SetActive(true);
        }
    }

    private void OnDestroy() {
        PlayerState.OnStateChanged -= ShowCorrectCam;
    }
}
