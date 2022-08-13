using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GyroTest : MonoBehaviour
{
    //ԭʼ֡����������ת
    public Transform A0;

    //��ǰ����������ת
    public Transform A1;


    public Transform UT0;

    //ԭʼ�Ĺ�ѧλ��
    public Transform B0;

    //Ҫ��������ڵĹ�ѧλ��
    public Transform B1;

    Matrix4x4 T;

    // Start is called before the first frame update
    void Start()
    {
        //Ŀ��,��һ��UT0�任��A1��λ��ȥ
        Matrix4x4 trsA0 = Matrix4x4.TRS(A0.position, A0.rotation, Vector3.one);
        Matrix4x4 trsA1 = Matrix4x4.TRS(A1.position, A1.rotation, Vector3.one);

        Debug.Log(trsA1.ToString());

        //����������A0��һ��������,�����������λ����A1��λ����,T������������嵽A0����ռ�ı任����
        //������trsA1 = trsA0 * T ��������.
        T = trsA0.inverse * trsA1;

        //���������ǵ����������Թ�ϵ�͹�ѧ���������ϵ��һ�µ�

        //�鿴��֤temp=trsA1
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
