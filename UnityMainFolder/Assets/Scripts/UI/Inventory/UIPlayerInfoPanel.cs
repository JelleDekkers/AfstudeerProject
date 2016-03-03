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
            armorPointsText.text = Player.Instance.ArmorPoints.ToString();
            attackPointsText.text = Player.Instance.AttackPoints.ToString();
            shieldPointsText.text = Player.Instance.ShieldPoints.ToString();
        }
    }
}