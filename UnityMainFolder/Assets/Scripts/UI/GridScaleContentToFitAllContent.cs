using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AfstudeerProject.UI {

    [ExecuteInEditMode]
    public class GridScaleContentToFitAllContent : MonoBehaviour {

        [SerializeField]
        private bool fitX = true;
        [SerializeField]
        private bool fitY = true;

        private float itemAmount;
        private bool useSpacing = false;
        private GridLayoutGroup grid;
        private Rect rect;

        private void OnEnable() {
            itemAmount = transform.childCount;
            grid = GetComponent<GridLayoutGroup>();
            rect = GetComponentInChildren<RectTransform>().rect;
            float xSize = rect.width / itemAmount;
            float ySize = rect.height / itemAmount;
            if (fitX)
                xSize = rect.width;
            if (fitY)
                ySize = rect.height;
            grid.cellSize = new Vector2(xSize, ySize);
        }
    }
}
