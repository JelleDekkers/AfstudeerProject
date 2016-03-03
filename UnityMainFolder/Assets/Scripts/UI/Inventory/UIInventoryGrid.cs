using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AfstudeerProject.UI {

    public class UIInventoryGrid : MonoBehaviour {

        [SerializeField] private UIInventoryItem inventoryGridItemPrefab;

        private void OnEnable() {
            ClearGrid();
            if (Player.Instance.Inventory.Items.Count != 0)
                ShowItemsInGrid(Player.Instance.Inventory.Items);
        }

        public void ShowItemsInGrid(List<InventoryItem> items) {
            UIInventoryManager.itemsInGrid = items;
            ClearGrid();

            for (int i = 0; i < items.Count; i++) {
                UIInventoryItem obj = Instantiate(inventoryGridItemPrefab);
                obj.Init(items[i]);
                obj.transform.SetParent(transform, false);
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