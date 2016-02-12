using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AfstudeerProject.UI {

    public class GridScaleContentToFitByAmount : MonoBehaviour {

        [SerializeField]
        private int preferredItemsPerColumn = 5;

        private Rect rect;

        private void Start() {
            GetComponent<GridLayoutGroup>().cellSize = CalculateScale();
        }

        private Vector2 CalculateScale() {
            rect = GetComponentInChildren<RectTransform>().rect;
            float xSize = rect.width / preferredItemsPerColumn;
            float ySize = xSize;
            return new Vector2(xSize, ySize);
        }
    }
}
