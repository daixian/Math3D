using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dxlib
{
    [JsonObject(MemberSerialization.Fields)]
    public class AutoCalibDto
    {
        // 是否显示标定板
        public bool isEnable = false;

        // 移动的方向坐标
        public Vector2 moveTo;

        //旋转的角度(度数)
        public float rotateAngle = 0;
    }
}
