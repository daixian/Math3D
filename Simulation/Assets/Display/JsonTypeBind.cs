using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using xuexue.LitJson;

namespace xuexue.common.json
{
    /// <summary>
    /// 这是一个xuexuejson的绑定相关类的实现。
    /// 在程序一启动的时候应该绑定各种支持类型
    /// </summary>
    public class JsonTypeBind
    {
        /// <summary>
        /// 实现只执行一次绑定
        /// </summary>
        private static bool isBind = false;

        /// <summary>
        /// 绑定类型，在程序一运行的最早就应执行。
        /// </summary>
        public static void Bind()
        {
            if (isBind)
            {
                return;
            }
            isBind = true;

            //设置自定义的绑定类型
            JsonTypeRegister.BindType(typeof(Vector3), new xuexueJsonClass("x", "y", "z") { defaultFieldConstraint = false, defaultPropertyConstraint = false });
            JsonTypeRegister.BindType(typeof(Quaternion), new xuexueJsonClass("x", "y", "z", "w") { defaultFieldConstraint = false, defaultPropertyConstraint = false });
            JsonTypeRegister.BindType(typeof(Camera), new xuexueJsonClass("transform", "worldToCameraMatrix", "projectionMatrix") { defaultFieldConstraint = false, defaultPropertyConstraint = false });
            JsonTypeRegister.BindType(typeof(Transform), new xuexueJsonClass("name", "position", "rotation") { defaultFieldConstraint = false, defaultPropertyConstraint = false });

            //设置Json忽略类型
            JsonTypeRegister.AddIgnoreClass(typeof(GameObject));
            //JsonTypeRegister.AddIgnoreClass(typeof(Transform));
            JsonTypeRegister.AddIgnoreClass(typeof(Sprite));
            JsonTypeRegister.AddIgnoreClass(typeof(Texture2D));

        }

    }

}
