using UnityEngine;
using UnityEditor;

namespace AfstudeerProject.LevelEditor {

    //[InitializeOnLoad]
    public static class LevelEditor {

        private const KeyCode COPY_KEY = KeyCode.LeftControl;
        private const KeyCode DRAG_KEY = KeyCode.LeftShift;

        private static GameObject currentlyActiveGameObject;
        private static Vector3 mousePos;
        private static RaycastHit hit;

        static LevelEditor() {
            SceneView.onSceneGUIDelegate += Update;
        }

        public static void Update(SceneView view) {
            Event e = Event.current;

            if (Tools.current == Tool.Move && e.button == 0 && e.keyCode == COPY_KEY)
                CopyItem();

            if (Selection.activeGameObject == null || e.type == EventType.MouseUp)
                currentlyActiveGameObject = null;

            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            hit = new RaycastHit();
            var controlID = GUIUtility.GetControlID(FocusType.Passive);
            var eventType = e.GetTypeForControl(controlID);

            if (Physics.Raycast(ray, out hit, 100000f)) {
                mousePos = new Vector3(hit.point.x, hit.point.y, hit.point.z);

                if(e.keyCode == COPY_KEY && e.button == 0 && e.type == EventType.MouseDown && Tools.current == Tool.Move && Selection.activeGameObject != null) {
                    CopyItem();
                }
            }
        }

        private static void CopyItem() {
            if (Selection.activeGameObject == null || Selection.activeGameObject == currentlyActiveGameObject)
                return;

            currentlyActiveGameObject = Object.Instantiate(Selection.activeGameObject);
            currentlyActiveGameObject.name = Selection.activeGameObject.name;
            Selection.activeGameObject = currentlyActiveGameObject;
            Undo.RegisterCreatedObjectUndo(currentlyActiveGameObject, "Create " + currentlyActiveGameObject.name);
        }
    }
}