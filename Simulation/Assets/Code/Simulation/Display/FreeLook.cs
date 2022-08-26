using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 相机自由观察
/// </summary>
public class FreeLook : MonoBehaviour
{

    public Transform lookAtTarget;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButton(1))//当鼠标长按右键的时候执行
        {

            this.transform.RotateAround(lookAtTarget.position, this.transform.right, Input.GetAxis("Mouse Y") * 80 * Time.deltaTime);
            this.transform.RotateAround(lookAtTarget.position, this.transform.up, Input.GetAxis("Mouse X") * 80 * Time.deltaTime);

            //摄像机LookAt,加上这个第二个参数可以避免翻转角度
            this.transform.LookAt(lookAtTarget.position, this.transform.up);
        }

        //通过鼠标滚轮放大 和 缩放视角
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            this.transform.Translate(this.transform.forward * 10 * Time.deltaTime, Space.World);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            this.transform.Translate(-this.transform.forward * 10 * Time.deltaTime, Space.World);
        }

    }

}