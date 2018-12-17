using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//using ServerLogic = ServerLib.ServerLogic;
using ServerLogger = ServerLib.FileLogger;
using DevLog = ServerLib.DevLog;
using ServerEnvironment = ServerLib.ServerEnvironment;
using CommonServerLib;

namespace SocketServerForm
{
    public partial class MainForm : Form
    {
        private void MainForm_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ChatServerLogic.StopServer();
        }


        void InitUIOption()
        {
            listBoxLogLevel.SelectedIndex = 2;
            DevLog.Init((CommonServerLib.LOG_LEVEL)listBoxLogLevel.SelectedIndex);

            모든_UI_Log_출력_중단(checkBoxAllUILogStop.Checked);

            DevLog.EnableDisable(LOG_STRING_TYPE.CONNECTED, checkBoxViewLogConnect.Checked);
            DevLog.EnableDisable(LOG_STRING_TYPE.DISCONNECTED, checkBoxViewLogDisConnect.Checked);
            DevLog.EnableDisable(LOG_STRING_TYPE.CHECK_USER_STATUS, checkBoxViewLogCheckUserStatus.Checked);
            DevLog.EnableDisable(LOG_STRING_TYPE.PACKET_RECEIVED, checkBoxViewLogPacketReceived.Checked);
            DevLog.EnableDisable(LOG_STRING_TYPE.HEART_BEAT, checkBoxViewLogHeartBeat.Checked);
            DevLog.EnableDisable(LOG_STRING_TYPE.BLOCK_USER, checkBoxViewLogBlockUser.Checked);
            DevLog.EnableDisable(LOG_STRING_TYPE.WRONG_USER, checkBoxViewLogWrongUser.Checked);
            DevLog.EnableDisable(LOG_STRING_TYPE.S2S_MESSAGE, checkBoxViewLogS2SMessage.Checked);
            DevLog.EnableDisable(LOG_STRING_TYPE.LOGIN, checkBoxViewLogLogin.Checked);
            DevLog.EnableDisable(LOG_STRING_TYPE.LOBBY_ENTER, checkBoxViewLogEnterLobby.Checked);
            DevLog.EnableDisable(LOG_STRING_TYPE.LOBBY_LEAVE, checkBoxViewLogLeaveLobby.Checked);

            MsgToHostProgram.EnableDisable(InnerMsgType.SERVER_START, true);
            MsgToHostProgram.EnableDisable(InnerMsgType.CREATE_COMPONENT, true);
            MsgToHostProgram.EnableDisable(InnerMsgType.CURRENT_CONNECT_COUNT, checkBoxUserCountUIUpdateStop.Checked);
            MsgToHostProgram.EnableDisable(InnerMsgType.CURRENT_LOBBY_INFO, checkBoxLobbyStatusUIUpdateStop.Checked);
        }

        void CreateLobbyUIData()
        {
            var lobbyID = ServerLib.ServerEnvironment.LobbyStartIndex;

            for (int i = 0; i < ServerLib.ServerEnvironment.LobbyCount; ++i)
            {
                ListViewItem lvi = new ListViewItem((lobbyID + i).ToString());
                lvi.SubItems.Add(0.ToString());
                listViewLobbyInfo.Items.Add(lvi);
            }

            listViewLobbyInfo.Refresh();
        }

        void 모든_UI_Log_출력_중단(bool isStop)
        {
            checkBoxViewLogConnect.Checked = !isStop;
            checkBoxViewLogDisConnect.Checked = !isStop;
            checkBoxViewLogCheckUserStatus.Checked = !isStop;
            checkBoxViewLogPacketReceived.Checked = !isStop;
            checkBoxViewLogHeartBeat.Checked = !isStop;
            checkBoxViewLogBlockUser.Checked = !isStop;
            checkBoxViewLogWrongUser.Checked = !isStop;
            checkBoxViewLogS2SMessage.Checked = !isStop;
            checkBoxViewLogLogin.Checked = !isStop;
            checkBoxViewLogEnterLobby.Checked = !isStop;
            checkBoxViewLogLeaveLobby.Checked = !isStop;
        }


        void SetUserStatusCheckOption()
        {
            ServerLib.UserStatusCheckOption option;
            option.CheckHeartBeat = checkBoxHeartBeat.Checked;
            option.CheckTimeLimitUserAuth = checkBoxTimeLimitUserAuth.Checked;
            option.CheckTimeLimitUserLobby = checkBoxTimeLimitUserLobby.Checked;
            option.CheckHeavyNetworkTrafficUser = checkBoxHeavyNetworkTrafficUser.Checked;

            ChatServerLogic.SetUserStatusCheckOption(option);
        }

        private void listBoxLogLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            var curLogLevel = DevLog.CurrentLogLevel();
            var changeLogLevel = (LOG_LEVEL)listBoxLogLevel.SelectedIndex;

            DevLog.Init(changeLogLevel);

            var logMsg = string.Format("로그레벨 변경. {0} -> {1}", curLogLevel.ToString(), changeLogLevel.ToString());
            DevLog.Write(logMsg, LOG_LEVEL.INFO);
        }

        private void checkBoxViewLogConnect_CheckedChanged(object sender, EventArgs e)
        {
            DevLog.EnableDisable(LOG_STRING_TYPE.CONNECTED, checkBoxViewLogConnect.Checked);
        }

        private void checkBoxViewLogDisConnect_CheckedChanged(object sender, EventArgs e)
        {
            DevLog.EnableDisable(LOG_STRING_TYPE.DISCONNECTED, checkBoxViewLogDisConnect.Checked);
        }

        private void checkBoxViewLogCheckUserStatus_CheckedChanged(object sender, EventArgs e)
        {
            DevLog.EnableDisable(LOG_STRING_TYPE.CHECK_USER_STATUS, checkBoxViewLogCheckUserStatus.Checked);
        }

        private void checkBoxViewLogPacketReceived_CheckedChanged(object sender, EventArgs e)
        {
            DevLog.EnableDisable(LOG_STRING_TYPE.PACKET_RECEIVED, checkBoxViewLogPacketReceived.Checked);
        }

        private void checkBoxViewLogHeartBeat_CheckedChanged(object sender, EventArgs e)
        {
            DevLog.EnableDisable(LOG_STRING_TYPE.HEART_BEAT, checkBoxViewLogHeartBeat.Checked);
        }

        private void checkBoxViewLogBlockUser_CheckedChanged(object sender, EventArgs e)
        {
            DevLog.EnableDisable(LOG_STRING_TYPE.BLOCK_USER, checkBoxViewLogBlockUser.Checked);
        }

        private void checkBoxViewLogWrongUser_CheckedChanged(object sender, EventArgs e)
        {
            DevLog.EnableDisable(LOG_STRING_TYPE.WRONG_USER, checkBoxViewLogWrongUser.Checked);
        }

        private void checkBoxViewLogS2SMessage_CheckedChanged(object sender, EventArgs e)
        {
            DevLog.EnableDisable(LOG_STRING_TYPE.S2S_MESSAGE, checkBoxViewLogS2SMessage.Checked);
        }

        private void checkBoxViewLogLogin_CheckedChanged(object sender, EventArgs e)
        {
            DevLog.EnableDisable(LOG_STRING_TYPE.LOGIN, checkBoxViewLogLogin.Checked);
        }

        private void checkBoxViewLogEnterLobby_CheckedChanged(object sender, EventArgs e)
        {
            DevLog.EnableDisable(LOG_STRING_TYPE.LOBBY_ENTER, checkBoxViewLogEnterLobby.Checked);
        }

        private void checkBoxViewLogLeaveLobby_CheckedChanged(object sender, EventArgs e)
        {
            DevLog.EnableDisable(LOG_STRING_TYPE.LOBBY_LEAVE, checkBoxViewLogLeaveLobby.Checked);
        }

        // UI 업데이트 설정
        private void checkBoxUserCountUIUpdateStop_CheckedChanged(object sender, EventArgs e)
        {
            MsgToHostProgram.EnableDisable(InnerMsgType.CURRENT_CONNECT_COUNT, checkBoxUserCountUIUpdateStop.Checked);
        }

        private void checkBoxLobbyStatusUIUpdateStop_CheckedChanged(object sender, EventArgs e)
        {
            MsgToHostProgram.EnableDisable(InnerMsgType.CURRENT_LOBBY_INFO, checkBoxLobbyStatusUIUpdateStop.Checked);
        }

        private void checkBoxAllUIUpdateStop_CheckedChanged(object sender, EventArgs e)
        {
            모든_UI_Log_출력_중단(checkBoxAllUILogStop.Checked);
        }

        private void checkBoxHeartBeat_CheckedChanged(object sender, EventArgs e)
        {
            SetUserStatusCheckOption();
        }

        private void checkBoxTimeLimitUserAuth_CheckedChanged(object sender, EventArgs e)
        {
            SetUserStatusCheckOption();
        }

        private void checkBoxTimeLimitUserLobby_CheckedChanged(object sender, EventArgs e)
        {
            SetUserStatusCheckOption();
        }

        private void checkBoxHeavyNetworkTrafficUser_CheckedChanged(object sender, EventArgs e)
        {
            SetUserStatusCheckOption();
        }



        // 서버 공지
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxServerNotice.Text))
            {
                DevLog.Write("공지 메시지가 없습니다.", CommonServerLib.LOG_LEVEL.ERROR);
                return;
            }

            string message = string.Format("[공지]: {0}", textBoxServerNotice.Text);
            ChatServerLogic.Dev공지보내기(message);
        }

        // 클라이언트 세션 짜르기
        private void button2_Click(object sender, EventArgs e)
        {
            ChatServerLogic.DisConnect(textBoxDisConnectSessioID.Text);
        }

        // S2S 메시지 보내기
        private void button3_Click(object sender, EventArgs e)
        {
            var server = listBoxS2SServer.SelectedItem.ToString();
            var msgType = listBoxS2SMsgType.SelectedItem.ToString();

            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(msgType) ||
                string.IsNullOrEmpty(textBoxS2SMessage.Text))
            {
                MessageBox.Show("서버 미 선택 or 메시지 타입 미 선택 or 메시지 없음");
                return;
            }

            ChatServerLogic.Dev_SendMessageToChatServers(server, msgType, textBoxS2SMessage.Text);
        }

        // 테스트를 위해 레디스에 게임서버에 로그인 되었을 때의 정보를 임의로 가공해서 저장
        private void button4_Click(object sender, EventArgs e)
        {
            var loginInfo = new CommonServerLib.RedisLib.MemoryDBUserInfo()
            {
                UID = 101,
                Token = textBoxRedisWriteUserAuthToken.Text,
                CV = 0,
                CDV = 0,
            };

            ChatServerLogic.Dev_SaveUserInfo(textBoxRedisWriteUserID.Text, loginInfo);
        }

        private void button테스트_동접변경_Click(object sender, EventArgs e)
        {
            textBoxCurrentUserCount.Text = textBoxTestUserCount.Text;
        }

        // IPC 메시지 보내기
        private void button5_Click(object sender, EventArgs e)
        {
            //var sendData = new ProcessCommunicate.IPCTestMsg { N1 = 1, S1 = textBox1.Text };
            //var jsonFormat = Util.ToJsonFormatString<ProcessCommunicate.IPCTestMsg>(sendData);
            //var packet = new ProcessCommunicate.IPCPacket { PacketIndex = 1, JsonFormat = jsonFormat };

            //var result = IPCCommu.ServerSend(packet);
            //if(result == false)
            //{
            //    DevLog.Write("Agent와 IPC Send 불가", LOG_LEVEL.INFO);
            //}
        }
        // IPC 메시지 받기
        private void button6_Click(object sender, EventArgs e)
        {
            //var packet = IPCCommu.ServerReceive();
            //if (packet.PacketIndex == 0)
            //{
            //    return;
            //}

            //if (packet.PacketIndex == ProcessCommunicate.ProcessCommu.PACKET_INDEX_DISCONNECT)
            //{
            //    DevLog.Write("Agent와 IPC Receive 불가", LOG_LEVEL.INFO);
            //}
            //else
            //{
            //    var responData = Util.JsonFormatStringToObject<ProcessCommunicate.IPCTestMsg>(packet.JsonFormat);
            //    DevLog.Write(string.Format("Agent가 보낸 메시지. {0}, {1}", responData.N1, responData.S1), LOG_LEVEL.INFO);
            //}
        }
    }
}
