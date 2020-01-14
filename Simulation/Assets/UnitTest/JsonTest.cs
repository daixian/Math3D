using DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using xuexue.unity.utility.test;

namespace dxlib
{
    /// <summary>
    /// josn的单元测试
    /// </summary>
    [TestClass]
    class JsonTest
    {
        public JsonTest()
        {
            xuexue.json.U3DJsonSetting.SetDefault();
        }

        [TestMethod]
        void Vector3()
        {
            Vector3 o = new Vector3(123, 324, 4353);
            string text = JsonConvert.SerializeObject(o);
            Vector3 o2 = JsonConvert.DeserializeObject<Vector3>(text);
            Debug.Assert(o == o2);
        }

        [TestMethod]
        void Vector3List()
        {
            List<Vector3> o = new List<Vector3> { new Vector3(123, 324, 4353), new Vector3(324, 22, 1123) };
            string text = JsonConvert.SerializeObject(o);
            List<Vector3> o2 = JsonConvert.DeserializeObject<List<Vector3>>(text);
            Debug.Assert(o.Count == o2.Count);
            for (int i = 0; i < o.Count; i++)
            {
                Debug.Assert(o[i] == o2[i]);
            }
        }

        [TestMethod]
        void cvLine2cvComponent()
        {
            cvLine line = new cvLine()
            {
                type = "cvLine",
                color = new Color32(0, 0, 0, 255),
                pos0 = new Vector3(0, 0, 1),
                pos1 = new Vector3(123, 32, 3)
            };
            string text = JsonConvert.SerializeObject(line);
            cvComponent com = JsonConvert.DeserializeObject<cvComponent>(text);
            Debug.Assert(com.type == "cvLine");
            cvLine line2 = com as cvLine;
            Debug.Assert(line.pos0 == line2.pos0);
            Debug.Assert(line.pos1 == line2.pos1);

        }

    }
}
