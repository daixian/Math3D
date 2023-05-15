using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System;

namespace dxlib
{

    [CustomEditor(typeof(dxlib.Observe))]
    public class ObserveEditor : Editor
    {

        private void Awake()
        {
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            dxlib.Observe myScript = (dxlib.Observe)target;
            //在检视面板显示一个按钮
            if (GUILayout.Button("重新载入json场景")) {
                myScript.LoadFile(myScript.jsonPath);
            }
            if (GUILayout.Button("更新Net场景")) {
                myScript.UpdateSceneWithNet();
            }
            if (GUILayout.Button("重设remote地址")) {
                myScript.remoteIP = "127.0.0.1";
            }

            //显示10条历史记录
            string[] history = Config.Inst.GetHistory();
            for (int i = 0; i < 10; i++) {
                if (history != null && history.Length > i) {
                    FileInfo fi = new FileInfo(history[i]);
                    if (GUILayout.Button(fi.Name)) {
                        myScript.LoadFile(fi.FullName);
                        myScript.jsonPath = fi.FullName;
                    }
                }
            }
        }
    }
}
