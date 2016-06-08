using UnityEngine;
using System.Collections.Generic;

namespace AfstudeerProject.LevelEditor {

    [ExecuteInEditMode]
    public class LevelBuilder : MonoBehaviour {

        [Header("Level Items + Snapping values: ")]
        public List<EditorItem> EditorItems = new List<EditorItem>();
        public Transform LevelParent;
        public Transform RayCatchPlane;

        public const KeyCode PLACE_AGAIN = KeyCode.LeftControl;
        public const KeyCode CANCEL_KEY = KeyCode.Escape;
        public const KeyCode TOGGLE_SNAPPING = KeyCode.Q;

        public static bool ShowWireBox = true;

        public GameObject prevPlacedGameObject;
    }
}