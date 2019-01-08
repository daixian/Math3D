﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xuexue.LitJson;

namespace dxlib
{
    /// <summary>
    /// 配置文件
    /// </summary>
    [xuexueJsonClass]
    public class Config
    {
        /// <summary>
        /// 构造
        /// </summary>
        public Config()
        {
        }

        /// <summary>
        /// 单例
        /// </summary>
        [xuexueJsonIgnore]
        private static Config _instance = new Config();

        /// <summary>
        /// 单例
        /// </summary>
        [xuexueJsonIgnore]
        public static Config Inst
        {
            get { return _instance; }
        }

        #region 字段

        /// <summary>
        /// 历史文件列表(路径),直接暴露出来用，注意线程安全
        /// </summary>
        private List<string> listJsonSceneHistory = new List<string>();

        /// <summary>
        /// 最后一次加载的文件路径
        /// </summary>
        public string lastJsonScene;

        #endregion

        #region 载入保存配置文件

        /// <summary>
        /// 保存配置
        /// </summary>
        public void Save(string path = "./config.json")
        {
            string text = xuexue.LitJson.JsonMapper.ToJson(_instance);
            File.WriteAllText(path, text);
        }

        /// <summary>
        /// 载入配置
        /// </summary>
        public void Load(string path = "./config.json")
        {
            if (!File.Exists(path))
            {
                return;
            }
            string text = File.ReadAllText(path);
            _instance = xuexue.LitJson.JsonMapper.ToObject<Config>(text);
        }

        #endregion

        #region 添加历史记录

        /// <summary>
        /// 只有文件路径不同的时候才加入这个历史记录
        /// </summary>
        /// <param name="filePath"></param>
        public void AddHistory(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            if (!listJsonSceneHistory.Contains(fi.FullName))//只有路径不一样了才记录
            {
                listJsonSceneHistory.Add(filePath);
                if (listJsonSceneHistory.Count > 10)
                {
                    listJsonSceneHistory.RemoveAt(0);
                }
            }

            lastJsonScene = fi.FullName;//记录最后一次的文件
        }

        /// <summary>
        /// 得到所有的历史文件
        /// </summary>
        /// <returns></returns>
        public string[] GetHistory()
        {
            return listJsonSceneHistory.ToArray();
        }

        #endregion

    }
}
