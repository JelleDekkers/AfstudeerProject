using UnityEngine;
using System.Collections;

public static class OutlineMaterialManager {

    public static Material itemMat, selectionMat, normalMat;
    private static bool initialized;

    private static void Init() {
        itemMat = Resources.Load("Materials/Outlines/Outline_Items", typeof(Material)) as Material;
        selectionMat = Resources.Load("Materials/Outlines/Outline_Selected", typeof(Material)) as Material;
        normalMat = Resources.Load("Materials/BumpedDiffuse", typeof(Material)) as Material;
        initialized = true;
    }

    public static void ChangeMatsToSelectionMats(GameObject g) {
        if (!initialized)
            Init();

        if (g.GetComponent<Renderer>())
            SetMaterialAndSwitchTextures(selectionMat, g.GetComponent<Renderer>());

        foreach (Transform t in g.transform) {
            if (t.GetComponent<Renderer>()) {
                SetMaterialAndSwitchTextures(selectionMat, t.GetComponent<Renderer>());
            }
        }
    }

    public static void ChangeMatsToItemMats(GameObject g) {
        if (!initialized)
            Init();

        if (g.GetComponent<Renderer>())
            SetMaterialAndSwitchTextures(itemMat, g.GetComponent<Renderer>());

        foreach (Transform t in g.transform) {
            if (t.GetComponent<Renderer>())
                SetMaterialAndSwitchTextures(itemMat, t.GetComponent<Renderer>());
        }
    }

    public static void ChangeMatsToNormalMat(GameObject g) {
        if (!initialized)
            Init();

        if (g.GetComponent<Renderer>())
            SetMaterialAndSwitchTextures(normalMat, g.GetComponent<Renderer>());

        foreach (Transform t in g.transform) {
            if (t.GetComponent<Renderer>())
                SetMaterialAndSwitchTextures(normalMat, t.GetComponent<Renderer>());
        }
    }

    private static void SetMaterialAndSwitchTextures(Material newMat, Renderer r) {
        Texture mainTex = null;
        Texture normal = null;
        mainTex = r.material.GetTexture("_MainTex");
        normal = r.material.GetTexture("_BumpMap");
        r.material = newMat;
        r.material.SetTexture("_MainTex", mainTex);
        r.material.SetTexture("_BumpMap", normal);
    }
}
