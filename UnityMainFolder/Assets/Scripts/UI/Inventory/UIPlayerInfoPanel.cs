using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AfstudeerProject.UI {

    public class UIPlayerInfoPanel : MonoBehaviour {

        [SerializeField] private Text armorPointsText;
        [SerializeField] private Text attackPointsText;
        [SerializeField] private Text healthPointsText;

        private void OnEnable() {
            armorPointsText.text = Player.ArmorPoints.ToString();
            attackPointsText.text = Player.AttackPoints.ToString();
            healthPointsText.text = Player.HealthPoints.ToString();
        }
    }
}