using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace AfstudeerProject.LevelEditor {

#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public static class LevelEditor {

        private const KeyCode DRAG_KEY = KeyCode.D;

        private static GameObject currentlyActiveGameObject;
        private static Vector3 mousePos;
        private static RaycastHit hit;
        private static Vector3 startPos;
        private static Vector3 endPos;
        private static Stack draggedObjectsStack = new Stack();
        private static float snap = 10;
        private static bool isDragging;
        private static GameObject[] originalItems;
        private static LayerMask rayDetectorLayerMask = 1 << 31;

        static LevelEditor() {
            SceneView.onSceneGUIDelegate += Update;
        }

        public static void Update(SceneView view) {
            Event e = Event.current;

            if (Selection.activeGameObject == null || e.type == EventType.MouseUp)
                currentlyActiveGameObject = null;


            if (SceneView.currentDrawingSceneView != EditorWindow.focusedWindow) {
                if (isDragging)
                    StopDragging();
                return;
            }
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            hit = new RaycastHit();
            var controlID = GUIUtility.GetControlID(FocusType.Passive);
            var eventType = e.GetTypeForControl(controlID);

            if (e.button == 0 && e.keyCode == DRAG_KEY && Selection.activeGameObject != null) {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, rayDetectorLayerMask)) {
                    mousePos = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    if (!isDragging)
                        StartDragging(Selection.gameObjects);
                    else
                        DragItems(Selection.activeGameObject, mousePos);
                }
            }

            if (isDragging && e.type == EventType.KeyUp && e.keyCode == DRAG_KEY) 
                StopDragging();
        }

        private static void StartDragging(GameObject[] item) {
            isDragging = true;
            originalItems = item;
            draggedObjectsStack.Push(originalItems);
        }

        private static void DragItems(GameObject item, Vector3 mousePos) {
            Vector3 dragDirection = (item.transform.position - mousePos).normalized;
            if (Mathf.Abs(dragDirection.x) >= Mathf.Abs(dragDirection.z)) {
                if (dragDirection.x > 0)
                    dragDirection = new Vector3(-1, 0, 0);
                else
                    dragDirection = new Vector3(1, 0, 0);
            } else {
                if(dragDirection.z > 0)
                    dragDirection = new Vector3(0, 0, -1);
                else
                    dragDirection = new Vector3(0, 0, 1);
            }

            float itemAmount = (int)(Vector3.Distance(item.transform.position, mousePos) / snap);

            if (itemAmount != 1 && itemAmount < draggedObjectsStack.Count) {
                //Debug.Log("destroying last item because: itemAmount: " + itemAmount + " stack: " + draggedObjectsStack.Count);
                DestroyLastItemsInStack();
            } else if (itemAmount > draggedObjectsStack.Count) {
                //Debug.Log("creating new item because: itemAmount: " + itemAmount + " stack: " + draggedObjectsStack.Count);
                CreateNewItems(dragDirection);
            }
        }

        private static void StopDragging() {
            //remove items from stack
            isDragging = false;
            List<GameObject> selection = new List<GameObject>();
            foreach (Object obj in Selection.objects)
                selection.Add((GameObject)obj);

            while (draggedObjectsStack.Count != 0) {
                GameObject[] arr = (GameObject[])draggedObjectsStack.Pop();
                foreach (GameObject g in arr) {
                    selection.Add(g);

                    if(draggedObjectsStack.Count > 0)
                        Undo.RegisterCreatedObjectUndo(g, "Create " + g.name);
                }
            }
            Selection.objects = selection.ToArray();
            draggedObjectsStack.Clear();
           
            //Debug.Log("stop dragging");
        }

        private static void DestroyLastItemsInStack() {
            if (draggedObjectsStack.Count == 1)
                return;

            GameObject[] items = (GameObject[])draggedObjectsStack.Pop();
            foreach(GameObject g in items)
                Object.DestroyImmediate(g);
        }

        private static void CreateNewItems(Vector3 direction) {
            GameObject[] items = (GameObject[])draggedObjectsStack.Peek();
            GameObject[] newItems = new GameObject[items.Length];
            for(int i = 0; i < items.Length; i++) {
                GameObject g = Object.Instantiate(items[i]);
                g.name = items[i].name;
                g.transform.eulerAngles = new Vector3(Common.RoundToNearest(items[i].transform.eulerAngles.x, 90), Common.RoundToNearest(items[i].transform.eulerAngles.y, 90), Common.RoundToNearest(items[i].transform.eulerAngles.z, 90));
                g.transform.parent = items[i].transform.parent;
                g.transform.position = items[i].transform.position + (direction * (1) * snap);
                newItems[i] = g;
            }
            draggedObjectsStack.Push(newItems);
        }
    }
}