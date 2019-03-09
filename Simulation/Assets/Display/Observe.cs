using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace dxlib
{
    /// <summary>
    /// 读ocv产生的json场景文件，显示对应的场景结果
    /// </summary>
    public class Observe : MonoBehaviour
    {
        /// <summary>
        /// json场景文件路径
        /// </summary>
        public string jsonPath = @"../omake/stereoCalib.json";

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
            Config.Inst.Load();
            LoadResources();
            QualitySettings.vSyncCount = 4;//设置垂直同步来减少cpu占用
        }

        // Use this for initialization
        void Start()
        {
            if (!string.IsNullOrEmpty(Config.Inst.lastJsonScene))
            {
                jsonPath = Config.Inst.lastJsonScene;
            }
            LoadFile(jsonPath);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnApplicationQuit()
        {
            Config.Inst.Save();
        }

        #region 载入json场景

        /// <summary>
        /// 载入一个json场景文件
        /// </summary>
        /// <param name="path"></param>
        public void LoadFile(string path)
        {
            if (!File.Exists(path))
            {
                Debug.Log("Observe.LoadFile():文件不存在. " + path);
                return;//如果文件不存在就直接返回
            }

            Config.Inst.AddHistory(path);

            Clear();
            string str = File.ReadAllText(path);
            CvScene scene = xuexue.LitJson.JsonMapper.ToObject<CvScene>(str);
            for (int i = 0; i < scene.vGameObj.Length; i++)
            {
                this.AddCvObj(scene.vGameObj[i]);
            }
        }

        /// <summary>
        /// 添加一个cv物体到场景里
        /// </summary>
        /// <param name="co"></param>
        /// <param name="parent"></param>
        private void AddCvObj(CvObject co, GameObject parent = null)
        {
            Vector3 pos = new Vector3((float)co.position[0], (float)co.position[1], (float)co.position[2]);
            Quaternion rot = new Quaternion((float)co.rotation[0], (float)co.rotation[1], (float)co.rotation[2], (float)co.rotation[3]);
            Vector3 scale = new Vector3(1, 1, 1);
            if (co.localScale != null)
                scale = new Vector3((float)co.localScale[0], (float)co.localScale[1], (float)co.localScale[2]);

            GameObject go;
            if (co.type >= 0)//type=-1则为空物体
            {
                GameObject pref = prefab[co.type];
                if (pref == null)//如果支持资源里面没有这个物体
                {
                    pref = prefab[(co.type / 100) * 100];//那么就使用这个类型物体的起始类型
                }
                go = GameObject.Instantiate(pref);
            }
            else
            {
                go = new GameObject();
            }

            go.name = co.name;
            if (parent != null)
                go.transform.parent = parent.transform;

            if (co.isLocal)//json里的坐标是否是本地坐标
            {
                go.transform.localPosition = pos;
                go.transform.localRotation = rot;
            }
            else
            {
                go.transform.position = pos;
                go.transform.rotation = rot;
            }
            if (scale != Vector3.zero)//只有当缩放不为0的时候才设置(如果为0认为忽略缩放设置，使用u3d资源里的缩放)
                go.transform.localScale = scale;

            //设置好自己的坐标之后在安排自己的子物体们
            if (co.children != null)
            {
                for (int i = 0; i < co.children.Length; i++)
                {
                    AddCvObj(co.children[i], go);//递归一下
                }
            }

            if (co.lines != null)
            {
                for (int i = 0; i < co.lines.Length; i++)
                {
                    this.AddCvLine(co.lines[i], go);
                }
            }

            go.SetActive(co.isActive);
            _listObj.Add(go);//记录这个添加的物体
        }

        private void AddCvLine(CvLine cl, GameObject go)
        {
            GameObject line = new GameObject(cl.name);
            line.transform.parent = go.transform;
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