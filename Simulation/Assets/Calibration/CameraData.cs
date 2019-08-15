using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using xuexue.LitJson;

namespace DTO
{
    [xuexueJsonClass]
    public class CameraData
    {
        public Matrix4x4 projectionMatrix;
        public Matrix4x4 worldToCameraMatrix;
        [xuexueJsonIgnore]
        public Matrix4x4 cameraToWorldMatrix;

    }
}
