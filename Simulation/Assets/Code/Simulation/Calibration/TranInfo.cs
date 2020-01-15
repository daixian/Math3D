using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 调试为了得到一个transform的4x4矩阵
/// </summary>
public class TranInfo : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Matrix4x4 l2w = transform.localToWorldMatrix;
        Debug.Log(l2w);
        Debug.Log($"{transform.rotation.x},{transform.rotation.y},{transform.rotation.z},{transform.rotation.w}");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
