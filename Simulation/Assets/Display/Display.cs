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
        public GameObject[] prefab;

        /// <summary>
        /// 所有物体的列表
        /// </summary>
        private List<GameObject> _listObj = new List<GameObject>();

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