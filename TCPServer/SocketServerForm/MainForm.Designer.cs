namespace SocketServerForm
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBoxHeavyNetworkTrafficUser = new System.Windows.Forms.CheckBox();
            this.checkBoxTimeLimitUserLobby = new System.Windows.Forms.CheckBox();
            this.checkBoxTimeLimitUserAuth = new System.Windows.Forms.CheckBox();
            this.checkBoxHeartBeat = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxViewLogLeaveLobby = new System.Windows.Forms.CheckBox();
            this.checkBoxViewLogEnterLobby = new System.Windows.Forms.CheckBox();
            this.checkBoxViewLogLogin = new System.Windows.Forms.CheckBox();
            this.checkBoxViewLogS2SMessage = new System.Windows.Forms.CheckBox();
            this.checkBoxViewLogWrongUser = new System.Windows.Forms.CheckBox();
            this.checkBoxViewLogBlockUser = new System.Windows.Forms.CheckBox();
            this.checkBoxViewLogHeartBeat = new System.Windows.Forms.CheckBox();
            this.checkBoxAllUILogStop = new System.Windows.Forms.CheckBox();
            this.checkBoxViewLogPacketReceived = new System.Windows.Forms.CheckBox();
            this.checkBoxViewLogCheckUserStatus = new System.Windows.Forms.CheckBox();
            this.checkBoxViewLogDisConnect = new System.Windows.Forms.CheckBox();
            this.checkBoxViewLogConnect = new System.Windows.Forms.CheckBox();
            this.checkBoxLobbyStatusUIUpdateStop = new System.Windows.Forms.CheckBox();
            this.checkBoxUserCountUIUpdateStop = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxServerNotice = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listBoxLogLevel = new System.Windows.Forms.ListBox();
            this.listViewLobbyInfo = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxMaxUserPerLobby = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxLobbyStartIndex = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxLobbyCount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxServerID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCurrentUserCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBoxDisConnectSessioID = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.button테스트_동접변경 = new System.Windows.Forms.Button();
            this.textBoxTestUserCount = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.textBoxRedisWriteUserAuthToken = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxRedisWriteUserID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.textBoxS2SMessage = new System.Windows.Forms.TextBox();
            this.listBoxS2SServer = new System.Windows.Forms.ListBox();
            this.listBoxS2SMsgType = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBoxHeavyNetworkTrafficUser);
            this.groupBox4.Controls.Add(this.checkBoxTimeLimitUserLobby);
            this.groupBox4.Controls.Add(this.checkBoxTimeLimitUserAuth);
            this.groupBox4.Controls.Add(this.checkBoxHeartBeat);
            this.groupBox4.Location = new System.Drawing.Point(536, 136);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(199, 147);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "유저 상태 조사";
            // 
            // checkBoxHeavyNetworkTrafficUser
            // 
            this.checkBoxHeavyNetworkTrafficUser.AutoSize = true;
            this.checkBoxHeavyNetworkTrafficUser.Checked = true;
            this.checkBoxHeavyNetworkTrafficUser.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHeavyNetworkTrafficUser.Location = new System.Drawing.Point(6, 78);
            this.checkBoxHeavyNetworkTrafficUser.Name = "checkBoxHeavyNetworkTrafficUser";
            this.checkBoxHeavyNetworkTrafficUser.Size = new System.Drawing.Size(128, 16);
            this.checkBoxHeavyNetworkTrafficUser.TabIndex = 14;
            this.checkBoxHeavyNetworkTrafficUser.Text = "과다 네트워크 사용";
            this.checkBoxHeavyNetworkTrafficUser.UseVisualStyleBackColor = true;
            this.checkBoxHeavyNetworkTrafficUser.CheckedChanged += new System.EventHandler(this.checkBoxHeavyNetworkTrafficUser_CheckedChanged);
            // 
            // checkBoxTimeLimitUserLobby
            // 
            this.checkBoxTimeLimitUserLobby.AutoSize = true;
            this.checkBoxTimeLimitUserLobby.Checked = true;
            this.checkBoxTimeLimitUserLobby.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTimeLimitUserLobby.Location = new System.Drawing.Point(6, 58);
            this.checkBoxTimeLimitUserLobby.Name = "checkBoxTimeLimitUserLobby";
            this.checkBoxTimeLimitUserLobby.Size = new System.Drawing.Size(116, 16);
            this.checkBoxTimeLimitUserLobby.TabIndex = 13;
            this.checkBoxTimeLimitUserLobby.Text = "시간내 로비 입장";
            this.checkBoxTimeLimitUserLobby.UseVisualStyleBackColor = true;
            this.checkBoxTimeLimitUserLobby.CheckedChanged += new System.EventHandler(this.checkBoxTimeLimitUserLobby_CheckedChanged);
            // 
            // checkBoxTimeLimitUserAuth
            // 
            this.checkBoxTimeLimitUserAuth.AutoSize = true;
            this.checkBoxTimeLimitUserAuth.Checked = true;
            this.checkBoxTimeLimitUserAuth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTimeLimitUserAuth.Location = new System.Drawing.Point(6, 38);
            this.checkBoxTimeLimitUserAuth.Name = "checkBoxTimeLimitUserAuth";
            this.checkBoxTimeLimitUserAuth.Size = new System.Drawing.Size(88, 16);
            this.checkBoxTimeLimitUserAuth.TabIndex = 12;
            this.checkBoxTimeLimitUserAuth.Text = "시간내 인증";
            this.checkBoxTimeLimitUserAuth.UseVisualStyleBackColor = true;
            this.checkBoxTimeLimitUserAuth.CheckedChanged += new System.EventHandler(this.checkBoxTimeLimitUserAuth_CheckedChanged);
            // 
            // checkBoxHeartBeat
            // 
            this.checkBoxHeartBeat.AutoSize = true;
            this.checkBoxHeartBeat.Checked = true;
            this.checkBoxHeartBeat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHeartBeat.Location = new System.Drawing.Point(6, 20);
            this.checkBoxHeartBeat.Name = "checkBoxHeartBeat";
            this.checkBoxHeartBeat.Size = new System.Drawing.Size(104, 16);
            this.checkBoxHeartBeat.TabIndex = 11;
            this.checkBoxHeartBeat.Text = "하트 비트 사용";
            this.checkBoxHeartBeat.UseVisualStyleBackColor = true;
            this.checkBoxHeartBeat.CheckedChanged += new System.EventHandler(this.checkBoxHeartBeat_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxViewLogLeaveLobby);
            this.groupBox3.Controls.Add(this.checkBoxViewLogEnterLobby);
            this.groupBox3.Controls.Add(this.checkBoxViewLogLogin);
            this.groupBox3.Controls.Add(this.checkBoxViewLogS2SMessage);
            this.groupBox3.Controls.Add(this.checkBoxViewLogWrongUser);
            this.groupBox3.Controls.Add(this.checkBoxViewLogBlockUser);
            this.groupBox3.Controls.Add(this.checkBoxViewLogHeartBeat);
            this.groupBox3.Controls.Add(this.checkBoxAllUILogStop);
            this.groupBox3.Controls.Add(this.checkBoxViewLogPacketReceived);
            this.groupBox3.Controls.Add(this.checkBoxViewLogCheckUserStatus);
            this.groupBox3.Controls.Add(this.checkBoxViewLogDisConnect);
            this.groupBox3.Controls.Add(this.checkBoxViewLogConnect);
            this.groupBox3.Location = new System.Drawing.Point(7, 135);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(344, 147);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "View에 로그 출력 여부";
            // 
            // checkBoxViewLogLeaveLobby
            // 
            this.checkBoxViewLogLeaveLobby.AutoSize = true;
            this.checkBoxViewLogLeaveLobby.Location = new System.Drawing.Point(177, 102);
            this.checkBoxViewLogLeaveLobby.Name = "checkBoxViewLogLeaveLobby";
            this.checkBoxViewLogLeaveLobby.Size = new System.Drawing.Size(88, 16);
            this.checkBoxViewLogLeaveLobby.TabIndex = 13;
            this.checkBoxViewLogLeaveLobby.Text = "로비 나가기";
            this.checkBoxViewLogLeaveLobby.UseVisualStyleBackColor = true;
            this.checkBoxViewLogLeaveLobby.CheckedChanged += new System.EventHandler(this.checkBoxViewLogLeaveLobby_CheckedChanged);
            // 
            // checkBoxViewLogEnterLobby
            // 
            this.checkBoxViewLogEnterLobby.AutoSize = true;
            this.checkBoxViewLogEnterLobby.Location = new System.Drawing.Point(95, 102);
            this.checkBoxViewLogEnterLobby.Name = "checkBoxViewLogEnterLobby";
            this.checkBoxViewLogEnterLobby.Size = new System.Drawing.Size(76, 16);
            this.checkBoxViewLogEnterLobby.TabIndex = 12;
            this.checkBoxViewLogEnterLobby.Text = "로비 입장";
            this.checkBoxViewLogEnterLobby.UseVisualStyleBackColor = true;
            this.checkBoxViewLogEnterLobby.CheckedChanged += new System.EventHandler(this.checkBoxViewLogEnterLobby_CheckedChanged);
            // 
            // checkBoxViewLogLogin
            // 
            this.checkBoxViewLogLogin.AutoSize = true;
            this.checkBoxViewLogLogin.Location = new System.Drawing.Point(9, 102);
            this.checkBoxViewLogLogin.Name = "checkBoxViewLogLogin";
            this.checkBoxViewLogLogin.Size = new System.Drawing.Size(60, 16);
            this.checkBoxViewLogLogin.TabIndex = 11;
            this.checkBoxViewLogLogin.Text = "로그인";
            this.checkBoxViewLogLogin.UseVisualStyleBackColor = true;
            this.checkBoxViewLogLogin.CheckedChanged += new System.EventHandler(this.checkBoxViewLogLogin_CheckedChanged);
            // 
            // checkBoxViewLogS2SMessage
            // 
            this.checkBoxViewLogS2SMessage.AutoSize = true;
            this.checkBoxViewLogS2SMessage.Location = new System.Drawing.Point(103, 78);
            this.checkBoxViewLogS2SMessage.Name = "checkBoxViewLogS2SMessage";
            this.checkBoxViewLogS2SMessage.Size = new System.Drawing.Size(128, 16);
            this.checkBoxViewLogS2SMessage.TabIndex = 10;
            this.checkBoxViewLogS2SMessage.Text = "서버간 메시지 읽기";
            this.checkBoxViewLogS2SMessage.UseVisualStyleBackColor = true;
            this.checkBoxViewLogS2SMessage.CheckedChanged += new System.EventHandler(this.checkBoxViewLogS2SMessage_CheckedChanged);
            // 
            // checkBoxViewLogWrongUser
            // 
            this.checkBoxViewLogWrongUser.AutoSize = true;
            this.checkBoxViewLogWrongUser.Location = new System.Drawing.Point(9, 80);
            this.checkBoxViewLogWrongUser.Name = "checkBoxViewLogWrongUser";
            this.checkBoxViewLogWrongUser.Size = new System.Drawing.Size(88, 16);
            this.checkBoxViewLogWrongUser.TabIndex = 8;
            this.checkBoxViewLogWrongUser.Text = "잘못된 유저";
            this.checkBoxViewLogWrongUser.UseVisualStyleBackColor = true;
            this.checkBoxViewLogWrongUser.CheckedChanged += new System.EventHandler(this.checkBoxViewLogWrongUser_CheckedChanged);
            // 
            // checkBoxViewLogBlockUser
            // 
            this.checkBoxViewLogBlockUser.AutoSize = true;
            this.checkBoxViewLogBlockUser.Location = new System.Drawing.Point(227, 58);
            this.checkBoxViewLogBlockUser.Name = "checkBoxViewLogBlockUser";
            this.checkBoxViewLogBlockUser.Size = new System.Drawing.Size(76, 16);
            this.checkBoxViewLogBlockUser.TabIndex = 7;
            this.checkBoxViewLogBlockUser.Text = "유저 블럭";
            this.checkBoxViewLogBlockUser.UseVisualStyleBackColor = true;
            this.checkBoxViewLogBlockUser.CheckedChanged += new System.EventHandler(this.checkBoxViewLogBlockUser_CheckedChanged);
            // 
            // checkBoxViewLogHeartBeat
            // 
            this.checkBoxViewLogHeartBeat.AutoSize = true;
            this.checkBoxViewLogHeartBeat.Location = new System.Drawing.Point(145, 58);
            this.checkBoxViewLogHeartBeat.Name = "checkBoxViewLogHeartBeat";
            this.checkBoxViewLogHeartBeat.Size = new System.Drawing.Size(76, 16);
            this.checkBoxViewLogHeartBeat.TabIndex = 6;
            this.checkBoxViewLogHeartBeat.Text = "허트 비트";
            this.checkBoxViewLogHeartBeat.UseVisualStyleBackColor = true;
            this.checkBoxViewLogHeartBeat.CheckedChanged += new System.EventHandler(this.checkBoxViewLogHeartBeat_CheckedChanged);
            // 
            // checkBoxAllUILogStop
            // 
            this.checkBoxAllUILogStop.AutoSize = true;
            this.checkBoxAllUILogStop.Checked = true;
            this.checkBoxAllUILogStop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAllUILogStop.Location = new System.Drawing.Point(9, 14);
            this.checkBoxAllUILogStop.Name = "checkBoxAllUILogStop";
            this.checkBoxAllUILogStop.Size = new System.Drawing.Size(104, 16);
            this.checkBoxAllUILogStop.TabIndex = 5;
            this.checkBoxAllUILogStop.Text = "모든 로그 중단";
            this.checkBoxAllUILogStop.UseVisualStyleBackColor = true;
            this.checkBoxAllUILogStop.CheckedChanged += new System.EventHandler(this.checkBoxAllUIUpdateStop_CheckedChanged);
            // 
            // checkBoxViewLogPacketReceived
            // 
            this.checkBoxViewLogPacketReceived.AutoSize = true;
            this.checkBoxViewLogPacketReceived.Location = new System.Drawing.Point(9, 58);
            this.checkBoxViewLogPacketReceived.Name = "checkBoxViewLogPacketReceived";
            this.checkBoxViewLogPacketReceived.Size = new System.Drawing.Size(130, 16);
            this.checkBoxViewLogPacketReceived.TabIndex = 3;
            this.checkBoxViewLogPacketReceived.Text = "OnPacketReceived";
            this.checkBoxViewLogPacketReceived.UseVisualStyleBackColor = true;
            this.checkBoxViewLogPacketReceived.CheckedChanged += new System.EventHandler(this.checkBoxViewLogPacketReceived_CheckedChanged);
            // 
            // checkBoxViewLogCheckUserStatus
            // 
            this.checkBoxViewLogCheckUserStatus.AutoSize = true;
            this.checkBoxViewLogCheckUserStatus.Location = new System.Drawing.Point(205, 36);
            this.checkBoxViewLogCheckUserStatus.Name = "checkBoxViewLogCheckUserStatus";
            this.checkBoxViewLogCheckUserStatus.Size = new System.Drawing.Size(132, 16);
            this.checkBoxViewLogCheckUserStatus.TabIndex = 2;
            this.checkBoxViewLogCheckUserStatus.Text = "유저 상태 조사 결과";
            this.checkBoxViewLogCheckUserStatus.UseVisualStyleBackColor = true;
            this.checkBoxViewLogCheckUserStatus.CheckedChanged += new System.EventHandler(this.checkBoxViewLogCheckUserStatus_CheckedChanged);
            // 
            // checkBoxViewLogDisConnect
            // 
            this.checkBoxViewLogDisConnect.AutoSize = true;
            this.checkBoxViewLogDisConnect.Location = new System.Drawing.Point(95, 36);
            this.checkBoxViewLogDisConnect.Name = "checkBoxViewLogDisConnect";
            this.checkBoxViewLogDisConnect.Size = new System.Drawing.Size(104, 16);
            this.checkBoxViewLogDisConnect.TabIndex = 1;
            this.checkBoxViewLogDisConnect.Text = "연결 종료 로그";
            this.checkBoxViewLogDisConnect.UseVisualStyleBackColor = true;
            this.checkBoxViewLogDisConnect.CheckedChanged += new System.EventHandler(this.checkBoxViewLogDisConnect_CheckedChanged);
            // 
            // checkBoxViewLogConnect
            // 
            this.checkBoxViewLogConnect.AutoSize = true;
            this.checkBoxViewLogConnect.Location = new System.Drawing.Point(9, 36);
            this.checkBoxViewLogConnect.Name = "checkBoxViewLogConnect";
            this.checkBoxViewLogConnect.Size = new System.Drawing.Size(76, 16);
            this.checkBoxViewLogConnect.TabIndex = 0;
            this.checkBoxViewLogConnect.Text = "연결 로그";
            this.checkBoxViewLogConnect.UseVisualStyleBackColor = true;
            this.checkBoxViewLogConnect.CheckedChanged += new System.EventHandler(this.checkBoxViewLogConnect_CheckedChanged);
            // 
            // checkBoxLobbyStatusUIUpdateStop
            // 
            this.checkBoxLobbyStatusUIUpdateStop.AutoSize = true;
            this.checkBoxLobbyStatusUIUpdateStop.Location = new System.Drawing.Point(6, 39);
            this.checkBoxLobbyStatusUIUpdateStop.Name = "checkBoxLobbyStatusUIUpdateStop";
            this.checkBoxLobbyStatusUIUpdateStop.Size = new System.Drawing.Size(76, 16);
            this.checkBoxLobbyStatusUIUpdateStop.TabIndex = 6;
            this.checkBoxLobbyStatusUIUpdateStop.Text = "로비 상태";
            this.checkBoxLobbyStatusUIUpdateStop.UseVisualStyleBackColor = true;
            this.checkBoxLobbyStatusUIUpdateStop.CheckedChanged += new System.EventHandler(this.checkBoxLobbyStatusUIUpdateStop_CheckedChanged);
            // 
            // checkBoxUserCountUIUpdateStop
            // 
            this.checkBoxUserCountUIUpdateStop.AutoSize = true;
            this.checkBoxUserCountUIUpdateStop.Location = new System.Drawing.Point(6, 20);
            this.checkBoxUserCountUIUpdateStop.Name = "checkBoxUserCountUIUpdateStop";
            this.checkBoxUserCountUIUpdateStop.Size = new System.Drawing.Size(104, 16);
            this.checkBoxUserCountUIUpdateStop.TabIndex = 4;
            this.checkBoxUserCountUIUpdateStop.Text = "동시 접속자 수";
            this.checkBoxUserCountUIUpdateStop.UseVisualStyleBackColor = true;
            this.checkBoxUserCountUIUpdateStop.CheckedChanged += new System.EventHandler(this.checkBoxUserCountUIUpdateStop_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(652, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 45);
            this.button1.TabIndex = 20;
            this.button1.Text = "서버 공지";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxServerNotice
            // 
            this.textBoxServerNotice.Location = new System.Drawing.Point(6, 6);
            this.textBoxServerNotice.Multiline = true;
            this.textBoxServerNotice.Name = "textBoxServerNotice";
            this.textBoxServerNotice.Size = new System.Drawing.Size(640, 45);
            this.textBoxServerNotice.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(398, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "현재 UI  출력 로그 레벨";
            // 
            // listBoxLogLevel
            // 
            this.listBoxLogLevel.FormattingEnabled = true;
            this.listBoxLogLevel.ItemHeight = 12;
            this.listBoxLogLevel.Items.AddRange(new object[] {
            "TRACE",
            "DEBUG",
            "INFO",
            "WARN",
            "ERROR",
            "DISABLE"});
            this.listBoxLogLevel.Location = new System.Drawing.Point(400, 33);
            this.listBoxLogLevel.Name = "listBoxLogLevel";
            this.listBoxLogLevel.Size = new System.Drawing.Size(130, 88);
            this.listBoxLogLevel.TabIndex = 17;
            this.listBoxLogLevel.SelectedIndexChanged += new System.EventHandler(this.listBoxLogLevel_SelectedIndexChanged);
            // 
            // listViewLobbyInfo
            // 
            this.listViewLobbyInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewLobbyInfo.Location = new System.Drawing.Point(768, 19);
            this.listViewLobbyInfo.Name = "listViewLobbyInfo";
            this.listViewLobbyInfo.Size = new System.Drawing.Size(121, 571);
            this.listViewLobbyInfo.TabIndex = 16;
            this.listViewLobbyInfo.UseCompatibleStateImageBehavior = false;
            this.listViewLobbyInfo.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "로비ID";
            this.columnHeader1.Width = 52;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "인원";
            this.columnHeader2.Width = 39;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxMaxUserPerLobby);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBoxLobbyStartIndex);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBoxLobbyCount);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(191, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(190, 115);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "로비 설정";
            // 
            // textBoxMaxUserPerLobby
            // 
            this.textBoxMaxUserPerLobby.Location = new System.Drawing.Point(120, 60);
            this.textBoxMaxUserPerLobby.Name = "textBoxMaxUserPerLobby";
            this.textBoxMaxUserPerLobby.Size = new System.Drawing.Size(52, 21);
            this.textBoxMaxUserPerLobby.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "로비 최대 유저 수:";
            // 
            // textBoxLobbyStartIndex
            // 
            this.textBoxLobbyStartIndex.Location = new System.Drawing.Point(120, 13);
            this.textBoxLobbyStartIndex.Name = "textBoxLobbyStartIndex";
            this.textBoxLobbyStartIndex.Size = new System.Drawing.Size(53, 21);
            this.textBoxLobbyStartIndex.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "시작 번호:";
            // 
            // textBoxLobbyCount
            // 
            this.textBoxLobbyCount.Location = new System.Drawing.Point(120, 37);
            this.textBoxLobbyCount.Name = "textBoxLobbyCount";
            this.textBoxLobbyCount.Size = new System.Drawing.Size(53, 21);
            this.textBoxLobbyCount.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "로비 수:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxServerID);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBoxAddress);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxCurrentUserCount);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 115);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "서버 정보";
            // 
            // textBoxServerID
            // 
            this.textBoxServerID.Enabled = false;
            this.textBoxServerID.Location = new System.Drawing.Point(99, 13);
            this.textBoxServerID.Name = "textBoxServerID";
            this.textBoxServerID.Size = new System.Drawing.Size(60, 21);
            this.textBoxServerID.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(48, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "서버 ID:";
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Enabled = false;
            this.textBoxAddress.Location = new System.Drawing.Point(99, 38);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(60, 21);
            this.textBoxAddress.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "서버 Port:";
            // 
            // textBoxCurrentUserCount
            // 
            this.textBoxCurrentUserCount.Enabled = false;
            this.textBoxCurrentUserCount.Location = new System.Drawing.Point(99, 65);
            this.textBoxCurrentUserCount.Name = "textBoxCurrentUserCount";
            this.textBoxCurrentUserCount.Size = new System.Drawing.Size(60, 21);
            this.textBoxCurrentUserCount.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "동시 접속자 수:";
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.HorizontalScrollbar = true;
            this.listBoxLog.ItemHeight = 12;
            this.listBoxLog.Location = new System.Drawing.Point(12, 367);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.ScrollAlwaysVisible = true;
            this.listBoxLog.Size = new System.Drawing.Size(750, 304);
            this.listBoxLog.TabIndex = 13;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.checkBoxLobbyStatusUIUpdateStop);
            this.groupBox5.Controls.Add(this.checkBoxUserCountUIUpdateStop);
            this.groupBox5.Location = new System.Drawing.Point(360, 135);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(170, 148);
            this.groupBox5.TabIndex = 23;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Form UI 실시간 갱신";
            // 
            // textBoxDisConnectSessioID
            // 
            this.textBoxDisConnectSessioID.Location = new System.Drawing.Point(74, 57);
            this.textBoxDisConnectSessioID.Name = "textBoxDisConnectSessioID";
            this.textBoxDisConnectSessioID.Size = new System.Drawing.Size(136, 21);
            this.textBoxDisConnectSessioID.TabIndex = 24;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(213, 57);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 23);
            this.button2.TabIndex = 25;
            this.button2.Text = "세션 짜르기";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 10);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(749, 351);
            this.tabControl1.TabIndex = 26;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.listBoxLogLevel);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(741, 325);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(633, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 12);
            this.label9.TabIndex = 24;
            this.label9.Text = "2014. 08.03 14:41";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox9);
            this.tabPage2.Controls.Add(this.groupBox8);
            this.tabPage2.Controls.Add(this.groupBox7);
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.textBoxServerNotice);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.textBoxDisConnectSessioID);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(741, 325);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "조작";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(385, 20);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(45, 22);
            this.button6.TabIndex = 32;
            this.button6.Text = "받기";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(329, 20);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(50, 22);
            this.button5.TabIndex = 31;
            this.button5.Text = "보내기";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 21);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(315, 21);
            this.textBox1.TabIndex = 30;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.button테스트_동접변경);
            this.groupBox8.Controls.Add(this.textBoxTestUserCount);
            this.groupBox8.Controls.Add(this.label12);
            this.groupBox8.Location = new System.Drawing.Point(387, 146);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(348, 46);
            this.groupBox8.TabIndex = 29;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "동접이 Agent에 잘 가는지 테스트";
            // 
            // button테스트_동접변경
            // 
            this.button테스트_동접변경.Location = new System.Drawing.Point(140, 15);
            this.button테스트_동접변경.Name = "button테스트_동접변경";
            this.button테스트_동접변경.Size = new System.Drawing.Size(75, 23);
            this.button테스트_동접변경.TabIndex = 2;
            this.button테스트_동접변경.Text = "변경";
            this.button테스트_동접변경.UseVisualStyleBackColor = true;
            this.button테스트_동접변경.Click += new System.EventHandler(this.button테스트_동접변경_Click);
            // 
            // textBoxTestUserCount
            // 
            this.textBoxTestUserCount.Location = new System.Drawing.Point(75, 16);
            this.textBoxTestUserCount.Name = "textBoxTestUserCount";
            this.textBoxTestUserCount.Size = new System.Drawing.Size(58, 21);
            this.textBoxTestUserCount.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "현재 인원:";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.button4);
            this.groupBox7.Controls.Add(this.textBoxRedisWriteUserAuthToken);
            this.groupBox7.Controls.Add(this.label11);
            this.groupBox7.Controls.Add(this.textBoxRedisWriteUserID);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Location = new System.Drawing.Point(387, 92);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(348, 47);
            this.groupBox7.TabIndex = 28;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Redis에 로그인 정보 저장";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(277, 17);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(55, 23);
            this.button4.TabIndex = 27;
            this.button4.Text = "저장";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBoxRedisWriteUserAuthToken
            // 
            this.textBoxRedisWriteUserAuthToken.Location = new System.Drawing.Point(194, 19);
            this.textBoxRedisWriteUserAuthToken.Name = "textBoxRedisWriteUserAuthToken";
            this.textBoxRedisWriteUserAuthToken.Size = new System.Drawing.Size(76, 21);
            this.textBoxRedisWriteUserAuthToken.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(121, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 12);
            this.label11.TabIndex = 2;
            this.label11.Text = "AuthToken:";
            // 
            // textBoxRedisWriteUserID
            // 
            this.textBoxRedisWriteUserID.Location = new System.Drawing.Point(31, 20);
            this.textBoxRedisWriteUserID.Name = "textBoxRedisWriteUserID";
            this.textBoxRedisWriteUserID.Size = new System.Drawing.Size(76, 21);
            this.textBoxRedisWriteUserID.TabIndex = 1;
            this.textBoxRedisWriteUserID.Text = "TEST_01";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(20, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "ID:";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.button3);
            this.groupBox6.Controls.Add(this.textBoxS2SMessage);
            this.groupBox6.Controls.Add(this.listBoxS2SServer);
            this.groupBox6.Controls.Add(this.listBoxS2SMsgType);
            this.groupBox6.Location = new System.Drawing.Point(8, 92);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(373, 100);
            this.groupBox6.TabIndex = 27;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "서버To서버 메시지";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(264, 61);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(92, 23);
            this.button3.TabIndex = 26;
            this.button3.Text = "보내기";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBoxS2SMessage
            // 
            this.textBoxS2SMessage.Location = new System.Drawing.Point(6, 20);
            this.textBoxS2SMessage.Multiline = true;
            this.textBoxS2SMessage.Name = "textBoxS2SMessage";
            this.textBoxS2SMessage.Size = new System.Drawing.Size(152, 64);
            this.textBoxS2SMessage.TabIndex = 25;
            // 
            // listBoxS2SServer
            // 
            this.listBoxS2SServer.FormattingEnabled = true;
            this.listBoxS2SServer.ItemHeight = 12;
            this.listBoxS2SServer.Items.AddRange(new object[] {
            "GAME",
            "CHAT"});
            this.listBoxS2SServer.Location = new System.Drawing.Point(264, 20);
            this.listBoxS2SServer.Name = "listBoxS2SServer";
            this.listBoxS2SServer.Size = new System.Drawing.Size(94, 40);
            this.listBoxS2SServer.TabIndex = 1;
            // 
            // listBoxS2SMsgType
            // 
            this.listBoxS2SMsgType.FormattingEnabled = true;
            this.listBoxS2SMsgType.ItemHeight = 12;
            this.listBoxS2SMsgType.Items.AddRange(new object[] {
            "NTF",
            "DIS_CONNECT",
            "GUILD_CHAT"});
            this.listBoxS2SMsgType.Location = new System.Drawing.Point(164, 20);
            this.listBoxS2SMsgType.Name = "listBoxS2SMsgType";
            this.listBoxS2SMsgType.Size = new System.Drawing.Size(94, 64);
            this.listBoxS2SMsgType.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 12);
            this.label8.TabIndex = 26;
            this.label8.Text = "SessionID:";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.button5);
            this.groupBox9.Controls.Add(this.button6);
            this.groupBox9.Controls.Add(this.textBox1);
            this.groupBox9.Location = new System.Drawing.Point(6, 198);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(440, 62);
            this.groupBox9.TabIndex = 33;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Agent와 IPC 통신 테스트";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 680);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.listViewLobbyInfo);
            this.Controls.Add(this.listBoxLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBoxTimeLimitUserLobby;
        private System.Windows.Forms.CheckBox checkBoxTimeLimitUserAuth;
        private System.Windows.Forms.CheckBox checkBoxHeartBeat;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxLobbyStatusUIUpdateStop;
        private System.Windows.Forms.CheckBox checkBoxAllUILogStop;
        private System.Windows.Forms.CheckBox checkBoxUserCountUIUpdateStop;
        private System.Windows.Forms.CheckBox checkBoxViewLogPacketReceived;
        private System.Windows.Forms.CheckBox checkBoxViewLogCheckUserStatus;
        private System.Windows.Forms.CheckBox checkBoxViewLogDisConnect;
        private System.Windows.Forms.CheckBox checkBoxViewLogConnect;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxServerNotice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listBoxLogLevel;
        private System.Windows.Forms.ListView listViewLobbyInfo;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxMaxUserPerLobby;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxLobbyStartIndex;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxLobbyCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxServerID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCurrentUserCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.CheckBox checkBoxHeavyNetworkTrafficUser;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox checkBoxViewLogHeartBeat;
        private System.Windows.Forms.CheckBox checkBoxViewLogLeaveLobby;
        private System.Windows.Forms.CheckBox checkBoxViewLogEnterLobby;
        private System.Windows.Forms.CheckBox checkBoxViewLogLogin;
        private System.Windows.Forms.CheckBox checkBoxViewLogS2SMessage;
        private System.Windows.Forms.CheckBox checkBoxViewLogWrongUser;
        private System.Windows.Forms.CheckBox checkBoxViewLogBlockUser;
        private System.Windows.Forms.TextBox textBoxDisConnectSessioID;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBoxS2SMessage;
        private System.Windows.Forms.ListBox listBoxS2SServer;
        private System.Windows.Forms.ListBox listBoxS2SMsgType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBoxRedisWriteUserAuthToken;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxRedisWriteUserID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button button테스트_동접변경;
        private System.Windows.Forms.TextBox textBoxTestUserCount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox9;
    }
}

