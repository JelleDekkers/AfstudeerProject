using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUI : MonoBehaviour {

    [SerializeField] private GameItemInfoPanel infoPanel;

    private void Start() {
        PlayerInteractions.OnNearbyItemSelectable += ShowInfoPanel;
        PlayerInteractions.OnNoNearbyItemSelectable += HideInfoPanel;
    }

    private void ShowInfoPanel(InteractableObject item) {
        infoPanel.gameObject.SetActive(true);
        infoPanel.UpdateInfo(item);
    }

    private void HideInfoPanel() {
        print("hiding");
        infoPanel.gameObject.SetActive(false);
    }
}
