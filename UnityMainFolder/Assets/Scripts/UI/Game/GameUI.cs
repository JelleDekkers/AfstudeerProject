using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUI : MonoBehaviour {

    [SerializeField] private GameItemInfoPanel infoPanel;
    [SerializeField] private Slider healthbarSlider;
    [SerializeField] private Text potionCount;

    private void Start() {
        PlayerInteractions.OnNearbyItemSelectable += ShowInfoPanel;
        PlayerInteractions.OnNoNearbyItemSelectable += HideInfoPanel;
        healthbarSlider.maxValue = Player.Instance.CurrentHealthPoints;
    }

    private void Update() {
        healthbarSlider.value = Player.Instance.CurrentHealthPoints;
        potionCount.text = Player.Instance.Potions.ToString();
    }

    private void ShowInfoPanel(InteractableObject item) {
        infoPanel.gameObject.SetActive(true);
        infoPanel.UpdateInfo(item);
    }

    private void HideInfoPanel() {
        infoPanel.gameObject.SetActive(false);
    }
}
