using UnityEngine;
using System.Collections;

public class OutlineShader : MonoBehaviour {

    [Range(0, 0.05f)]
    public float OutlineWidth;

    private Renderer renderComponent;

    private void Start() {
        renderComponent = GetComponent<Renderer>();
    }

    private void Update() {
        renderComponent.material.SetFloat("_Outline", OutlineWidth);
    }
}
