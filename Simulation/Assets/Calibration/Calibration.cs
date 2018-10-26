using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Calibration : MonoBehaviour
{

    /// <summary>
    /// 棋盘格
    /// </summary>
    public Transform board;

    public Camera cam1;
    public Camera cam2;

    //文件名的计数
    public int count = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnGUI()
    {
        if (GUILayout.Button("采图"))
        {
            CaptrueCamera(cam1, new Rect(0, 0, 1920, 1080), "L" + count);
            CaptrueCamera(cam2, new Rect(0, 0, 1920, 1080), "R" + count);
            count++;
        }

    }


    public Texture2D CaptrueCamera(Camera camera, Rect rect, string filename)
    {
        // 创建一个RenderTexture对象  
        RenderTexture rt = new RenderTexture((int)rect.width, (int)rect.height, 0);
        // 临时设置相关相机的targetTexture为rt, 并手动渲染相关相机  
        camera.targetTexture = rt;
        camera.Render();
        // 激活这个rt, 并从中中读取像素。  
        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(rect, 0, 0);// 注：这个时候，它是从RenderTexture.active中读取像素  
        screenShot.Apply();

        // 重置相关参数，以使用camera继续在屏幕上显示  
        camera.targetTexture = null;
        //ps: camera2.targetTexture = null;  
        RenderTexture.active = null; // JC: added to avoid errors  
        GameObject.Destroy(rt);
        string formatType = ".png";
        string formatTime = DateTime.Now.ToString();
        formatTime = formatTime.Replace('/', '-').Replace(' ', '-').Replace(':', '-').ToString();
        // 最后将这些纹理数据，成一个png图片文件  
        byte[] bytes = screenShot.EncodeToPNG();
#if UNITY_ANDROID
                filename = Application.persistentDataPath + "/"  + formatType;
                Debug.Log("android!");
#elif UNITY_IPHONE
                filename = Application.persistentDataPath + "/" + formatType;
                Debug.Log("ios");
#elif UNITY_EDITOR
        filename = Path.Combine(filename + formatType);
        Debug.Log("editor");
#endif
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("截屏了一张照片: {0}", filename));

        return screenShot;
    }

}
