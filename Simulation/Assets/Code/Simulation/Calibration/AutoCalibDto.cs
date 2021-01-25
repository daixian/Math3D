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
        // 命令的序号
        public int cmdID = 0;

        // 是否是重置
        public bool reset = false;

        // 是否显示标定板
        public bool isEnable = false;

        // 移动的方向坐标
        public Vector2 moveTo;

        //旋转的角度(度数)
        public float rotateAngle = 0;
    }
}
