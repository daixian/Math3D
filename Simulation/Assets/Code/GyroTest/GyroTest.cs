using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GyroTest : MonoBehaviour
{
    //原始帧的陀螺仪旋转
    public Transform A0;

    //当前的陀螺仪旋转
    public Transform A1;


    public Transform UT0;

    //原始的光学位置
    public Transform B0;

    //要推算的现在的光学位置
    public Transform B1;

    Matrix4x4 T;

    // Start is called before the first frame update
    void Start()
    {
        //目标,把一个UT0变换到A1的位置去
        Matrix4x4 trsA0 = Matrix4x4.TRS(A0.position, A0.rotation, Vector3.one);
        Matrix4x4 trsA1 = Matrix4x4.TRS(A1.position, A1.rotation, Vector3.one);

        Debug.Log(trsA1.ToString());

        //这个等于如果A0有一个子物体,子物体的世界位置在A1的位置上,T就是这个子物体到A0物体空间的变换矩阵
        //这是由trsA1 = trsA0 * T 推算来的.
        T = trsA0.inverse * trsA1;

        //考虑陀螺仪的子物体的相对关系和光学的子物体关系是一致的

        //查看验证temp=trsA1
        Matrix4x4 temp = trsA0 * T;

        UT0.position = temp.GetPosition();
        UT0.rotation = temp.rotation;

        Debug.Log(temp.ToString());

        //transform.Rotate(new Vector3(1, 0, 0), 45, Space.Self);
        //transform.Rotate(new Vector3(0, 1, 0), 65, Space.Self);

        //Matrix4x4.
    }

    // Update is called once per frame
    void Update()
    {
        Matrix4x4 trsA0 = Matrix4x4.TRS(A0.position, A0.rotation, Vector3.one);
        Matrix4x4 trsA1 = Matrix4x4.TRS(A1.position, A1.rotation, Vector3.one);
        Matrix4x4 trsB0 = Matrix4x4.TRS(B0.position, B0.rotation, Vector3.one);

        //Matrix4x4 temp = trsB0 * T;
        Matrix4x4 trsB1 = trsB0 * trsA0.inverse * trsA1;
        B1.position = trsB1.GetPosition();
        B1.rotation = trsB1.rotation;

    }
}
