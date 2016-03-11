using UnityEngine;
using System.Collections;

public class NodeManager : MonoBehaviour {

    public static NodeManager Instance;
    public Node[] allNodes;
    public LayerMask nodeDetectionLayerMask;

    private void Awake() {
        if (Instance == null)
            Instance = this;

        allNodes = transform.GetComponentsInChildren<Node>();
    }
}
