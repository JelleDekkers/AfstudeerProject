using UnityEngine;
using System.Collections;

namespace AfstudeerProject.UI {

    public class UIInventoryGrid : MonoBehaviour {

        [SerializeField] private UIInventoryItem inventoryGridItemPrefab;

        private void OnEnable() {
            ClearGrid();
            if (Player.Inventory.Items.Count != 0)
                ShowItemsInGrid(Player.Inventory.Items.ToArray());
        }

        public void ShowItemsInGrid(ItemData[] items) {
            UIInventory.itemsBeingShown = items;
            ClearGrid();

            for (int i = 0; i < items.Length; i++) {
                UIInventoryItem obj = Instantiate(inventoryGridItemPrefab);
                obj.transform.SetParent(transform, false);
                obj.Init(items[i]);
            }
        }

        private void ClearGrid() {
            if (transform.childCount > 0) {
                foreach (Transform t in transform)
                    Destroy(t.gameObject);
            }
        }
    }
}