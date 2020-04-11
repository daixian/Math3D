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
        /// 所有物体的列表
        /// </summary>
        private List<GameObject> _listObj = new List<GameObject>();


        void Awake()
        {
            xuexue.json.U3DJsonSetting.SetDefault();

            Config.Inst.Load();
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
            if (scene != null || scene.objects != null)
            {
                Debug.LogWarning("Observe.LoadFile():Json反序列化失败,场景对象为null!");
            }
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
                    this.AddComponentWithJsonObj(co.components[i], go);
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

            //尝试给颜色赋值，寻找子物体下面的组件
            Renderer rnd = go.GetComponentInChildren<Renderer>();
            if (rnd != null)
            {
                rnd.material.color = co.color;
            }

            go.SetActive(co.isActive);
            _listObj.Add(go);//记录这个添加的物体
        }

        private void AddComponentWithJsonObj(cvComponent com, GameObject go)
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