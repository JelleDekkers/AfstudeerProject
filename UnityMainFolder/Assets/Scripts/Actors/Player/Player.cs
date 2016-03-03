using UnityEngine;
using System.Collections;

public class Player : Actor {

    public static Player Instance;

    private void Start() {
        if (Instance == null)
            Instance = this;
    }
}
