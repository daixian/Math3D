using DTO;
using Newtonsoft.Json;
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
        public string jsonPath = @"../omake/Camera-F3DSC01.json";

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
            xuexue.json.U3DJsonSetting.SetDefault();

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
            path = path.Replace("\"", "");

            if (!File.Exists(path))
            {
                Debug.Log("Observe.LoadFile():文件不存在. " + path);
                return;//如果文件不存在就直接返回
            }

            Config.Inst.AddHistory(path);

            Clear();
            string text = File.ReadAllText(path);
            cvScene scene = JsonConvert.DeserializeObject<cvScene>(text);
            for (int i = 0; i < scene.objects.Count; i++)
            {
                this.AddCvObj(scene.objects[i]);
            }
        }

        /// <summary>
        /// 添加一个cv物体到场景里
        /// </summary>
        /// <param name="co"></param>
        /// <param name="parent"></param>
        private void AddCvObj(cvObject co, GameObject parent = null)
        {
            GameObject go;
            if (co.type > 0 || !string.IsNullOrEmpty(co.prefabName))//如果它不是一个空物体
            {
                GameObject pref = Resources.Load<GameObject>(co.prefabName);
                if (pref != null)//如果支持资源里面有这个物体
                {
                    go = GameObject.Instantiate(pref);
                }
                else
                {
                    go = new GameObject();
                }
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
                go.transform.localPosition = co.position;
                go.transform.localRotation = co.rotation;
            }
            else
            {
                go.transform.position = co.position;
                go.transform.rotation = co.rotation;
            }

            go.transform.localScale = co.localScale;

            if (co.components != null)
            {
                for (int i = 0; i < co.components.Count; i++)
                {
                    this.AddComponentWithcv(co.components[i], go);
                }
            }

            //设置好自己的坐标之后在安排自己的子物体们
            if (co.children != null)
            {
                for (int i = 0; i < co.children.Count; i++)
                {
                    AddCvObj(co.children[i], go);//递归一下
                }
            }

            //尝试给颜色赋值
            Renderer rnd = go.GetComponentInChildren<Renderer>();
            if (rnd != null)
            {
                rnd.material.color = co.color;
            }

            go.SetActive(co.isActive);
            _listObj.Add(go);//记录这个添加的物体
        }

        private void AddComponentWithcv(cvComponent com, GameObject go)
        {
            if (com.GetType() == typeof(cvLine))
            {
                cvLine cl = com as cvLine;

                LineRenderer lr = go.AddComponent<LineRenderer>();
                lr.startWidth = 0.001f;
                lr.endWidth = 0.001f;
                lr.positionCount = 2;
                lr.material = Resources.Load<Material>("white");
                lr.material.color = cl.color;
                lr.SetPositions(new Vector3[] { cl.pos0, cl.pos1 });
            }

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