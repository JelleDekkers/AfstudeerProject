using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AfstudeerProject.UI {

    [ExecuteInEditMode]
    public class GridScaleContentToFitAllVertical : MonoBehaviour {

        private float itemAmount;
        private GridLayoutGroup grid;
        private Rect rect;

        private void OnEnable() {
            itemAmount = transform.childCount;
            grid = GetComponent<GridLayoutGroup>();
            rect = GetComponentInChildren<RectTransform>().rect;
            float xSize = rect.width / itemAmount;
            float ySize = rect.height / itemAmount;
            xSize = rect.width;
            grid.cellSize = new Vector2(xSize, ySize);
        }
    }
}