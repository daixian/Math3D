using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace DTO
{
    [JsonObject(MemberSerialization.Fields)]
    class cvObject
    {
        /// <summary> 物体名字. </summary>
        public string name;

        /// <summary> prefab的名字. </summary>
        public string prefabName;

        /// <summary> 物体类型. </summary>
        public int type = 0;

        /// <summary> 世界坐标. </summary>
        public Vector3 position;

        /// <summary> 旋转. </summary>
        public Quaternion rotation;

        /// <summary> 本地缩放. </summary>
        public Vector3 localScale;

        /// <summary> 这个物体是否在u3d里默认是显示的. </summary>
        public bool isActive = true;

        /// <summary> 子节点. </summary>
        public List<cvObject> children;
    }

    [JsonObject(MemberSerialization.Fields)]
    class cvScene
    {
        public List<cvObject> objects;
    }
}
