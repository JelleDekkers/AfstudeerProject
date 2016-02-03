using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinder : MonoBehaviour {

    public GameObject AI;
    public GameObject Target;

    public List<Node> OpenNodes = new List<Node>();
    public List<Node> ClosedNodes = new List<Node>();

}