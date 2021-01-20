using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using xuexue.DNET;

namespace dxlib
{
    /// <summary>
    /// 自动立体标定
    /// </summary>
    public class AutoStereoCalib : MonoBehaviour
    {
        Client client = new Client();

        private void Awake()
        {
            client.EventMsgProc += OnEventMsgProc;
            client.Connect("AutoStereoCalib_U3D", "127.0.0.1", 42017);
        }

        private void Start()
        {

        }

        private void Update()
        {
            client.Update();



        }

        private void OnEventMsgProc(Client sender, int id, string msg)
        {
            Debug.Log("处理消息id={id},msg={msg}");
        }

        private void OnDestroy()
        {
            client.Close();
        }



    }
}
