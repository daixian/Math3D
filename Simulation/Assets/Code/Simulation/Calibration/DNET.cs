using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace xuexue.DNET
{
    public enum DNetError
    {
        Unknown = -1,
        Ok = 0,
        NotImplemented = 1,
        NotInitialized = 2,
        AlreadyInitialized = 3,
        InvalidParameter = 4,
        InvalidContext = 5,
        InvalidHandle = 6,
        RuntimeIncompatible = 7,
        RuntimeNotFound = 8,
        SymbolNotFound = 9,
        BufferTooSmall = 10,
        SyncFailed = 11,
        OperationFailed = 12,
        InvalidAttribute = 13,
    }

    /// <summary>
    /// 服务器
    /// </summary>
    public class Server
    {
        /// <summary>
        /// 启动服务器.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public void Start(string name, string host, int port)
        {
            Create(name, host, port, out ptrServer);
            if (ptrServer != IntPtr.Zero)
            {
                SetMessageProc(ptrServer,
                    Marshal.GetFunctionPointerForDelegate(tcpMsgProcCallback),
                    Marshal.GetFunctionPointerForDelegate(kcpMsgProcCallback));
                dictServers.Add(ptrServer, this);
            }
            Start(ptrServer);
        }

        /// <summary>
        /// 关闭服务器.
        /// </summary>
        public void Close()
        {
            dictServers.Remove(ptrServer);
            TCPEventMsgProc = null;
            KCPEventMsgProc = null;
            Close(ptrServer);
            ptrServer = IntPtr.Zero;
        }

        /// <summary>
        /// 驱动接收.
        /// </summary>
        public void Update()
        {
            if (ptrServer != IntPtr.Zero)
                Update(ptrServer);
        }

        /// <summary>
        /// 向某个客户端发送数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        public void Send(int id, string msg)
        {
            byte[] data = System.Text.Encoding.Default.GetBytes(msg.ToCharArray());
            if (ptrServer != IntPtr.Zero)
                Send(ptrServer, id, data, data.Length);
        }

        /// <summary>
        /// TCP外部的消息处理事件.
        /// </summary>
        public event Action<Server, int, string> TCPEventMsgProc;

        /// <summary>
        /// KCP外部的消息处理事件.
        /// </summary>
        public event Action<Server, int, string> KCPEventMsgProc;

        /// <summary>
        /// 这个服务器对象指针.
        /// </summary>
        private IntPtr ptrServer = IntPtr.Zero;

        #region 全局静态

        static void TCPMsgProc(IntPtr sender, int id, string message)
        {
            if (dictServers.ContainsKey(sender))
            {
                //如果有这个server的记录
                Server server = dictServers[sender];
                if (server.TCPEventMsgProc != null)
                {
                    try
                    {
                        server.TCPEventMsgProc(server, id, message);//执行事件
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        static void KCPMsgProc(IntPtr sender, int id, string message)
        {
            if (dictServers.ContainsKey(sender))
            {
                //如果有这个server的记录
                Server server = dictServers[sender];
                if (server.KCPEventMsgProc != null)
                {
                    try
                    {
                        server.KCPEventMsgProc(server, id, message);//执行事件
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private static MsgProcCallbackDelegate tcpMsgProcCallback = new MsgProcCallbackDelegate(TCPMsgProc);
        private static MsgProcCallbackDelegate kcpMsgProcCallback = new MsgProcCallbackDelegate(KCPMsgProc);

        private static Dictionary<IntPtr, Server> dictServers = new Dictionary<IntPtr, Server>();

        #endregion

        private const string DllName = "DNET";

        /// <summary>
        /// C++部分的小时处理函数指针
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        public delegate void MsgProcCallbackDelegate(IntPtr sender, int id, string message);

        /// <summary>
        /// u3d设置一个字符串消息的回调函数.
        /// </summary>
        /// <param name="server"></param>
        /// <param name="proc"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "dnServerSetMessageProc", CallingConvention = CallingConvention.StdCall)]
        internal static extern DNetError SetMessageProc(IntPtr server, IntPtr tcpProc, IntPtr kcpProc);

        /// <summary>
        /// 创建服务器端.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="server"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "dnServerCreate", CallingConvention = CallingConvention.StdCall)]
        internal static extern DNetError Create(string name, string host, int port, out IntPtr server);

        /// <summary>
        /// 服务器启动.
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "dnServerStart", CallingConvention = CallingConvention.StdCall)]
        internal static extern DNetError Start(IntPtr server);

        /// <summary>
        /// 服务器关闭.
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "dnServerClose", CallingConvention = CallingConvention.StdCall)]
        internal static extern DNetError Close(IntPtr server);

        /// <summary>
        /// 这个对象需要一直Update来驱动接收.
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "dnServerUpdate", CallingConvention = CallingConvention.StdCall)]
        internal static extern DNetError Update(IntPtr server);

        /// <summary>
        /// 向客户端发送数据.
        /// </summary>
        /// <param name="server"></param>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "dnServerSend", CallingConvention = CallingConvention.StdCall)]
        internal static extern DNetError Send(IntPtr server, int id, byte[] msg, int len);
    }


    /// <summary>
    /// 客户端
    /// </summary>
    public class Client
    {
        /// <summary>
        /// 连接服务器,自己的名字.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public void Connect(string name, string host, int port)
        {
            Create(name, out ptrNative);
            if (ptrNative != IntPtr.Zero)
            {
                SetMessageProc(ptrNative,
                    Marshal.GetFunctionPointerForDelegate(tcpMsgProcCallback),
                    Marshal.GetFunctionPointerForDelegate(kcpMsgProcCallback));
                dictClient.Add(ptrNative, this);
            }
            LogOnError(Connect(ptrNative, host, port), "Connect");
        }

        /// <summary>
        /// 关闭客户端.
        /// </summary>
        public void Close()
        {
            dictClient.Remove(ptrNative);
            TCPEventMsgProc = null;
            KCPEventMsgProc = null;
            LogOnError(Close(ptrNative), "Close");
            ptrNative = IntPtr.Zero;
        }

        /// <summary>
        /// 驱动接收.
        /// </summary>
        public void Update()
        {
            if (ptrNative != IntPtr.Zero)
                LogOnError(Update(ptrNative), "Update");
        }

        /// <summary>
        /// 向服务器发送数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        public void Send(int id, string msg)
        {
            byte[] data = System.Text.Encoding.Default.GetBytes(msg.ToCharArray());
            if (ptrNative != IntPtr.Zero)
                LogOnError(Send(ptrNative, data, data.Length), "Send");
        }

        /// <summary>
        /// TCP外部的消息处理事件.
        /// </summary>
        public event Action<Client, int, string> TCPEventMsgProc;

        /// <summary>
        /// KCP外部的消息处理事件.
        /// </summary>
        public event Action<Client, int, string> KCPEventMsgProc;

        /// <summary>
        /// 这个服务器对象指针.
        /// </summary>
        private IntPtr ptrNative = IntPtr.Zero;

        #region 全局静态

        public static void LogOnError(DNetError pluginError, string functionName)
        {

            if (pluginError != DNetError.Ok)
            {
#if UNITY_EDITOR
                Debug.LogErrorFormat("DNET.{0} returned ZPluginError: {1}",
                    functionName, pluginError);
#else
                Debug.LogErrorFormat("DNET.{0} returned ZPluginError: {1}\n\n{2}",
                    functionName, pluginError, new System.Diagnostics.StackTrace());
#endif
            }

        }


        static void TCPMsgProc(IntPtr sender, int id, string message)
        {
            if (dictClient.ContainsKey(sender))
            {
                //如果有这个client的记录
                Client client = dictClient[sender];
                if (client.TCPEventMsgProc != null)
                {
                    try
                    {
                        client.TCPEventMsgProc(client, id, message);//执行事件
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        static void KCPMsgProc(IntPtr sender, int id, string message)
        {
            if (dictClient.ContainsKey(sender))
            {
                //如果有这个client的记录
                Client client = dictClient[sender];
                if (client.KCPEventMsgProc != null)
                {
                    try
                    {
                        client.KCPEventMsgProc(client, id, message);//执行事件
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private static MsgProcCallbackDelegate tcpMsgProcCallback = new MsgProcCallbackDelegate(TCPMsgProc);
        private static MsgProcCallbackDelegate kcpMsgProcCallback = new MsgProcCallbackDelegate(KCPMsgProc);

        private static Dictionary<IntPtr, Client> dictClient = new Dictionary<IntPtr, Client>();

        #endregion

        private const string DllName = "DNET";

        /// <summary>
        /// C++部分的小时处理函数指针
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        public delegate void MsgProcCallbackDelegate(IntPtr sender, int id, string message);

        /// <summary>
        /// u3d设置一个字符串消息的回调函数.
        /// </summary>
        /// <param name="server"></param>
        /// <param name="proc"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "dnClientSetMessageProc", CallingConvention = CallingConvention.StdCall)]
        internal static extern DNetError SetMessageProc(IntPtr client, IntPtr tcpProc, IntPtr kcpProc);

        /// <summary>
        /// 创建客户端端.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="server"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "dnClientCreate", CallingConvention = CallingConvention.StdCall)]
        internal static extern DNetError Create(string name, out IntPtr server);

        /// <summary>
        /// 客户端启动.
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "dnClientConnect", CallingConvention = CallingConvention.StdCall)]
        internal static extern DNetError Connect(IntPtr client, string host, int port);

        /// <summary>
        /// 客户端关闭.
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "dnClientClose", CallingConvention = CallingConvention.StdCall)]
        internal static extern DNetError Close(IntPtr client);

        /// <summary>
        /// 这个对象需要一直Update来驱动接收.
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "dnClientUpdate", CallingConvention = CallingConvention.StdCall)]
        internal static extern DNetError Update(IntPtr client);

        /// <summary>
        /// 向客户端发送数据.
        /// </summary>
        /// <param name="server"></param>
        /// <param name="msg"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "dnClientSend", CallingConvention = CallingConvention.StdCall)]
        internal static extern DNetError Send(IntPtr server, byte[] msg, int len);
    }
}
