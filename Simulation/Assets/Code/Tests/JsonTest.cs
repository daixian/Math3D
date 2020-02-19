using DTO;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace dxlib
{
    /// <summary>
    /// josn的单元测试
    /// </summary>
    [TestFixture]
    public class JsonTest
    {
        [SetUp]
        public void SetUp()
        {
            xuexue.json.U3DJsonSetting.SetDefault();

            //初始化一段实验数据.
            cvLine line = new cvLine()
            {
                type = "cvLine",
                color = new Color32(0, 0, 0, 255),
                pos0 = new Vector3(0, 0, 1),
                pos1 = new Vector3(123, 32, 3)
            };
            cvObject obj2 = new cvObject();
            obj2.components.Add(line);

            cvObject obj = new cvObject();
            obj.name = "一个名字";
            obj.prefabName = "pname";
            obj.position = new Vector3(1, 2, 3);
            obj.rotation = Quaternion.Euler(123, 312, 3);
            obj.children.Add(obj2);

            scene.objects.Add(obj);
            scene.objects.Add(obj);
            scene.objects.Add(obj);
        }

        //实验数据
        cvScene scene = new DTO.cvScene();


        [Test]
        public void Vector3()
        {
            Vector3 o = new Vector3(123, 324, 4353);
            string text = JsonConvert.SerializeObject(o);
            Vector3 o2 = JsonConvert.DeserializeObject<Vector3>(text);
            Debug.Assert(o == o2);
        }

        [Test]
        public void Vector3List()
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

        [Test]
        public void cvLine2cvComponent()
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

        [Test]
        public void cvObject()
        {
            cvLine line = new cvLine()
            {
                type = "cvLine",
                color = new Color32(0, 0, 0, 255),
                pos0 = new Vector3(0, 0, 1),
                pos1 = new Vector3(123, 32, 3)
            };

            cvObject obj = new cvObject();
            obj.name = "一个名字";
            obj.prefabName = "pname";
            obj.position = new Vector3(1, 2, 3);
            obj.rotation = Quaternion.Euler(123, 312, 3);
            cvObject obj2 = new cvObject();
            obj2.components.Add(line);
            obj.children.Add(obj2);

            Debug.Assert(object.ReferenceEquals(obj.children[0], obj2));

            string text = JsonConvert.SerializeObject(obj);
            cvObject objr = JsonConvert.DeserializeObject<cvObject>(text);
            Debug.Assert(objr.children.Count == 1);
            Debug.Assert(objr.children[0].components.Count == 1);

        }

        [Test]
        public void cvScene()
        {
            string text = JsonConvert.SerializeObject(scene);
            cvScene obj = JsonConvert.DeserializeObject<cvScene>(text);

            Debug.Assert(obj.objects.Count == scene.objects.Count);
        }
    }
}
