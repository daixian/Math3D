﻿using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

namespace dxlib
{
    [CustomEditor(typeof(dxlib.Observe))]
    public class ObserveEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            dxlib.Observe myScript = (dxlib.Observe)target;
            if (GUILayout.Button("重新载入json场景"))
            {
                myScript.LoadFile(myScript.jsonPath);
            }

            //显示10条历史记录
            string[] history = Config.Inst.GetHistory();
            for (int i = 0; i < 10; i++)
            {
                if (history != null && history.Length > i)
                {
                    FileInfo fi = new FileInfo(history[i]);
                    if (GUILayout.Button(fi.Name))
                    {
                        myScript.LoadFile(fi.FullName);
                    }
                }
            }
        }
    }
}