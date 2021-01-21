using Newtonsoft.Json;
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

        // 标定板
        public Transform board;

        private void Awake()
        {
            client.TCPEventMsgProc += OnTCPEventMsgProc;
            client.KCPEventMsgProc += OnKCPEventMsgProc;
            client.Connect("AutoStereoCalib_U3D", "127.0.0.1", 42017);
        }

        private void Start()
        {

        }

        private void Update()
        {
            client.Update();


            TimeSpan ts = DateTime.Now.Subtract(lastTime);
            if (ts.TotalSeconds > 5)
            {
                lastTime = DateTime.Now;
                client.Send(0, "❤~");
            }

        }

        DateTime lastTime;

        private void OnTCPEventMsgProc(Client sender, int id, string msg)
        {
            Debug.Log($"处理TCP消息id={id},msg={msg}");
        }

        private void OnKCPEventMsgProc(Client sender, int id, string msg)
        {
            Debug.Log($"处理KCP消息id={id},msg={msg}");
            AutoCalibDto dto = JsonConvert.DeserializeObject<AutoCalibDto>(msg);
            if (board != null)
            {
                board.position = board.position + new Vector3(dto.moveTo.x, dto.moveTo.y, 0);
            }
        }

        private void OnDestroy()
        {
            client.Close();
        }



    }
}
