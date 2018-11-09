namespace ChatServer
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
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxServerID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCurrentUserCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxLobbyMaxRoomCount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxLobbyCountPerThread = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxLobbyStartNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxLobbyThreadCount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxRoomStartNumber = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxRoomMaxUserCount = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.ItemHeight = 12;
            this.listBoxLog.Location = new System.Drawing.Point(12, 145);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(822, 328);
            this.listBoxLog.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxServerID);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBoxAddress);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxCurrentUserCount);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 115);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server Info";
            // 
            // textBoxServerID
            // 
            this.textBoxServerID.Location = new System.Drawing.Point(99, 13);
            this.textBoxServerID.Name = "textBoxServerID";
            this.textBoxServerID.Size = new System.Drawing.Size(73, 21);
            this.textBoxServerID.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(38, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "Server ID:";
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Location = new System.Drawing.Point(99, 38);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(73, 21);
            this.textBoxAddress.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Server Port:";
            // 
            // textBoxCurrentUserCount
            // 
            this.textBoxCurrentUserCount.Location = new System.Drawing.Point(134, 65);
            this.textBoxCurrentUserCount.Name = "textBoxCurrentUserCount";
            this.textBoxCurrentUserCount.Size = new System.Drawing.Size(38, 21);
            this.textBoxCurrentUserCount.TabIndex = 1;
            this.textBoxCurrentUserCount.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connect User Count:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxLobbyMaxRoomCount);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBoxLobbyCountPerThread);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBoxLobbyStartNumber);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBoxLobbyThreadCount);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(196, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(232, 115);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lobby";
            // 
            // textBoxLobbyMaxRoomCount
            // 
            this.textBoxLobbyMaxRoomCount.Location = new System.Drawing.Point(147, 89);
            this.textBoxLobbyMaxRoomCount.Name = "textBoxLobbyMaxRoomCount";
            this.textBoxLobbyMaxRoomCount.Size = new System.Drawing.Size(52, 21);
            this.textBoxLobbyMaxRoomCount.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "MaxRoomCount:";
            // 
            // textBoxLobbyCountPerThread
            // 
            this.textBoxLobbyCountPerThread.Location = new System.Drawing.Point(147, 64);
            this.textBoxLobbyCountPerThread.Name = "textBoxLobbyCountPerThread";
            this.textBoxLobbyCountPerThread.Size = new System.Drawing.Size(53, 21);
            this.textBoxLobbyCountPerThread.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "LobbyCountPerThread:";
            // 
            // textBoxLobbyStartNumber
            // 
            this.textBoxLobbyStartNumber.Location = new System.Drawing.Point(147, 13);
            this.textBoxLobbyStartNumber.Name = "textBoxLobbyStartNumber";
            this.textBoxLobbyStartNumber.Size = new System.Drawing.Size(53, 21);
            this.textBoxLobbyStartNumber.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Start Number:";
            // 
            // textBoxLobbyThreadCount
            // 
            this.textBoxLobbyThreadCount.Location = new System.Drawing.Point(147, 37);
            this.textBoxLobbyThreadCount.Name = "textBoxLobbyThreadCount";
            this.textBoxLobbyThreadCount.Size = new System.Drawing.Size(53, 21);
            this.textBoxLobbyThreadCount.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "Thread Count:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxRoomStartNumber);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.textBoxRoomMaxUserCount);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Location = new System.Drawing.Point(448, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(184, 66);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Room";
            // 
            // textBoxRoomStartNumber
            // 
            this.textBoxRoomStartNumber.Location = new System.Drawing.Point(115, 13);
            this.textBoxRoomStartNumber.Name = "textBoxRoomStartNumber";
            this.textBoxRoomStartNumber.Size = new System.Drawing.Size(53, 21);
            this.textBoxRoomStartNumber.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(28, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "Start Number:";
            // 
            // textBoxRoomMaxUserCount
            // 
            this.textBoxRoomMaxUserCount.Location = new System.Drawing.Point(115, 37);
            this.textBoxRoomMaxUserCount.Name = "textBoxRoomMaxUserCount";
            this.textBoxRoomMaxUserCount.Size = new System.Drawing.Size(53, 21);
            this.textBoxRoomMaxUserCount.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "Max User Count:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 482);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listBoxLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "ChatServer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCurrentUserCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxLobbyStartNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxLobbyThreadCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxServerID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxLobbyMaxRoomCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxLobbyCountPerThread;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxRoomStartNumber;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxRoomMaxUserCount;
        private System.Windows.Forms.Label label11;
    }
}

