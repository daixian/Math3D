using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeDisplay : MonoBehaviour
{
    public Text timeText;

    void Update()
    {
        //yyyy-MM-dd HH:
        timeText.text = DateTime.Now.ToString("mm:ss.fff");
    }
}
