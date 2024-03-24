using System;
using UnityEngine;

namespace DataUtil
{
    [Serializable]
    public class DataTransform
    {
        public Vector3 pos = Vector3.zero;
        public Quaternion rot = Quaternion.identity;
        public Vector3 scale = Vector3.one;
        public bool isParent = false;
    }
}
