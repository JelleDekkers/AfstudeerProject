using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AfstudeerProject.UI {

    [ExecuteInEditMode]
    public class GridScaleContentToFitByAmount : MonoBehaviour {

        [SerializeField]
        private int itemsPerColumn = 5;

        private GridLayoutGroup grid;
        private Rect rect;

        private void OnEnable() {
            grid = GetComponent<GridLayoutGroup>();
            rect = GetComponentInChildren<RectTransform>().rect;
            float xSize = rect.width / itemsPerColumn;
            float ySize = xSize;
            grid.cellSize = new Vector2(xSize, ySize);
        }
    }
}
