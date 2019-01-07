using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xuexue.LitJson;

namespace dxlib
{
    /// <summary>
    /// 场景里的一个cvObject
    /// </summary>
    [xuexueJsonClass]
    public class CvObject
    {
        /// <summary> 物体名字. </summary>
        public string name;

        /// <summary> 物体类型. </summary>
        public int type = 0;

        /// <summary> 世界坐标. </summary>
        public double[] position;

        /// <summary> 旋转. </summary>
        public double[] rotation;

        /// <summary> 这个物体的子物体. </summary>
        public CvObject[] children;
    }

    /// <summary>
    /// 场景里的一个cvObject
    /// </summary>
    [xuexueJsonClass]
    public class CvLine
    {
        /// <summary> 物体名字. </summary>
        public string name;

        /// <summary> 物体类型. </summary>
        public int type = 0;

        /// <summary> 点0. </summary>
        public double[] pos0;

        /// <summary> 点1. </summary>
        public double[] pos1;
    }


    /// <summary>
    /// 得到一个cv的场景
    /// </summary>
    [xuexueJsonClass]
    public class CvScene
    {
        public CvObject[] vGameObj;

        public CvLine[] vLine;
    }
}
