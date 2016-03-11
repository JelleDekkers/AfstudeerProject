using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {

    public List<Node> neighbours;

    private void Start() {
        neighbours = GetNeighbours();
    }

    private List<Node> GetNeighbours() {
        List<Node> neighbours = new List<Node>();
        RaycastHit hit;
        Vector3 direction = Vector3.zero;
        float distance = 0;
        LayerMask layerMask = NodeManager.Instance.nodeDetectionLayerMask;

        foreach (Node node in NodeManager.Instance.allNodes) {
            Vector3 heading = node.transform.position - transform.position;
            distance = heading.magnitude;
            direction = heading / distance;
            //distance = Vector3.Distance(transform.position, node.transform.position);

            if (Physics.Raycast(transform.position, direction, out hit, distance, layerMask)) {
                if (hit.collider.tag == "Node") {
                    neighbours.Add(hit.collider.gameObject.GetComponent<Node>());
                }
            }
        }
        return neighbours;
    }

    private void OnDrawGizmos() {
        Color gizmosColor = Color.white;
        foreach (Node node in neighbours) {
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
    }
}
