using UnityEngine;
using System.Collections;

public static class OutlineMaterialManager {

    public static Material outlineMat, selectionMat, normalMat;

    private static bool initialized;
    private const string COLOR_PROPERTY = "_node_5860";
    private static Color normalColor = Color.yellow;
    private static Color selectionColor = Color.green;

    private static void Init() {
        outlineMat = Resources.Load("Materials/Outlines/Outline_Items", typeof(Material)) as Material;
        normalMat = Resources.Load("Materials/BumpedDiffuse", typeof(Material)) as Material;
        initialized = true;
    }

    public static void ChangeMatsToSelectionColor(GameObject g) {
        if (!initialized)
            Init();

        if (g.GetComponent<Renderer>())
            ChangeMaterialColor(g.GetComponent<Renderer>().material, selectionColor);
        //SetMaterialAndSwitchTextures(selectionMat, g.GetComponent<Renderer>());

        foreach (Transform t in g.transform) {
            if (t.GetComponent<Renderer>()) {
                ChangeMaterialColor(t.GetComponent<Renderer>().material, selectionColor);
                //SetMaterialAndSwitchTextures(selectionMat, t.GetComponent<Renderer>());
            }
        }
    }

    public static void ChangeMatsToNormalColor(GameObject g) {
        if (!initialized)
            Init();

        if (g.GetComponent<Renderer>())
            ChangeMaterialColor(g.GetComponent<Renderer>().material, normalColor);
       // SetMaterialAndSwitchTextures(itemMat, g.GetComponent<Renderer>());

        foreach (Transform t in g.transform) {
            if (t.GetComponent<Renderer>())
                ChangeMaterialColor(t.GetComponent<Renderer>().material, normalColor);
            //SetMaterialAndSwitchTextures(itemMat, t.GetComponent<Renderer>());
        }
    }

    public static void SwitchToBumpedDiffuse(GameObject g) {
        if (!initialized)
            Init();

        if (g.GetComponent<Renderer>())
            SetMaterialAndSwitchTextures(normalMat, g.GetComponent<Renderer>());

        foreach (Transform t in g.transform) {
            if (t.GetComponent<Renderer>())
                SetMaterialAndSwitchTextures(normalMat, t.GetComponent<Renderer>());
        }
    }

    public static void SwitchToOutlineMat(GameObject g) {
        if (!initialized)
            Init();

        if (g.GetComponent<Renderer>())
            SetMaterialAndSwitchTextures(outlineMat, g.GetComponent<Renderer>());

        foreach (Transform t in g.transform) {
            if (t.GetComponent<Renderer>())
                SetMaterialAndSwitchTextures(outlineMat, t.GetComponent<Renderer>());
        }
    }

    private static void ChangeMaterialColor(Material mat, Color color) {
        mat.SetColor(COLOR_PROPERTY, color);
    }

    private static void SetMaterialAndSwitchTextures(Material newMat, Renderer r) {
        Texture mainTex = null;
        Texture normal = null;
        mainTex = r.material.GetTexture("_MainTex");
        normal = r.material.GetTexture("_BumpMap");
        r.material = newMat;
        r.material.SetTexture("_Albedo", mainTex);
        r.material.SetTexture("_Normal", normal);
    }
}
