using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatServer
{
    public partial class MainForm : Form
    {
        System.Windows.Threading.DispatcherTimer workProcessTimer = new System.Windows.Threading.DispatcherTimer();

        MainServer ServerApp;


        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            workProcessTimer.Tick += new EventHandler(OnProcessTimedEvent);
            workProcessTimer.Interval = new TimeSpan(0, 0, 0, 0, 32);
            workProcessTimer.Start();

            ServerApp = new MainServer();
            ServerApp.CreateStartServer();
            var error = ServerApp.CreateComponent();

            (error == CSBaseLib.ERROR_CODE.NONE).IfFalse(() => 
            {
                var errorMsg = string.Format("서버 컴포넌트 생성 실패. {0}: {1}", error, error.ToString());
                MainServer.WriteLog(errorMsg, CommonServerLib.LOG_LEVEL.INFO);
                CommonServerLib.DevLog.Write(errorMsg, CommonServerLib.LOG_LEVEL.ERROR);
            });
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ServerApp.StopServer();
        }

        private void OnProcessTimedEvent(object sender, EventArgs e)
        {
            ProcessLog();
            ProcessInnerMessage();
        }

        private void ProcessLog()
        {
            // 너무 이 작업만 할 수 없으므로 일정 작업 이상을 하면 일단 패스한다.
            int logWorkCount = 0;

            while (true)
            {
                string msg;

                if (CommonServerLib.DevLog.GetLog(out msg))
                {
                    ++logWorkCount;

                    if (listBoxLog.Items.Count > 100)
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

                if (logWorkCount > 32)
                {
                    break;
                }
            }
        }

        private void ProcessInnerMessage()
        {
            // 너무 이 작업만 할 수 없으므로 일정 작업 이상을 하면 일단 패스한다.
            int workedCount = 0;

            while (true)
            {
                CommonServerLib.InnerMsg msg;

                if (CommonServerLib.InnerMessageHostProgram.GetMsg(out msg))
                {
                    ++workedCount;

                    switch(msg.Type)
                    {
                        case CommonServerLib.InnerMsgType.SERVER_START:
                            var values = msg.Value1.Split("_");
                            textBoxServerID.Text = values[0];
                            textBoxAddress.Text = values[1];
                            break;
                        case CommonServerLib.InnerMsgType.CREATE_COMPONENT:
                            textBoxLobbyStartIndex.Text = ChatServerEnvironment.LobbyStartIndex.ToString();
                            textBoxLobbyThreadCount.Text = ChatServerEnvironment.LobbyThreadCount.ToString();
                            textBoxLobbyCountPerThread.Text = ChatServerEnvironment.LobbyCountPerThread.ToString();
                            textBoxMaxUserPerLobby.Text = ChatServerEnvironment.MaxUserPerLobby.ToString();
                            break;
                        case CommonServerLib.InnerMsgType.CURRENT_CONNECT_COUNT:
                            textBoxCurrentUserCount.Text = msg.Value1;
                            break;
                    }
                }
                else
                {
                    break;
                }

                if (workedCount > 32)
                {
                    break;
                }
            }
        }
        
    }
}
