using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 画辅助线
/// </summary>
public class Estimate : MonoBehaviour
{

    private void Awake()
    {

    }

    void Start()
    {

    }

    void Update()
    {

    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        //对着两个节点画连线
        if (Selection.transforms.Length == 2)
        {
            Transform tran1 = Selection.transforms[0];
            Transform tran2 = Selection.transforms[1];
            if (tran1 != null && tran2 != null)
            {
                Handles.DrawDottedLine(tran1.position, tran2.position, 0.01f);
                Handles.Label((tran1.position + tran2.position) / 2, $"{(tran1.position - tran2.position).magnitude:F3}", new GUIStyle()
                {
                    fontSize = 9
                });
            }

        }

        //如果只选中了一个物体
        if (Selection.transforms.Length == 1)
        {
            Transform tranRoot = Selection.transforms[0];

            //对着一个大节点画所有的线
            if (tranRoot.childCount >= 2)
            { //如果有子级(子级个数大于1)
                for (int i = 0; i < tranRoot.childCount - 1; i++)
                {
                    Transform tran1 = tranRoot.GetChild(i);
                    Transform tran2 = tranRoot.GetChild(i + 1);
                    if (tran1 != null && tran2 != null)
                    {
                        Vector3 p1 = tran1.position;
                        Vector3 p2 = tran2.position;
                        Handles.DrawLine(p1, p2);
                        Handles.Label((p1 + p2) / 2, $"{(p1 - p2).magnitude:F3}", new GUIStyle()
                        {
                            fontSize = 9
                        });
                    }
                }

            }
            else
            { //如果没有子级
                Handles.DrawLine(Vector3.zero, tranRoot.position);
                Handles.Label(tranRoot.position / 2, $"{tranRoot.position.magnitude:F3}", new GUIStyle()
                {
                    fontSize = 9
                });
            }

        }
    }

#endif
}
