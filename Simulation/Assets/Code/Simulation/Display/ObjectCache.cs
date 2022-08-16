using DTO;
using Flurl.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace dxlib
{
    /// <summary>
    /// 一个物体的缓存
    /// </summary>
    internal class ObjectCache
    {
        /// <summary>
        /// u3d场景里的物体
        /// </summary>
        public GameObject gameObject;

        /// <summary>
        /// 是否更新过的,如果没有更新的才隐藏,更新过的就不操作
        /// </summary>
        public bool updated = false;

    }
}
