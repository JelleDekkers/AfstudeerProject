using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIGridResizer : MonoBehaviour {

    [SerializeField] private float columnAmount = 5;
    [SerializeField] private bool oneColumnOnly = true;
    private GridLayoutGroup grid;
    private Rect rect;

    private void Start() {
        grid = GetComponent<GridLayoutGroup>();
        rect = GetComponentInChildren<RectTransform>().rect;
        float xSize = rect.width / columnAmount;
        float ySize = rect.width / columnAmount;
        if (oneColumnOnly) {
            ySize = rect.height;
        }
        grid.cellSize = new Vector2(xSize, ySize);
    }

}
