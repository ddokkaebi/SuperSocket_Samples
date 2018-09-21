using MsgPack.Serialization;
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
        ClientSocket socket = new ClientSocket();
        CLIENT_STATE ClientState = CLIENT_STATE.NONE;


        public MainForm()
        {
            InitializeComponent();
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

            if (socket.conn(address, port))
            {
                labelConnState.Text = string.Format("{0}. 서버에 접속 중", DateTime.Now);
                buttonLogInOut.Text = "Login";
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
            buttonLogInOut.Text = "Login";
            ClientState = CLIENT_STATE.NONE;

            socket.close();
        }

        // 로그 인/아웃
        private void buttonLogInOut_Click(object sender, EventArgs e)
        {
            if (ClientState == CLIENT_STATE.CONNECTED)
            {
                RequestLogin(textBoxID.Text, textBoxAuthToken.Text);
            }
            else if (ClientState == CLIENT_STATE.LOGIN)
            {
                RequestLogout();
            }
            else
            {
                MessageBox.Show("잘못된 명령입니다!!!");
            }
        }

        // 로비 나가기
        private void button1_Click(object sender, EventArgs e)
        {

        }

        // 로비 입장
        private void button4_Click(object sender, EventArgs e)
        {

        }

        // 채팅 보내기
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

        async void RequestLogin(string userID, string authToken)
        {
            PrintLog("서버에 로그인 요청");

            var reqLogin = new CSBaseLib.PKTReqLogin() { UserID = userID, AuthToken = authToken };

            var serializer = MessagePackSerializer.Create<CSBaseLib.PKTReqLogin>();
            var Body = serializer.PackSingleObject(reqLogin);
            var sendData = CSBaseLib.PacketToBytes.Make(CSBaseLib.PACKETID.REQ_LOGIN, 0, Body);

            await Task.Run(() => socket.s_write(sendData));

            
            Tuple<int, byte[]> recvData = null;
            await Task.Run(() => recvData = socket.s_read());

            if (recvData != null)
            {
                var packetData = CSBaseLib.PacketToBytes.ClientReceiveData(recvData.Item1, recvData.Item2);
                PacketProcess(packetData.Item1, packetData.Item2);
            }
        }

        async void RequestLogout()
        {
            PrintLog("서버에 로그아웃 요청");


            var sendData = CSBaseLib.PacketToBytes.Make(CSBaseLib.PACKETID.REQ_LOGOUT, 0, null);

            await Task.Run(() => socket.s_write(sendData));


            Tuple<int, byte[]> recvData = null;
            await Task.Run(() => recvData = socket.s_read());

            if (recvData != null)
            {
                var packetData = CSBaseLib.PacketToBytes.ClientReceiveData(recvData.Item1, recvData.Item2);
                PacketProcess(packetData.Item1, packetData.Item2);
            }
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

        void PacketProcess(int packetID, byte[] packetBodyData)
        {
            switch(packetID)
            {
                case (int)CSBaseLib.PACKETID.RES_LOGIN:
                    {
                        var deSerializer = MessagePackSerializer.Create<CSBaseLib.PKTResLogin>();
                        var resData = deSerializer.UnpackSingleObject(packetBodyData);

                        if (resData.Result == (short)CSBaseLib.ERROR_CODE.NONE)
                        {
                            ClientState = CLIENT_STATE.LOGIN;
                            PrintLog("로그인 성공");
                            buttonLogInOut.Text = "Logout";
                        }
                        else
                        {
                            PrintLog(string.Format("로그인 실패: {0} {1}", resData.Result, resData.Result.ToString()));
                        }
                    }
                    break;

                case (int)CSBaseLib.PACKETID.RES_LOGOUT:
                    {
                        var deSerializer = MessagePackSerializer.Create<CSBaseLib.PKTResLogin>();
                        var resData = deSerializer.UnpackSingleObject(packetBodyData);

                        if (resData.Result == (short)CSBaseLib.ERROR_CODE.NONE)
                        {
                            ClientState = CLIENT_STATE.CONNECTED;
                            PrintLog("로그아웃 성공");
                            buttonLogInOut.Text = "Login";
                        }
                        else
                        {
                            PrintLog(string.Format("로그아웃 실패: {0} {1}", resData.Result, resData.Result.ToString()));
                        }
                    }
                    break;
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
}
