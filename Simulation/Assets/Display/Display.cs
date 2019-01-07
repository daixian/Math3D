using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace dxlib
{
    /// <summary>
    /// 读ocv产生的json场景文件，显示对应的场景结果
    /// </summary>
    public class Display : MonoBehaviour
    {
        /// <summary>
        /// 支持的物体type类型
        /// </summary>
        private GameObject[] prefab = new GameObject[1000];

        /// <summary>
        /// 根据不同的线类型来画不同颜色的线
        /// </summary>
        private Material[] lineType;

        /// <summary>
        /// 所有物体的列表
        /// </summary>
        private List<GameObject> _listObj = new List<GameObject>();

        /// <summary>
        /// 载入所需要的资源
        /// </summary>
        private void LoadResources()
        {
            lineType = new Material[] { Resources.Load<Material>("red"),
                                        Resources.Load<Material>("green"),
                                        Resources.Load<Material>("blue"),
                                        Resources.Load<Material>("white"),
                                        Resources.Load<Material>("cyan"),
                                        Resources.Load<Material>("lightPink"),
                                        Resources.Load<Material>("orange")
            };
            //载入所有的cvObj预制体  ,支持1000个           
            int baseIndex = 0;
            for (int i = 0; i < 1000; i++)
            {
                GameObject obj = Resources.Load<GameObject>("CvObj" + i);//载入所有命名为CvObj的预制体
                if (obj != null)
                    prefab[i] = obj;
                else
                {
                    baseIndex += 100;//提升100到下一种资源物体
                    i = baseIndex - 1;//这里要减一因为i马上要自增1
                }
            }
        }


        void Awake()
        {
            LoadResources();
        }

        // Use this for initialization
        void Start()
        {

            LoadFile("");
        }

        // Update is called once per frame
        void Update()
        {

        }

        #region 载入json场景

        /// <summary>
        /// 载入一个json场景文件
        /// </summary>
        /// <param name="path"></param>
        private void LoadFile(string path)
        {
            Clear();
            string str = File.ReadAllText(@"D:\Work\MRSystem\x64\Release\images\calib\origin\F3D0003\stereoCalib.json");
            CvScene scene = xuexue.LitJson.JsonMapper.ToObject<CvScene>(str);
            for (int i = 0; i < scene.vGameObj.Length; i++)
            {
                this.AddCvObj(scene.vGameObj[i]);
            }

            for (int i = 0; i < scene.vLine.Length; i++)
            {
                this.AddCvLine(scene.vLine[i]);
            }
        }

        /// <summary>
        /// 添加一个cv物体到场景里
        /// </summary>
        /// <param name="co"></param>
        private void AddCvObj(CvObject co, GameObject parent = null)
        {
            Vector3 pos = new Vector3((float)co.position[0], (float)co.position[1], (float)co.position[2]);
            Quaternion rot = new Quaternion((float)co.rotation[0], (float)co.rotation[1], (float)co.rotation[2], (float)co.rotation[3]);

            GameObject go;
            if (co.type >= 0)//type=-1则为空物体
            {
                GameObject pref = prefab[co.type];
                go = GameObject.Instantiate(pref, pos, rot);
            }
            else
            {
                go = new GameObject();
                go.transform.position = pos;
                go.transform.rotation = rot;
            }

            go.name = co.name;
            if (parent != null)
                go.transform.parent = parent.transform;
            for (int i = 0; i < co.children.Length; i++)
            {
                AddCvObj(co.children[i], go);//递归一下
            }
            _listObj.Add(go);//记录这个添加的物体
        }

        private void AddCvLine(CvLine cl)
        {
            GameObject line = new GameObject(cl.name);
            LineRenderer lr = line.AddComponent<LineRenderer>();
            lr.startWidth = 0.001f;
            lr.endWidth = 0.001f;
            lr.positionCount = 2;
            lr.material = this.lineType[cl.type];
            lr.SetPositions(new Vector3[] { new Vector3((float)cl.pos0[0], (float)cl.pos0[1], (float)cl.pos0[2]),
                                            new Vector3((float)cl.pos1[0], (float)cl.pos1[1], (float)cl.pos1[2])});

            _listObj.Add(line);//记录这个添加的物体
        }

        /// <summary>
        /// 清空场景里的物体
        /// </summary>
        private void Clear()
        {
            foreach (var item in _listObj)
            {
                Destroy(item);
            }

        }

        #endregion
    }
}