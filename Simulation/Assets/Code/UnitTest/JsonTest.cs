using DTO;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace dxlib
{
    /// <summary>
    /// josn的单元测试
    /// </summary>
    [TestFixture(Description = "Newtonsoft.Json的单元测试")]
    public class JsonTest
    {
        [SetUp]
        public void SetUp()
        {
            //Newtonsoft.Json的U3D扩展类型支持
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


        [Test(Description = "测试一个Vector3能否序列化反序列化")]
        public void Vector3()
        {
            Vector3 o = new Vector3(123, 324, 4353);
            string text = JsonConvert.SerializeObject(o);
            Debug.Log(text);
            Vector3 o2 = JsonConvert.DeserializeObject<Vector3>(text);
            Assert.IsTrue(o == o2);
        }

        [Test(Description = "测试一个Vector3的List能否序列化反序列化")]
        public void Vector3List()
        {
            List<Vector3> o = new List<Vector3> { new Vector3(123, 324, 4353), new Vector3(324, 22, 1123) };
            string text = JsonConvert.SerializeObject(o);
            Debug.Log(text);
            List<Vector3> o2 = JsonConvert.DeserializeObject<List<Vector3>>(text);
            Assert.IsTrue(o.Count == o2.Count);
            for (int i = 0; i < o.Count; i++)
            {
                Assert.IsTrue(o[i] == o2[i]);
            }
        }

        [Test(Description = "测试cvLine继承cvComponent能否序列化反序列化")]
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
            Debug.Log(text);
            cvComponent com = JsonConvert.DeserializeObject<cvComponent>(text);
            Assert.IsTrue(com.type == "cvLine");
            cvLine line2 = com as cvLine;
            Assert.IsTrue(line.pos0 == line2.pos0);
            Assert.IsTrue(line.pos1 == line2.pos1);

        }

        [Test(Description = "测试cvObject能否序列化反序列化")]
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

            Assert.IsTrue(object.ReferenceEquals(obj.children[0], obj2));

            string text = JsonConvert.SerializeObject(obj);
            cvObject objr = JsonConvert.DeserializeObject<cvObject>(text);
            Assert.IsTrue(objr.children.Count == 1);
            Assert.IsTrue(objr.children[0].components.Count == 1);

        }

        [Test(Description = "测试cvScene能否序列化反序列化")]
        public void cvScene()
        {
            string text = JsonConvert.SerializeObject(scene);
            cvScene obj = JsonConvert.DeserializeObject<cvScene>(text);

            Assert.IsTrue(obj.objects.Count == scene.objects.Count);
        }

        [Test(Description = "测试一下loadFile能否工作")]
        public void loadFile()
        {
            string path = @"D:\GC3000\IDE0OVMzVxEAJgAm\images\calib\Camera-F3DSC01.json";
            if (File.Exists(path))
            {
                string text = File.ReadAllText(path);
                cvScene obj = JsonConvert.DeserializeObject<cvScene>(text);

                Assert.IsTrue(obj.objects.Count > -1);
            }
            else
            {
                Debug.Log("loadFile():文件不存在,未执行测试.");
            }
        }
    }
}
