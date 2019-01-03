﻿using System.Collections;
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
        public GameObject[] prefab;

        /// <summary>
        /// 根据不同的线类型来画不同颜色的线
        /// </summary>
        public Material[] lineType;

        /// <summary>
        /// 所有物体的列表
        /// </summary>
        private List<GameObject> _listObj = new List<GameObject>();

        private void Awake()
        {
            lineType = new Material[] { Resources.Load<Material>("red"),
                                        Resources.Load<Material>("green"),
                                        Resources.Load<Material>("blue"),
                                        Resources.Load<Material>("white") };
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

        #region 载入场景

        /// <summary>
        /// 载入一个json场景文件
        /// </summary>
        /// <param name="path"></param>
        private void LoadFile(string path)
        {
            Clear();
            string str = File.ReadAllText(@"D:\Work\MRSystem\x64\Release\0GC100GuessWorldCenter_step0.json");
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
        private void AddCvObj(CvObject co)
        {
            GameObject pref = prefab[co.type];
            Vector3 pos = new Vector3((float)co.position[0], (float)co.position[1], (float)co.position[2]);
            Quaternion rot = new Quaternion((float)co.rotation[0], (float)co.rotation[1], (float)co.rotation[2], (float)co.rotation[3]);
            GameObject go = GameObject.Instantiate(pref, pos, rot);
            go.name = co.name;
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