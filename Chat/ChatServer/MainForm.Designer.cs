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
            this.textBoxRoomMaxCountPerThread = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxRoomThreadCount = new System.Windows.Forms.TextBox();
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
            this.listBoxLog.ItemHeight = 15;
            this.listBoxLog.Location = new System.Drawing.Point(14, 181);
            this.listBoxLog.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(939, 409);
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
            this.groupBox1.Location = new System.Drawing.Point(14, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(203, 144);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server Info";
            // 
            // textBoxServerID
            // 
            this.textBoxServerID.Location = new System.Drawing.Point(113, 16);
            this.textBoxServerID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxServerID.Name = "textBoxServerID";
            this.textBoxServerID.ReadOnly = true;
            this.textBoxServerID.Size = new System.Drawing.Size(83, 25);
            this.textBoxServerID.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 15);
            this.label7.TabIndex = 4;
            this.label7.Text = "Server ID:";
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Location = new System.Drawing.Point(113, 48);
            this.textBoxAddress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.ReadOnly = true;
            this.textBoxAddress.Size = new System.Drawing.Size(83, 25);
            this.textBoxAddress.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Server Port:";
            // 
            // textBoxCurrentUserCount
            // 
            this.textBoxCurrentUserCount.Location = new System.Drawing.Point(153, 81);
            this.textBoxCurrentUserCount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxCurrentUserCount.Name = "textBoxCurrentUserCount";
            this.textBoxCurrentUserCount.ReadOnly = true;
            this.textBoxCurrentUserCount.Size = new System.Drawing.Size(43, 25);
            this.textBoxCurrentUserCount.TabIndex = 1;
            this.textBoxCurrentUserCount.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connect User Count:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxRoomMaxCountPerThread);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBoxRoomThreadCount);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(224, 15);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(265, 82);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lobby";
            // 
            // textBoxRoomMaxCountPerThread
            // 
            this.textBoxRoomMaxCountPerThread.Location = new System.Drawing.Point(168, 50);
            this.textBoxRoomMaxCountPerThread.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxRoomMaxCountPerThread.Name = "textBoxRoomMaxCountPerThread";
            this.textBoxRoomMaxCountPerThread.ReadOnly = true;
            this.textBoxRoomMaxCountPerThread.Size = new System.Drawing.Size(59, 25);
            this.textBoxRoomMaxCountPerThread.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(47, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "MaxRoomCount:";
            // 
            // textBoxRoomThreadCount
            // 
            this.textBoxRoomThreadCount.Location = new System.Drawing.Point(168, 18);
            this.textBoxRoomThreadCount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxRoomThreadCount.Name = "textBoxRoomThreadCount";
            this.textBoxRoomThreadCount.ReadOnly = true;
            this.textBoxRoomThreadCount.Size = new System.Drawing.Size(60, 25);
            this.textBoxRoomThreadCount.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Room Thread Count:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxRoomStartNumber);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.textBoxRoomMaxUserCount);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Location = new System.Drawing.Point(512, 15);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(210, 82);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Room";
            // 
            // textBoxRoomStartNumber
            // 
            this.textBoxRoomStartNumber.Location = new System.Drawing.Point(131, 16);
            this.textBoxRoomStartNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxRoomStartNumber.Name = "textBoxRoomStartNumber";
            this.textBoxRoomStartNumber.ReadOnly = true;
            this.textBoxRoomStartNumber.Size = new System.Drawing.Size(60, 25);
            this.textBoxRoomStartNumber.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(32, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 15);
            this.label10.TabIndex = 2;
            this.label10.Text = "Start Number:";
            // 
            // textBoxRoomMaxUserCount
            // 
            this.textBoxRoomMaxUserCount.Location = new System.Drawing.Point(131, 46);
            this.textBoxRoomMaxUserCount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxRoomMaxUserCount.Name = "textBoxRoomMaxUserCount";
            this.textBoxRoomMaxUserCount.ReadOnly = true;
            this.textBoxRoomMaxUserCount.Size = new System.Drawing.Size(60, 25);
            this.textBoxRoomMaxUserCount.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 15);
            this.label11.TabIndex = 0;
            this.label11.Text = "Max User Count:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 602);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listBoxLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
        private System.Windows.Forms.TextBox textBoxRoomThreadCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxServerID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxRoomMaxCountPerThread;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxRoomStartNumber;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxRoomMaxUserCount;
        private System.Windows.Forms.Label label11;
    }
}

