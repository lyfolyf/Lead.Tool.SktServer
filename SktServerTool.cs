using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lead.Tool.Interface;
using System.IO;
using Lead.Tool.XML;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.ComponentModel;
using Lead.Tool.Log;

namespace Lead.Tool.SktServer
{
    public class ConnectedSocketInfo
    {
        public bool IsConnected { get; set; }
        public string NetName { get; set; }
        public string Point { get; set; }
        public Socket Socket { get; set; }
    }


    public class ServerMsgInfo
    {
        public string NetName = "";
        public string Point = "";
        public object Context = null;
    }
    public delegate void MesRecved(string SktName,string mes);


    public class SktSeverTool : ITool
    {
        public Config _Config;
        public string _Path = "";
        public event MesRecved MesEvent = null;

        private ConfigUI _configUI;
        private DebugUI _debugUI;
        private IToolState _State = IToolState.ToolMin;

        private Socket _socketListen = null;
        private IPAddress _ip;
        private IPEndPoint _port;

        private Thread _sendTask = null;
        private Thread _listenTask = null;

        private bool isNeedCloseRevTak = false;
        private bool isNeedCloseSendTak = false;
        private bool isNeedCloseListenTak = false;

        private Queue<ServerMsgInfo> _sendQueue = null;
        private object _sendQueueMutex = new object();
        private object _revQueueMutex = new object();

        //连接的客户端
        public BindingList<ConnectedSocketInfo> _dicPointSocket = new BindingList<ConnectedSocketInfo>();
        private List<Thread> _listRevThread = new List<Thread>();

        private bool _IsConneted = false;
        private int SendQueueCnt = 0;
        private System.Threading.Timer time = null;
        private System.Threading.Timer timeHeart = null;

        public SktSeverTool(string Name, string path)
        {
            _Path = path;
            if (File.Exists(path))
            {
                _Config = (Config)XmlSerializerHelper.ReadXML(path, typeof(Config));
            }
            else
            {
                _Config = new Config();
            }
            _Config.ToolName = "SktSever";

            _configUI = new ConfigUI(this);
            _debugUI = new DebugUI(this);
        }

        #region Common
        public IToolState State
        {
            get { return _State; }
        }

        public Control ConfigUI
        {
            get
            {
                return _configUI;
            }
        }

        public Control DebugUI
        {
            get
            {
                return _debugUI;
            }
        }

        public void Init()
        {
            if (time == null)
            {
                time = new System.Threading.Timer(UpdeteLink, null, 0, 30);
            }
            if (timeHeart == null)
            {
                timeHeart = new System.Threading.Timer(Heart, null, 0, 10000);
            }

            if (_IsConneted)
            {
                MessageBox.Show("已连接，请先断开");
                return;
            }

            _ip = IPAddress.Parse(_Config.IP);

            if (_socketListen == null)
            {
                _socketListen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            _port = new IPEndPoint(_ip, int.Parse(_Config.Port ));

            if (SendQueueCnt <= 0)
            {
                _sendQueue = new Queue<ServerMsgInfo>();
            }
            else
            {
                _sendQueue = new Queue<ServerMsgInfo>(SendQueueCnt);
            }
            _State = IToolState.ToolInit;
        }

        public void Start()
        {
            //socket监听哪个端口

            _socketListen.Bind(_port);

            //同一个时间点过来10个客户端，排队

            _socketListen.Listen(10);

            _IsConneted = true;


            isNeedCloseRevTak = false;
            isNeedCloseSendTak = false;
            isNeedCloseListenTak = false;
            if (_sendTask == null)
            {
                _sendTask = new Thread(CycleSend);
                _sendTask.Start();
            }

            if (_listenTask == null)
            {
                _listenTask = new Thread(CycleListen);
                _listenTask.Start();
            }
            _State = IToolState.ToolRunning;
        }

        public void Terminate()
        {
            _IsConneted = false;
            isNeedCloseRevTak = true;
            isNeedCloseSendTak = true;
            isNeedCloseListenTak = true;

            Thread.Sleep(100);
            foreach (var item in _dicPointSocket)
            {
                if (item.IsConnected)
                {
                    item.Socket.Dispose();
                    item.IsConnected = false;
                }
            }
            Thread.Sleep(100);
            _socketListen.Close();
            _socketListen = null;

            _listenTask.Join();
            _listenTask = null;
            _sendTask.Join();
            _sendTask = null;
            _State = IToolState.ToolTerminate;
            return ;
        }
        #endregion


        #region Send
        public int SendInfoByNetName(string revNetName, object context)
        {
            int iRet = 0;

            lock (_sendQueueMutex)
            {
                ServerMsgInfo msg = new ServerMsgInfo();
                msg.NetName = revNetName;
                msg.Context = context;
                _sendQueue.Enqueue(msg);
            }

            return iRet;
        }
        public int SendInfoByPoint(string revPoint, object context)
        {
            int iRet = 0;

            lock (_sendQueueMutex)
            {
                ServerMsgInfo msg = new ServerMsgInfo();
                msg.Point = revPoint;
                msg.Context = context;
                _sendQueue.Enqueue(msg);
            }

            return iRet;
        }
        #endregion

        #region Cycle
        private void CycleSend()
        {
            while (true)
            {
                if (isNeedCloseSendTak)
                {
                    isNeedCloseSendTak = false;
                    Thread.CurrentThread.Abort();
                }

                if (!_IsConneted)
                {
                    Thread.Sleep(30);
                    continue;
                }


                if (_sendQueue.Count < 1)
                {
                    Thread.Sleep(2);
                    continue;
                }

                ServerMsgInfo sendMsgInfo = null;

                lock (_sendQueueMutex)
                {
                    sendMsgInfo = _sendQueue.Dequeue();
                }

                if (sendMsgInfo == null)
                {
                    Thread.Sleep(2);
                    continue;
                }

                try
                {
                    foreach (var item in _dicPointSocket)
                    {
                        if (item.Point == (sendMsgInfo.Point))
                        {
                            Socket sendSocket = item.Socket;
                            byte[] buffer = Encoding.UTF8.GetBytes((string)sendMsgInfo.Context);
                            sendSocket.Send(buffer);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Logger.Warn(ex.Message);
                    foreach (var item in _dicPointSocket)
                    {
                        if (item.Point == (sendMsgInfo.Point))
                        {
                            item.IsConnected = false;
                        }
                    }
                }

                Thread.Sleep(2);
            }
        }

        private void CycleRev(object socket)
        {
            Socket revSocket = socket as Socket;
            bool Isrun = true;
            while (Isrun)
            {
                if (isNeedCloseRevTak)
                {
                    isNeedCloseRevTak = false;
                    Thread.CurrentThread.Abort();
                }

                try
                {
                    //定义byte数组存放从客户端接收过来的数据
                    byte[] buffer = new byte[1024];

                    //将接收过来的数据放到buffer中，并返回实际接受数据的长度
                    int n = revSocket.Receive(buffer);

                    if (n <= 0)
                    {
                        throw new Exception("收到空字符，连接已断开");
                    }

                    //将字节转换成字符串
                    string words = Encoding.UTF8.GetString(buffer, 0, n);
                    string point = revSocket.RemoteEndPoint.ToString();

                    foreach (var item in _dicPointSocket)
                    {
                        if (item.Point == point)
                        {
                            if (MesEvent != null)
                            {
                                MesEvent(item.Point, words);
                            }
                        }
                        else
                        {
                            ;
                        }
                    }


                }
                catch (Exception ex)
                {
                    Logger.Warn(ex.Message);
                    revSocket.Dispose();
                    foreach (var item in _dicPointSocket)
                    {
                        if (item.Socket == revSocket)
                        {
                            item.IsConnected = false;
                        }
                    }
                    Isrun = false;
                }
                Thread.Sleep(2);
            }
        }

        private void CycleListen()
        {
            while (true)
            {
                try
                {
                    if (isNeedCloseListenTak)
                    {
                        isNeedCloseListenTak = false;
                        Thread.CurrentThread.Abort();
                    }

                    //创建通信用的Socket
                    if (!_IsConneted)
                    {
                        continue;
                    }
                    Socket tSocket = _socketListen.Accept();
                    string point = tSocket.RemoteEndPoint.ToString();


                    _debugUI.AddPoint(new ConnectedSocketInfo() { 
                        NetName = "1",
                        Point = point,
                        Socket = tSocket,
                        IsConnected = true
                    });


                    Thread revThreas = new Thread(CycleRev);
                    _listRevThread.Add(revThreas);

                    revThreas.IsBackground = true;
                    revThreas.Start(tSocket);
                }
                catch (Exception ex)
                {
                    Logger.Warn(ex.Message);
                }

            }
        }

        #endregion

        #region Timer
        private void UpdeteLink(object value)
        {
            foreach (var item in _dicPointSocket)
            {
                if (item.IsConnected == false)
                {
                    _debugUI.RemovePoint(item);
                    break;
                }
            }

        }
        private void Heart(object value)
        {
            Thread.Sleep(1000);
            foreach (var item in _dicPointSocket)
            {
                if (item.IsConnected && _Config.IsHeart)
                {
                    lock (_sendQueueMutex)
                    {
                        SendInfoByPoint(item.Point, "Heart");
                    }

                }

            }
        }

        #endregion

    }
}
