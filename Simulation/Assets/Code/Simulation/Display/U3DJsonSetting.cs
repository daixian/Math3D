using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace xuexue.json
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary> Newtonsoft.Json的U3D扩展类型支持. </summary>
    ///
    /// <remarks> Dx, 2020/1/14. </remarks>
    ///-------------------------------------------------------------------------------------------------
    public class U3DJsonSetting
    {
        /// <summary>
        /// 扩展默认设置支持几个常用的U3D数据类型
        /// </summary>
        public static void SetDefault()
        {
            JsonConvert.DefaultSettings = () =>
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.Formatting = Formatting.Indented;
                settings.MissingMemberHandling = MissingMemberHandling.Ignore;
                settings.Converters.Add(new Vector2Converter());
                settings.Converters.Add(new Vector3Converter());
                settings.Converters.Add(new QuaternionConverter());
                settings.Converters.Add(new Color32Converter());
                settings.Converters.Add(new ColorConverter());

                //组件的转换方法
                settings.Converters.Add(new DTO.cvComponentConverter());
                return settings;
            };
        }


        /// <summary>
        /// Vector2的转换实现
        /// </summary>
        class Vector2Converter : JsonConverter
        {
            [JsonObject(MemberSerialization.OptIn)]
            struct TVector2
            {
                [JsonProperty]
                public float x;
                [JsonProperty]
                public float y;

            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                //这里懒得写Json文本的转换方法了,所以借助了TVector2这个类型
                var obj = (Vector2)value;
                TVector2 tobj = new TVector2() { x = obj.x, y = obj.y };
                serializer.Serialize(writer, tobj);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                TVector2 tObj = serializer.Deserialize<TVector2>(reader);
                return new Vector2(tObj.x, tObj.y);
            }

            public override bool CanConvert(Type objectType)
            {
                if (objectType == typeof(Vector2))
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Vector3的转换实现
        /// </summary>
        class Vector3Converter : JsonConverter
        {
            [JsonObject(MemberSerialization.OptIn)]
            struct TVector3
            {
                [JsonProperty]
                public float x;
                [JsonProperty]
                public float y;
                [JsonProperty]
                public float z;

            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                //这里懒得写Json文本的转换方法了,所以借助了TVector3这个类型
                var obj = (Vector3)value;
                TVector3 tobj = new TVector3() { x = obj.x, y = obj.y, z = obj.z };
                serializer.Serialize(writer, tobj);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                TVector3 tObj = serializer.Deserialize<TVector3>(reader);
                return new Vector3(tObj.x, tObj.y, tObj.z);
            }

            public override bool CanConvert(Type objectType)
            {
                if (objectType == typeof(Vector3))
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        ///  Quaternion的转换实现
        /// </summary>
        class QuaternionConverter : JsonConverter
        {

            [JsonObject(MemberSerialization.OptIn)]
            struct TQuaternion
            {
                [JsonProperty]
                public float x;
                [JsonProperty]
                public float y;
                [JsonProperty]
                public float z;
                [JsonProperty]
                public float w;

            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var obj = (Quaternion)value;
                TQuaternion tobj = new TQuaternion() { x = obj.x, y = obj.y, z = obj.z, w = obj.w };
                serializer.Serialize(writer, tobj);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                TQuaternion tObj = serializer.Deserialize<TQuaternion>(reader);
                return new Quaternion(tObj.x, tObj.y, tObj.z, tObj.w);
            }

            public override bool CanConvert(Type objectType)
            {
                if (objectType == typeof(Quaternion))
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Color32的转换实现
        /// </summary>
        class Color32Converter : JsonConverter
        {
            [JsonObject(MemberSerialization.OptIn)]
            struct TColor32
            {
                [JsonProperty]
                public byte r;
                [JsonProperty]
                public byte g;
                [JsonProperty]
                public byte b;
                [JsonProperty]
                public byte a;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var obj = (Color32)value;
                TColor32 tobj = new TColor32() { r = obj.r, g = obj.g, b = obj.b, a = obj.a };
                serializer.Serialize(writer, tobj);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                TColor32 tObj = serializer.Deserialize<TColor32>(reader);
                return new Color32(tObj.r, tObj.g, tObj.b, tObj.a);
            }

            public override bool CanConvert(Type objectType)
            {
                if (objectType == typeof(Color32))
                {
                    return true;
                }
                return false;
            }
        }


        /// <summary>
        /// Color的转换实现
        /// </summary>
        class ColorConverter : JsonConverter
        {
            [JsonObject(MemberSerialization.OptIn)]
            struct TColor
            {
                [JsonProperty]
                public float r;
                [JsonProperty]
                public float g;
                [JsonProperty]
                public float b;
                [JsonProperty]
                public float a;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var obj = (Color)value;
                TColor tobj = new TColor() { r = obj.r, g = obj.g, b = obj.b, a = obj.a };
                serializer.Serialize(writer, tobj);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                TColor tObj = serializer.Deserialize<TColor>(reader);
                return new Color(tObj.r, tObj.g, tObj.b, tObj.a);
            }

            public override bool CanConvert(Type objectType)
            {
                if (objectType == typeof(Color))
                {
                    return true;
                }
                return false;
            }
        }
    }
}
