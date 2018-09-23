namespace ChatClient
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
            this.button3 = new System.Windows.Forms.Button();
            this.labelConnState = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.checkBoxLocalHostIP = new System.Windows.Forms.CheckBox();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonLogInOut = new System.Windows.Forms.Button();
            this.textBoxAuthToken = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listViewLobbyList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.textBoxSendChat = new System.Windows.Forms.TextBox();
            this.listBoxChat = new System.Windows.Forms.ListBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(429, 19);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(71, 19);
            this.button3.TabIndex = 25;
            this.button3.Text = "접속 끊기";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // labelConnState
            // 
            this.labelConnState.AutoSize = true;
            this.labelConnState.Location = new System.Drawing.Point(406, 17);
            this.labelConnState.Name = "labelConnState";
            this.labelConnState.Size = new System.Drawing.Size(111, 12);
            this.labelConnState.TabIndex = 24;
            this.labelConnState.Text = "서버 접속 상태: ???";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(302, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(66, 19);
            this.button2.TabIndex = 23;
            this.button2.Text = "접속하기";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBoxPort);
            this.groupBox5.Controls.Add(this.labelConnState);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.checkBoxLocalHostIP);
            this.groupBox5.Controls.Add(this.textBoxIP);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Location = new System.Drawing.Point(12, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(572, 52);
            this.groupBox5.TabIndex = 22;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Socket 더미 클라이언트 설정";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(225, 20);
            this.textBoxPort.MaxLength = 6;
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(51, 21);
            this.textBoxPort.TabIndex = 18;
            this.textBoxPort.Text = "18732";
            this.textBoxPort.WordWrap = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(163, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 12);
            this.label10.TabIndex = 17;
            this.label10.Text = "포트 번호:";
            // 
            // checkBoxLocalHostIP
            // 
            this.checkBoxLocalHostIP.AutoSize = true;
            this.checkBoxLocalHostIP.Checked = true;
            this.checkBoxLocalHostIP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLocalHostIP.Location = new System.Drawing.Point(285, 24);
            this.checkBoxLocalHostIP.Name = "checkBoxLocalHostIP";
            this.checkBoxLocalHostIP.Size = new System.Drawing.Size(103, 16);
            this.checkBoxLocalHostIP.TabIndex = 15;
            this.checkBoxLocalHostIP.Text = "localhost 사용";
            this.checkBoxLocalHostIP.UseVisualStyleBackColor = true;
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(68, 19);
            this.textBoxIP.MaxLength = 6;
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(87, 21);
            this.textBoxIP.TabIndex = 11;
            this.textBoxIP.Text = "172.20.60.220";
            this.textBoxIP.WordWrap = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 12);
            this.label9.TabIndex = 10;
            this.label9.Text = "서버 주소:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonLogInOut);
            this.groupBox1.Controls.Add(this.textBoxAuthToken);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxID);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Location = new System.Drawing.Point(12, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(572, 46);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "로그인";
            // 
            // buttonLogInOut
            // 
            this.buttonLogInOut.Location = new System.Drawing.Point(370, 19);
            this.buttonLogInOut.Name = "buttonLogInOut";
            this.buttonLogInOut.Size = new System.Drawing.Size(53, 19);
            this.buttonLogInOut.TabIndex = 30;
            this.buttonLogInOut.Text = "로그인";
            this.buttonLogInOut.UseVisualStyleBackColor = true;
            this.buttonLogInOut.Click += new System.EventHandler(this.buttonLogInOut_Click);
            // 
            // textBoxAuthToken
            // 
            this.textBoxAuthToken.Location = new System.Drawing.Point(210, 19);
            this.textBoxAuthToken.MaxLength = 6;
            this.textBoxAuthToken.Name = "textBoxAuthToken";
            this.textBoxAuthToken.Size = new System.Drawing.Size(86, 21);
            this.textBoxAuthToken.TabIndex = 29;
            this.textBoxAuthToken.Text = "32345";
            this.textBoxAuthToken.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(149, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 28;
            this.label1.Text = "보안토큰:";
            // 
            // textBoxID
            // 
            this.textBoxID.Location = new System.Drawing.Point(54, 18);
            this.textBoxID.MaxLength = 6;
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(87, 21);
            this.textBoxID.TabIndex = 27;
            this.textBoxID.Text = "test1";
            this.textBoxID.WordWrap = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 12);
            this.label2.TabIndex = 26;
            this.label2.Text = "아이디:";
            // 
            // listViewLobbyList
            // 
            this.listViewLobbyList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewLobbyList.Location = new System.Drawing.Point(6, 20);
            this.listViewLobbyList.Name = "listViewLobbyList";
            this.listViewLobbyList.Size = new System.Drawing.Size(135, 350);
            this.listViewLobbyList.TabIndex = 27;
            this.listViewLobbyList.UseCompatibleStateImageBehavior = false;
            this.listViewLobbyList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 29;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "현재 인원";
            this.columnHeader2.Width = 75;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.textBoxSendChat);
            this.groupBox2.Controls.Add(this.listBoxChat);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.listViewLobbyList);
            this.groupBox2.Location = new System.Drawing.Point(12, 122);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(572, 438);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "방";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(501, 405);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(52, 23);
            this.button5.TabIndex = 32;
            this.button5.Text = "보내기";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBoxSendChat
            // 
            this.textBoxSendChat.Location = new System.Drawing.Point(156, 405);
            this.textBoxSendChat.Name = "textBoxSendChat";
            this.textBoxSendChat.Size = new System.Drawing.Size(339, 21);
            this.textBoxSendChat.TabIndex = 31;
            // 
            // listBoxChat
            // 
            this.listBoxChat.FormattingEnabled = true;
            this.listBoxChat.ItemHeight = 12;
            this.listBoxChat.Location = new System.Drawing.Point(156, 20);
            this.listBoxChat.Name = "listBoxChat";
            this.listBoxChat.Size = new System.Drawing.Size(397, 376);
            this.listBoxChat.TabIndex = 30;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(68, 376);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(73, 23);
            this.button4.TabIndex = 29;
            this.button4.Text = "방 입장";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 403);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 23);
            this.button1.TabIndex = 28;
            this.button1.Text = "방 나가기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.ItemHeight = 12;
            this.listBoxLog.Location = new System.Drawing.Point(12, 566);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(574, 184);
            this.listBoxLog.TabIndex = 31;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 377);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(61, 21);
            this.textBox1.TabIndex = 33;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 755);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "채팅 클라이언트";
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label labelConnState;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBoxLocalHostIP;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxAuthToken;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listViewLobbyList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBoxSendChat;
        private System.Windows.Forms.ListBox listBoxChat;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonLogInOut;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.TextBox textBox1;
    }
}

