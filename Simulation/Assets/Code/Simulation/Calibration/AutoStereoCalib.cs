using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
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

        public Text uiText;

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
            if (ts.TotalSeconds > 5 && client.isConnected)
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
            if (board == null)
            {
                return;
            }

            AutoCalibDto dto = JsonConvert.DeserializeObject<AutoCalibDto>(msg);
            uiText.text = dto.cmdID.ToString();
            if (dto.reset)
            {
                board.position = Vector3.zero;
                board.rotation = Quaternion.Euler(0, 0, dto.rotateAngle);
            }
            else
            {
                board.position = board.position + new Vector3(dto.moveTo.x, dto.moveTo.y, 0);

                //board.Rotate(new Vector3(0, 0, -1), dto.rotateAngle);
            }
        }

        private void OnDestroy()
        {
            client.Close();
        }



    }
}
