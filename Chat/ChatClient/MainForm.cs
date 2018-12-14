using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class MainForm : Form
    {
        //ClientSocket socket = new ClientSocket();
        CLIENT_STATE ClientState = CLIENT_STATE.NONE;

        ClientSimpleTcp Network = new ClientSimpleTcp();

        bool IsNetworkThreadRunning = false;
        bool IsBackGroundProcessRunning = false;

        System.Threading.Thread NetworkReadThread = null;
        System.Threading.Thread NetworkSendThread = null;

        PacketBufferManager PacketBuffer = new PacketBufferManager();
        Queue<PacketData> RecvPacketQueue = new Queue<PacketData>();
        Queue<byte[]> SendPacketQueue = new Queue<byte[]>();

        Timer dispatcherUITimer = new Timer();


        public MainForm()
        {
            InitializeComponent();

            PacketBuffer.Init((8096 * 10), CSBaseLib.PacketDef.PACKET_HEADER_SIZE, 1024);

            IsNetworkThreadRunning = true;
            NetworkReadThread = new System.Threading.Thread(this.NetworkReadProcess);
            NetworkReadThread.Start();
            NetworkSendThread = new System.Threading.Thread(this.NetworkSendProcess);
            NetworkSendThread.Start();

            IsBackGroundProcessRunning = true;
            dispatcherUITimer.Tick += new EventHandler(BackGroundProcess);
            dispatcherUITimer.Interval = 100;
            dispatcherUITimer.Enabled = true; 
        }

        void BackGroundProcess(object sender, EventArgs e)
        {
            ProcessLog();

            try
            {
                var packet = new PacketData();

                lock (((System.Collections.ICollection)RecvPacketQueue).SyncRoot)
                {
                    if (RecvPacketQueue.Count() > 0)
                    {
                        packet = RecvPacketQueue.Dequeue();
                    }
                }

                if (packet.PacketID != 0)
                {
                    PacketProcess(packet);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("ReadPacketQueueProcess. error:{0}", ex.Message));
            }
        }

        private void ProcessLog()
        {
            // 너무 이 작업만 할 수 없으므로 일정 작업 이상을 하면 일단 패스한다.
            int logWorkCount = 0;

            while (IsBackGroundProcessRunning)
            {
                System.Threading.Thread.Sleep(1);

                string msg;

                if (DevLog.GetLog(out msg))
                {
                    ++logWorkCount;

                    if (listBoxLog.Items.Count > 512)
                    {
                        listBoxLog.Items.Clear();
                    }

                    listBoxLog.Items.Add(msg);
                    listBoxLog.SelectedIndex = listBoxLog.Items.Count - 1;
                }
                else
                {
                    break;
                }

                if (logWorkCount > 8)
                {
                    break;
                }
            }
        }

        void NetworkReadProcess()
        {
            const Int16 PacketHeaderSize = CSBaseLib.PacketDef.PACKET_HEADER_SIZE;

            while (IsNetworkThreadRunning)
            {
                if (Network.IsConnected() == false)
                {
                    System.Threading.Thread.Sleep(1);
                    continue;
                }

                var recvData = Network.Receive();

                if (recvData != null)
                {
                    PacketBuffer.Write(recvData.Item2, 0, recvData.Item1);

                    while (true)
                    {
                        var data = PacketBuffer.Read();
                        if (data.Count < 1)
                        {
                            break;
                        }

                        var packet = new PacketData();
                        packet.DataSize = (short)(data.Count - PacketHeaderSize);
                        packet.PacketID = BitConverter.ToInt16(data.Array, data.Offset + 2);
                        packet.Type = (SByte)data.Array[(data.Offset + 4)];
                        packet.BodyData = new byte[packet.DataSize];
                        Buffer.BlockCopy(data.Array, (data.Offset + PacketHeaderSize), packet.BodyData, 0, (data.Count - PacketHeaderSize));
                        lock (((System.Collections.ICollection)RecvPacketQueue).SyncRoot)
                        {
                            RecvPacketQueue.Enqueue(packet);
                        }
                    }
                    //DevLog.Write($"받은 데이터: {recvData.Item2}", LOG_LEVEL.INFO);
                }
                else
                {
                    Network.Close();
                    SetDisconnectd();
                    DevLog.Write("서버와 접속 종료 !!!", LOG_LEVEL.INFO);
                }
            }
        }

        void NetworkSendProcess()
        {
            while (IsNetworkThreadRunning)
            {
                System.Threading.Thread.Sleep(1);

                if (Network.IsConnected() == false)
                {
                    continue;
                }

                lock (((System.Collections.ICollection)SendPacketQueue).SyncRoot)
                {
                    if (SendPacketQueue.Count > 0)
                    {
                        var packet = SendPacketQueue.Dequeue();
                        Network.Send(packet);
                    }
                }
            }
        }

        public void SetDisconnectd()
        {
            ClientState = CLIENT_STATE.NONE;
            /*if (btnConnect.Enabled == false)
            {
                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;
            }*/

            SendPacketQueue.Clear();

            //labelStatus.Text = "서버 접속이 끊어짐";
        }




        // 서버에 접속
        private void button2_Click(object sender, EventArgs e)
        {
            string address = textBoxIP.Text;

            if (checkBoxLocalHostIP.Checked)
            {
                address = "127.0.0.1";
            }

            int port = Convert.ToInt32(textBoxPort.Text);

            if (Network.Connect(address, port))
            {
                labelConnState.Text = string.Format("{0}. 서버에 접속 중", DateTime.Now);
                ClientState = CLIENT_STATE.CONNECTED;
            }
            else
            {
                labelConnState.Text = string.Format("{0}. 서버에 접속 실패", DateTime.Now);
            }
        }

        // 서버 접속 끊기
        private void button3_Click(object sender, EventArgs e)
        {
            ClientState = CLIENT_STATE.NONE;
            SetDisconnectd();
            Network.Close();
        }

        // 로그 인/아웃
        private void buttonLogIn_Click(object sender, EventArgs e)
        {
            if (ClientState == CLIENT_STATE.CONNECTED)
            {
                RequestLogin(textBoxID.Text, textBoxAuthToken.Text);
            }
            else
            {
                MessageBox.Show("잘못된 명령입니다!!!");
            }
        }

        // 방 나가기
        private void button1_Click(object sender, EventArgs e)
        {

        }

        // 방 입장
        private void button4_Click(object sender, EventArgs e)
        {
            var roomNum = textBoxRoomNum.Text;
        }

        // 방 채팅 보내기
        private void button5_Click(object sender, EventArgs e)
        {

        }

        void PrintLog(string logMsg)
        {
            if (listBoxLog.Items.Count > 20)
            {
                listBoxLog.Items.Clear();
            }

            listBoxLog.Items.Add(logMsg);
            listBoxLog.SelectedIndex = listBoxLog.Items.Count - 1;
        }

        void RequestLogin(string userID, string authToken)
        {
            PrintLog("서버에 로그인 요청");

            var reqLogin = new CSBaseLib.PKTReqLogin() { UserID = userID, AuthToken = authToken };

            var Body = MessagePackSerializer.Serialize(reqLogin);
            var sendData = CSBaseLib.PacketToBytes.Make(CSBaseLib.PACKETID.REQ_LOGIN, Body);
            PostSendPacket(sendData);
            //await Task.Run(() => socket.s_write(sendData));

            
            //Tuple<int, byte[]> recvData = null;
            //await Task.Run(() => recvData = socket.s_read());

            //if (recvData != null)
            //{
            //    var packetData = CSBaseLib.PacketToBytes.ClientReceiveData(recvData.Item1, recvData.Item2);
            //    PacketProcess(packetData.Item1, packetData.Item2);
            //}
        }

        public void PostSendPacket(byte[] sendData)
        {
            if (Network.IsConnected() == false)
            {
                DevLog.Write("서버 연결이 되어 있지 않습니다", LOG_LEVEL.ERROR);
                return;
            }
            
            SendPacketQueue.Enqueue(sendData);
        }


        //async void RequestLobbyList()
        //{
        //listViewLobbyList.Items.Clear();

        //byte[] Body = Encoding.Unicode.GetBytes(echoMsg);

        //List<byte> dataSource = new List<byte>();
        //dataSource.AddRange(BitConverter.GetBytes((Int32)PACKETID.REQ_ECHO));
        //dataSource.AddRange(BitConverter.GetBytes((Int16)1));
        //dataSource.AddRange(BitConverter.GetBytes((Int16)2));
        //dataSource.AddRange(BitConverter.GetBytes(Body.Length));
        //dataSource.AddRange(Body);

        //await Task.Run(() => socket.s_write(dataSource.ToArray()));

        //labelSendEcho.Text = string.Format("{0}: {1}", DateTime.Now, echoMsg);


        //Tuple<int, byte[]> recvData = null;
        //await Task.Run(() => recvData = socket.s_read());

        //if (recvData != null)
        //{
        //    var arySeg = new ArraySegment<byte>(recvData.Item2, 8, (recvData.Item1 - 8));
        //    string msg = System.Text.Encoding.GetEncoding("utf-16").GetString(arySeg.ToArray());
        //    textBoxRecvEcho.Text = msg;
        //}
        //}

        void PacketProcess(PacketData packet)
        {
            switch(packet.PacketID)
            {
                case (int)CSBaseLib.PACKETID.RES_LOGIN:
                    {
                        var resData = MessagePackSerializer.Deserialize<CSBaseLib.PKTResLogin>(packet.BodyData);

                        if (resData.Result == (short)CSBaseLib.ERROR_CODE.NONE)
                        {
                            ClientState = CLIENT_STATE.LOGIN;
                            PrintLog("로그인 성공");
                            buttonLogIn.Text = "Logout";
                        }
                        else
                        {
                            PrintLog(string.Format("로그인 실패: {0} {1}", resData.Result, resData.Result.ToString()));
                        }
                    }
                    break;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Network.Close();

            IsNetworkThreadRunning = false;
            IsBackGroundProcessRunning = false;

            if(NetworkReadThread.IsAlive)
            {
                NetworkReadThread.Join();
            }
            
            if(NetworkSendThread.IsAlive)
            {
                NetworkSendThread.Join();
            }            
        }
    }

    enum CLIENT_STATE
    {
        NONE        = 0,
        CONNECTED   = 1,
        LOGIN       = 2,
        LOBBY       = 3
    }

    struct PacketData
    {
        public Int16 DataSize;
        public Int16 PacketID;
        public SByte Type;
        public byte[] BodyData;
    }
}
