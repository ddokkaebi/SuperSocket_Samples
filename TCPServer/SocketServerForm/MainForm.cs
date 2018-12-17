using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.ServiceModel.Web;
using System.ServiceModel;
using System.ServiceModel.Description;

//using ServerLogic = ServerLib.ServerLogic;
using ServerEnvironment = ServerLib.ServerEnvironment;
using ServerLogger = ServerLib.FileLogger;
using CommonServerLib;


namespace SocketServerForm
{        
    public partial class MainForm : Form
    {
        ServerLib.ServerLogic ChatServerLogic;

        
        public MainForm()
        {
            InitializeComponent();
        }

        void Init()
        {
            // 멀티코어 JIT 유효화
            System.Runtime.ProfileOptimization.SetProfileRoot(Environment.CurrentDirectory);
            System.Runtime.ProfileOptimization.StartProfile("App.JIT.Profile");

            SettingChatServerEnvironment();

            InitUIOption();

            CreateFormWorkThread();

            CreateServerLogic();

            CreateLobbyUIData();
            SetUserStatusCheckOption();

            var programName = "ChatServerTest";

            this.Text = programName;
        }

        bool CreateServerLogic()
        {
            ChatServerLogic = new ServerLib.ServerLogic();
            if (ChatServerLogic.CreateNetwork() == false)
            {
                return false;
            }

            //var RemoteServers = new List<string>();
            //foreach (var server in Properties.Settings.Default.RemoteServers)
            //{
            //    RemoteServers.Add(server);
            //}

            //ChatServerLogic.StartRemoteConnectCheck(RemoteServers);
                        

            var error = ChatServerLogic.CreateComponent();
            if (error != CSBaseLib.ERROR_CODE.NONE)
            {
                var errorMsg = string.Format("서버 컴포넌트 생성 실패. {0}: {1}", error, error.ToString());
                ServerLogger.Write(errorMsg, LOG_LEVEL.INFO);
                return false;
            }

            return true;
        }

        void SettingChatServerEnvironment()
        {
            int serverType = (int)ServerLib.SERVER_TYPE.CHAT;

            ServerEnvironment.SetBasic(serverType, "ChatServer", 1000, 8);
            ServerEnvironment.SetLobby(1, 10, 50);
            ServerEnvironment.SetChatServer(1, 1, 1);
        }
                
                
    }
}
