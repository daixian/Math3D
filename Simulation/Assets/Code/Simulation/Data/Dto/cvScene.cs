using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DTO
{
    /// <summary>
    /// 组件基类
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class cvComponent
    {
        /// <summary>
        /// 组件类型
        /// </summary>
        public string type;
    }

    /// <summary>
    /// Line组件
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class cvLine : cvComponent
    {
        /// <summary> 颜色 </summary>
        public Color32 color;

        /// <summary> 点0. </summary>
        public Vector3 pos0;

        /// <summary> 点1. </summary>
        public Vector3 pos1;
    }

    [JsonObject(MemberSerialization.Fields)]
    public class cvObject
    {
        /// <summary> 物体名字. </summary>
        public string name;

        /// <summary> prefab的名字. </summary>
        public string prefabName;

        /// <summary> 物体类型. </summary>
        public int type = 0;

        /// <summary> 颜色 </summary>
        public Color32 color;

        /// <summary> 世界坐标. </summary>
        public Vector3 position;

        /// <summary> 旋转. </summary>
        public Quaternion rotation;

        /// <summary> 本地缩放. </summary>
        public Vector3 localScale;

        /// <summary> 这个物体是否在u3d里默认是显示的. </summary>
        [JsonProperty(PropertyName = "isActive")]
        public bool isActive = false;

        /// <summary> 上面的坐标旋转是否是本地坐标 <summary>
        [JsonProperty(PropertyName = "isLocal")]
        public bool isLocal = false;

        /// <summary> 是否覆盖预制体材质 </summary>
        [JsonProperty(PropertyName = "isColor")]
        public bool isColor = true;

        /// <summary> 所有包含的组件. </summary>
        public List<cvComponent> components = new List<cvComponent>();

        /// <summary> 子节点. </summary>
        public List<cvObject> children = new List<cvObject>();
    }

    [JsonObject(MemberSerialization.Fields)]
    public class cvScene
    {    // 场景信息
        public string info;

        // 场景的标记戳
        public string stamp;

        public List<cvObject> objects = new List<cvObject>();
    }

    #region JsonConverter

    /// <summary>
    /// cvComponent的json转换
    /// </summary>
    class cvComponentConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var obj = (cvComponent)value;
            if (obj.type == "cvLine" || obj.type == "Line")//目前有两个版本的代码一个用的是cvLine,一个用的是Line
            {
                cvLine line = (cvLine)value;
                serializer.Serialize(writer, line);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            if (jo.ContainsKey("type"))
            {
                string t = (string)jo["type"];
                if (t == "cvLine" || t == "Line")//目前有两个版本的代码一个用的是cvLine,一个用的是Line
                {
                    return jo.ToObject<cvLine>();
                }
            }
            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(cvComponent))
            {
                return true;
            }
            return false;
        }
    }

    #endregion
}
