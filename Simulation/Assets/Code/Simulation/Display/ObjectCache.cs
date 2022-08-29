using DTO;
using Flurl.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace dxlib
{
    /// <summary>
    /// 一个物体的缓存
    /// </summary>
    internal class ObjectCache
    {
        /// <summary>
        /// Cache的物体名字,防止gameObject的name设置.
        /// </summary>
        public string name;

        /// <summary>
        /// u3d场景里的物体
        /// </summary>
        public GameObject gameObject;

        /// <summary>
        /// 是否更新过的,如果没有更新的才隐藏,更新过的就不操作
        /// </summary>
        public bool updated = false;

        /// <summary>
        /// 看Cache名字是否能和json中的匹配.现在物体类型使用#号分割
        /// </summary>
        /// <param name="jsonName"></param>
        /// <returns></returns>
        public bool Match(string jsonName)
        {
            if (name == jsonName)
            {
                return true;
            }
            //#号前表示的是物体类型
            string[] splitGO = name.Split('#');
            string[] splitJsonName = jsonName.Split('#');
            if (string.Equals(splitGO[0], splitJsonName[0]))
            {
                return true;
            }
            return false;
        }

    }
}
