using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUI : MonoBehaviour {

    [SerializeField] private GameItemInfoPanel infoPanel;
    [SerializeField] private Slider healthbarSlider;

    private void Start() {
        PlayerInteractions.OnNearbyItemSelectable += ShowInfoPanel;
        PlayerInteractions.OnNoNearbyItemSelectable += HideInfoPanel;
        healthbarSlider.maxValue = Player.Instance.HealthPoints;
    }

    private void Update() {
        healthbarSlider.value = Player.Instance.HealthPoints;
    }

    private void ShowInfoPanel(InteractableObject item) {
        infoPanel.gameObject.SetActive(true);
        infoPanel.UpdateInfo(item);
    }

    private void HideInfoPanel() {
        infoPanel.gameObject.SetActive(false);
    }
}
