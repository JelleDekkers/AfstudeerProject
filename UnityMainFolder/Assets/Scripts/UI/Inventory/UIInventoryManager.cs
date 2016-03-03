using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

namespace AfstudeerProject.UI {

    public class UIInventoryManager : MonoBehaviour {

        public enum shownItemsCategory {
            All,
            Weapons,
            Shields,
            Armor,
            Misc
        }

        [SerializeField] private UIInventoryGrid inventoryGrid;
        [SerializeField] private UIItemInfoPanel itemInfoPanel;

        private ItemData currentSelectedItem;

        private static UIInventoryManager instance;

        public static shownItemsCategory ShownCategory = shownItemsCategory.All;
        public static List<InventoryItem> itemsInGrid;
        public static event Action OnSelectedItemIsNull;

        private void Awake() {
            if (instance == null)
                instance = this;
        }

        private void OnEnable() {
            OnSelectedItemIsNull += OnSelectedItemIsNullFunction;
            ShowItemsInGrid(Player.Instance.Inventory.Items);
        }

        private void Update() { 
            if (Input.GetMouseButtonUp(0)) {
                if (!EventSystem.current.IsPointerOverGameObject()) {
                    OnSelectedItemIsNull();
                } else if(EventSystem.current.IsPointerOverGameObject()) {
                    GameObject selectedGObj = EventSystem.current.currentSelectedGameObject;
                    if (selectedGObj == null) {
                        OnSelectedItemIsNull();
                    } else if (selectedGObj.GetComponent<UIItemSlot>() && selectedGObj.GetComponent<UIItemSlot>().equippedItem == null) {
                        OnSelectedItemIsNull();
                    }
                }
            }
        }

        private void OnSelectedItemIsNullFunction() {
            currentSelectedItem = null;
            itemInfoPanel.gameObject.SetActive(false);
        }

        public static void OnItemMovedFunction() {
            instance.HideItemInfo();
        }

        public static void ActivateOnItemSelected(ItemData item, bool showDiscardedBtn) {
            instance.ShowItemInfo(item, showDiscardedBtn);
        }

        public void ShowItemsInGrid(List<InventoryItem> items) {
            inventoryGrid.ShowItemsInGrid(items);
        }

        public static void UpdateItemsGrid() { 
            instance.HideItemInfo();
            // TODO: dynamisch maken via cat tabs
            instance.inventoryGrid.ShowItemsInGrid(Player.Instance.Inventory.Items);
        }

        private void ShowItemInfo(ItemData item, bool showDiscardedBtn) {
            currentSelectedItem = item;
            itemInfoPanel.gameObject.SetActive(true);
            itemInfoPanel.UpdateInfo(currentSelectedItem, showDiscardedBtn);
        }

        private void HideItemInfo() {
            currentSelectedItem = null;
            itemInfoPanel.gameObject.SetActive(false);
        }

        #region UI Buttons
        public void Destroy() {
            Player.Instance.Inventory.RemoveItem(currentSelectedItem);
            //ItemGameObject.InstantiateFromResourcesFolder(currentSelectedItem);
            UpdateItemsGrid();
        }

        public void SetItemsShownCategory(int cat) {
            if (cat > Enum.GetNames(typeof(shownItemsCategory)).Length) {
                Debug.LogWarning("Entered Category number is too high, setting category to 'All'.");
                ShownCategory = shownItemsCategory.All;
            }
            ShownCategory = (shownItemsCategory)cat;
        }
        #endregion
    }
}