using UnityEngine;
using System;

namespace AfstudeerProject.LevelEditor { 

[Serializable]
public class EditorItem {

        public GameObject Obj;
        public bool YisAlwaysZero;
        public bool rndRot90Degrees;
        public Vector3 SnappingValues;
        public Vector3 PosOffset;
        public Vector3 BoundsSize;
        public Vector3 BoundsOffset;
    }
}