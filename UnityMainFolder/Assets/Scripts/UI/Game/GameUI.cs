using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUI : MonoBehaviour {

    [SerializeField] private GameItemInfoPanel infoPanel;
    [SerializeField] private Slider healthbarSlider;
    [SerializeField] private Text potionCount;
    [SerializeField] private GameObject ReadPanelObject;

    public static GameUI Instance;

    private void Start() {
        PlayerInteractions.OnNearbyItemSelectable += ShowInfoPanel;
        PlayerInteractions.OnNoNearbyItemSelectable += HideInfoPanel;
        healthbarSlider.maxValue = Player.Instance.CurrentHealthPoints;
        if (Instance == null)
            Instance = this;
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

    public void ShowReadObject(string text) {
        ReadPanelObject.transform.FindChild("Text").GetComponent<Text>().text = text;
        ReadPanelObject.SetActive(true);
        Player.Instance.OnDamageTaken += HideReadObject;
    }

    private void HideReadObject(GameObject damager) {
        Player.Instance.OnDamageTaken -= HideReadObject;
        HideReadObject();
    }

    public void HideReadObject() {
        ReadPanelObject.SetActive(false);
        Time.timeScale = 1;
    }
}
