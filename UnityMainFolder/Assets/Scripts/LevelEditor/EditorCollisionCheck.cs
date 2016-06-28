using UnityEngine;
using System.Collections;
using AfstudeerProject.LevelEditor;

[ExecuteInEditMode]
public class EditorCollisionCheck : MonoBehaviour {

    public Bounds Bounds;
    public bool Intersecting;

    private Vector3 offSet;

    public void Init(Vector3 boundsOffset, Vector3 boundsSize) {
        offSet = boundsOffset;
        Bounds = new Bounds(transform.position + offSet, boundsSize);
    }

    private void OnDrawGizmos() {
        if (Bounds != null) {
            Bounds.center = transform.position + offSet;
            
            //if (LevelBuilder.ShowWireBox)
            //    Gizmos.DrawWireCube(Bounds.center, Bounds.size);
        }
    }
}
