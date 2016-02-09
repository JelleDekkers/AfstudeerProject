using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteInEditMode]
public class UIGridResizer : MonoBehaviour {

    [SerializeField] private float itemAmount;
    [SerializeField] private bool fitX = true;
    [SerializeField] private bool fitY = true;
    [SerializeField] private bool useSpacing = false;
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
