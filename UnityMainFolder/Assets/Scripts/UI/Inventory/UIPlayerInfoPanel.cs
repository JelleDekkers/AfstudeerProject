using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AfstudeerProject.UI {

    public class UIPlayerInfoPanel : MonoBehaviour {

        [SerializeField] private Text armorPointsText;
        [SerializeField] private Text attackPointsText;
        [SerializeField] private Text shieldPointsText;

        private void OnEnable() {
            UIItemSlot.OnItemSlotChanged += UpdateText;
            UpdateText();
        }
        
        private void UpdateText() {
            Debug.Log("updating text");
            armorPointsText.text = Player.ArmorPoints.ToString();
            attackPointsText.text = Player.AttackPoints.ToString();
            shieldPointsText.text = Player.ShieldPoints.ToString();
        }
    }
}