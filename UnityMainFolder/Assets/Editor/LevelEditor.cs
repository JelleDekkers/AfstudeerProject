using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace AfstudeerProject.LevelEditor {

    [CustomEditor(typeof(LevelBuilder))]
    public class LevelEditor : Editor {

        private LevelBuilder levelBuilder;
        private EditorItem itemCurrentlyPlacing;
        private GameObject gameObjectCurrentlyPlacing;
        private Vector3 mousePos;
        private bool isPlacingMultiple;
        private bool snapping = true;
        private RaycastHit hit;

        public override void OnInspectorGUI() {
            levelBuilder = (LevelBuilder)target;

            if (levelBuilder == null)
                return;

            EditorGUILayout.LabelField("Place items in scene: ", EditorStyles.boldLabel);

            foreach(EditorItem item in levelBuilder.EditorItems) {
                if (item.Obj != null) {
                    if (GUILayout.Button(item.Obj.name)) {
                        SpawnItem(item);
                        FocusOnSceneView();
                        Tools.current = Tool.View;
                    }
                }
            }

            GUILayout.Space(10);
            EditorGUILayout.LabelField("Options: ", EditorStyles.boldLabel);
            snapping = EditorGUILayout.Toggle("Snap Items", snapping);
            LevelBuilder.ShowWireBox = EditorGUILayout.Toggle("Show Items Wire Box", LevelBuilder.ShowWireBox);

            GUILayout.Space(20);
            base.OnInspectorGUI();
        }

        private void OnSceneGUI() {
            if (levelBuilder == null)
                return;

            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            hit = new RaycastHit();
            Event e = Event.current;
            var controlID = GUIUtility.GetControlID(FocusType.Passive);
            var eventType = e.GetTypeForControl(controlID);

            if (itemCurrentlyPlacing != null) {
                if (Physics.Raycast(ray, out hit, 100000f)) {
                    mousePos = new Vector3(hit.point.x, hit.point.y, hit.point.z);

                    //if (Event.current.keyCode == LevelBuilder.DRAG_KEY) {
                    //    DragMultipleItems();
                    //} 

                   
                    if (e.keyCode == LevelBuilder.CANCEL_KEY) 
                        CancelDragging();

                    if (e.button == 0 && eventType == EventType.MouseUp) {
                        bool placeMore = e.shift;
                        PlaceCurrentItem(placeMore);
                    } else {
                        DragCurrentItem();
                    }
                }
            }

            if(Tools.current == Tool.Rotate && levelBuilder.prevPlacedGameObject != null) {
                Selection.activeGameObject = levelBuilder.prevPlacedGameObject;
            }

            SceneViewGUI();
            SceneView.RepaintAll();
        }        

        private void SceneViewGUI() {
            Handles.BeginGUI();
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.white;
            List<string> GUIMessages = new List<string>();
            GUIMessages.Add("Snapping: " + snapping);

            if (itemCurrentlyPlacing != null) {
                if(snapping)
                    GUIMessages.Add("Current snap values: " + itemCurrentlyPlacing.SnappingValues.ToString());

                GUIMessages.Add("Press " + "'" + LevelBuilder.CANCEL_KEY.ToString() + "'" + " to cancel");

                if (isPlacingMultiple == false)
                    GUIMessages.Add("Hold " + "'" + "Shift" + "'" + " to place multiple");
            }

            for(int i = 0; i < GUIMessages.Count; i++)
                GUI.Label(new Rect(10, 10 + (10 * i), 10000, 100), GUIMessages[i], style);

            Handles.EndGUI();
        }

        public void SpawnItem(EditorItem item) {
            if (itemCurrentlyPlacing != null)
                DestroyImmediate(gameObjectCurrentlyPlacing);

            itemCurrentlyPlacing = item;
            gameObjectCurrentlyPlacing = Instantiate(itemCurrentlyPlacing.Obj);
            gameObjectCurrentlyPlacing.name = "Selection: " + itemCurrentlyPlacing.Obj.name;
            gameObjectCurrentlyPlacing.AddComponent<EditorCollisionCheck>().Init(itemCurrentlyPlacing.BoundsOffset, itemCurrentlyPlacing.BoundsSize);

            if (levelBuilder.prevPlacedGameObject != null) {
                if (itemCurrentlyPlacing.rndRot90Degrees) {
                    Vector3 rot = gameObjectCurrentlyPlacing.transform.eulerAngles;
                    gameObjectCurrentlyPlacing.transform.eulerAngles = new Vector3(rot.x, rot.y + (90 * Random.Range(0, 4)), rot.z);
                } else {
                    gameObjectCurrentlyPlacing.transform.eulerAngles = levelBuilder.prevPlacedGameObject.transform.eulerAngles;
                }
            }
        }

        private void DragCurrentItem() {
            if (itemCurrentlyPlacing == null || gameObjectCurrentlyPlacing == null)
                return;

            //if (hit.collider.gameObject != gameObjectCurrentlyPlacing && hit.collider.GetComponent<EditorCollisionCheck>()) {
            //    Bounds objBounds = gameObjectCurrentlyPlacing.GetComponent<EditorCollisionCheck>().Bounds;
            //    Bounds otherBounds = hit.collider.GetComponent<EditorCollisionCheck>().Bounds;

            //    Debug.Log(objBounds.Intersects(otherBounds));
            //}

            //TODO: dit bij de spawn doen, samen met de box collider en gizmos
            //TODO: dit door geven aan EditorCollisionCheck en dan de gizmos rood of groen maken
            Vector3 colSize = itemCurrentlyPlacing.BoundsSize / 2;
            float offset = 0.5f;
            if (colSize.x > offset)
                colSize.x -= offset;
            if (colSize.y > offset)
                colSize.y -= offset;
            if (colSize.z > offset)
                colSize.z -= offset;

            Collider[] cols = Physics.OverlapBox(gameObjectCurrentlyPlacing.transform.position, colSize);
            foreach(Collider col in cols) {
                if(col.transform != levelBuilder.RayCatchPlane && col.transform.IsChildOf(gameObjectCurrentlyPlacing.transform) == false)
                    Debug.Log(col.name);
            }

            Vector3 pos = mousePos;
            if(snapping)
                pos = Snap(itemCurrentlyPlacing.SnappingValues);

            if (itemCurrentlyPlacing.YisAlwaysZero)
                pos = new Vector3(pos.x, 0, pos.z);

            gameObjectCurrentlyPlacing.transform.position = pos;
        }

        private void PlaceCurrentItem(bool placeAgain) {
            gameObjectCurrentlyPlacing.transform.parent = levelBuilder.LevelParent;
            levelBuilder.prevPlacedGameObject = gameObjectCurrentlyPlacing;
            gameObjectCurrentlyPlacing.name = itemCurrentlyPlacing.Obj.name;
            Selection.activeGameObject = levelBuilder.gameObject;
            Undo.RegisterCreatedObjectUndo(gameObjectCurrentlyPlacing, "Create " + gameObjectCurrentlyPlacing.name);
            gameObjectCurrentlyPlacing = null;

            if (placeAgain)
                SpawnItem(itemCurrentlyPlacing);
            else
                itemCurrentlyPlacing = null;
        }

        private void DragMultipleItems() {

        }

        private void CancelDragging() {
            DestroyImmediate(gameObjectCurrentlyPlacing);
            itemCurrentlyPlacing = null;
        }

        private void FocusOnSceneView() {
            SceneView view = (SceneView)SceneView.sceneViews[0];
            view.Focus();
        }

        private Vector3 Snap(Vector3 snapValues) {
            Vector3 pos = new Vector3(Round(mousePos.x, snapValues.x), Round(mousePos.y, snapValues.y), Round(mousePos.z, snapValues.z));
            return pos;
        }

        private float Round(float pos, float snapValue) {
            return snapValue * Mathf.Round((pos / snapValue));
        }
    }
}