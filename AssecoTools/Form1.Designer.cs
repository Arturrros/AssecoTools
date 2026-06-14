namespace AssecoTools
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnGain = new System.Windows.Forms.Button();
            this.btnSessions = new System.Windows.Forms.Button();
            this.btnLocks = new System.Windows.Forms.Button();
            this.btnSerwerror = new System.Windows.Forms.Button();
            this.btnDDL = new System.Windows.Forms.Button();
            this.btnLongOps = new System.Windows.Forms.Button();
            this.btnHolds = new System.Windows.Forms.Button();
            this.btn_Session = new System.Windows.Forms.Button();
            this.btnActiveSessionHistory = new System.Windows.Forms.Button();
            this.button_GlobalUndo = new System.Windows.Forms.Button();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showTipsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.langToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.btnExecutes = new System.Windows.Forms.Button();
            this.btnParses = new System.Windows.Forms.Button();
            this.btnUserCalls = new System.Windows.Forms.Button();
            this.btnDeadlocks = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnTrans = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnRecover = new System.Windows.Forms.Button();
            this.btnTemp = new System.Windows.Forms.Button();
            this.btnBytesSend = new System.Windows.Forms.Button();
            this.btnPhysicalReads = new System.Windows.Forms.Button();
            this.btnCPUBYTHIS = new System.Windows.Forms.Button();
            this.btnCommits = new System.Windows.Forms.Button();
            this.btnTableStatistics = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonRepors = new System.Windows.Forms.Button();
            this.btnAWR = new System.Windows.Forms.Button();
            this.btnMissingInedxes = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btnSqlId = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.Active = false;
            // 
            // btnGain
            // 
            this.btnGain.Location = new System.Drawing.Point(643, 19);
            this.btnGain.Name = "btnGain";
            this.btnGain.Size = new System.Drawing.Size(85, 22);
            this.btnGain.TabIndex = 5;
            this.btnGain.Text = "Gain";
            this.toolTip1.SetToolTip(this.btnGain, "Show Differences on tables size");
            this.btnGain.UseVisualStyleBackColor = true;
            this.btnGain.Click += new System.EventHandler(this.btnGain_Click);
            // 
            // btnSessions
            // 
            this.btnSessions.Location = new System.Drawing.Point(97, 19);
            this.btnSessions.Name = "btnSessions";
            this.btnSessions.Size = new System.Drawing.Size(85, 22);
            this.btnSessions.TabIndex = 2;
            this.btnSessions.Text = "Sessions";
            this.toolTip1.SetToolTip(this.btnSessions, "Show sessions\r\nIn this window you have some administrative rights for :\r\nkill ses" +
        "sions,\r\nflush sql plans,\r\nsee all plans");
            this.btnSessions.UseVisualStyleBackColor = true;
            this.btnSessions.Click += new System.EventHandler(this.btnSessions_Click);
            // 
            // btnLocks
            // 
            this.btnLocks.Location = new System.Drawing.Point(6, 19);
            this.btnLocks.Name = "btnLocks";
            this.btnLocks.Size = new System.Drawing.Size(85, 22);
            this.btnLocks.TabIndex = 3;
            this.btnLocks.Text = "Locks";
            this.toolTip1.SetToolTip(this.btnLocks, "Show blockers and waiters\r\nIn this window you can kill sessions");
            this.btnLocks.UseVisualStyleBackColor = true;
            this.btnLocks.Click += new System.EventHandler(this.btnLocks_Click);
            // 
            // btnSerwerror
            // 
            this.btnSerwerror.Location = new System.Drawing.Point(279, 19);
            this.btnSerwerror.Name = "btnSerwerror";
            this.btnSerwerror.Size = new System.Drawing.Size(85, 22);
            this.btnSerwerror.TabIndex = 4;
            this.btnSerwerror.Text = "Error";
            this.toolTip1.SetToolTip(this.btnSerwerror, "Show Server Errors and DDL actions");
            this.btnSerwerror.UseVisualStyleBackColor = true;
            this.btnSerwerror.Click += new System.EventHandler(this.btnServererror_Click);
            // 
            // btnDDL
            // 
            this.btnDDL.Location = new System.Drawing.Point(370, 19);
            this.btnDDL.Name = "btnDDL";
            this.btnDDL.Size = new System.Drawing.Size(85, 22);
            this.btnDDL.TabIndex = 6;
            this.btnDDL.Text = "DDL";
            this.toolTip1.SetToolTip(this.btnDDL, "Show Server Errors and DDL actions");
            this.btnDDL.UseVisualStyleBackColor = true;
            this.btnDDL.Click += new System.EventHandler(this.btnDDL_Click);
            // 
            // btnLongOps
            // 
            this.btnLongOps.Location = new System.Drawing.Point(188, 19);
            this.btnLongOps.Name = "btnLongOps";
            this.btnLongOps.Size = new System.Drawing.Size(85, 22);
            this.btnLongOps.TabIndex = 7;
            this.btnLongOps.Text = "LongOps";
            this.toolTip1.SetToolTip(this.btnLongOps, "Show Server Errors and DDL actions");
            this.btnLongOps.UseVisualStyleBackColor = true;
            this.btnLongOps.Click += new System.EventHandler(this.btnLongOps_Click);
            // 
            // btnHolds
            // 
            this.btnHolds.Location = new System.Drawing.Point(461, 19);
            this.btnHolds.Name = "btnHolds";
            this.btnHolds.Size = new System.Drawing.Size(85, 22);
            this.btnHolds.TabIndex = 8;
            this.btnHolds.Text = "Holds";
            this.toolTip1.SetToolTip(this.btnHolds, "Show Server Errors and DDL actions");
            this.btnHolds.UseVisualStyleBackColor = true;
            this.btnHolds.Click += new System.EventHandler(this.btnHolds_Click);
            // 
            // btn_Session
            // 
            this.btn_Session.Location = new System.Drawing.Point(552, 19);
            this.btn_Session.Name = "btn_Session";
            this.btn_Session.Size = new System.Drawing.Size(85, 22);
            this.btn_Session.TabIndex = 8;
            this.btn_Session.Text = "Sessions";
            this.toolTip1.SetToolTip(this.btn_Session, "Show Differences on tables size");
            this.btn_Session.UseVisualStyleBackColor = true;
            this.btn_Session.Click += new System.EventHandler(this.btn_Session_Click);
            // 
            // btnActiveSessionHistory
            // 
            this.btnActiveSessionHistory.Location = new System.Drawing.Point(643, 47);
            this.btnActiveSessionHistory.Name = "btnActiveSessionHistory";
            this.btnActiveSessionHistory.Size = new System.Drawing.Size(85, 22);
            this.btnActiveSessionHistory.TabIndex = 18;
            this.btnActiveSessionHistory.Text = "ASH";
            this.toolTip1.SetToolTip(this.btnActiveSessionHistory, "Hist Active Session");
            this.btnActiveSessionHistory.UseVisualStyleBackColor = true;
            this.btnActiveSessionHistory.Visible = false;
            this.btnActiveSessionHistory.Click += new System.EventHandler(this.btnActiveSessionHistory_Click);
            // 
            // button_GlobalUndo
            // 
            this.button_GlobalUndo.Location = new System.Drawing.Point(154, 47);
            this.button_GlobalUndo.Name = "button_GlobalUndo";
            this.button_GlobalUndo.Size = new System.Drawing.Size(28, 22);
            this.button_GlobalUndo.TabIndex = 19;
            this.button_GlobalUndo.Text = "G";
            this.toolTip1.SetToolTip(this.button_GlobalUndo, "Global UNDO Sum");
            this.button_GlobalUndo.UseVisualStyleBackColor = true;
            this.button_GlobalUndo.Click += new System.EventHandler(this.button_GlobalUndo_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.AutoToolTip = true;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.disconnectToolStripMenuItem.Text = "Disconnect";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripMenuItem,
            this.changeLogToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.infoToolStripMenuItem.Text = "Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // changeLogToolStripMenuItem
            // 
            this.changeLogToolStripMenuItem.Name = "changeLogToolStripMenuItem";
            this.changeLogToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.changeLogToolStripMenuItem.Text = "Change Log";
            this.changeLogToolStripMenuItem.Click += new System.EventHandler(this.changeLogToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.langToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(768, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showTipsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // showTipsToolStripMenuItem
            // 
            this.showTipsToolStripMenuItem.Name = "showTipsToolStripMenuItem";
            this.showTipsToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.showTipsToolStripMenuItem.Text = "Show Tips";
            this.showTipsToolStripMenuItem.Click += new System.EventHandler(this.showTipsToolStripMenuItem_Click);
            // 
            // langToolStripMenuItem
            // 
            this.langToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eNToolStripMenuItem,
            this.pLToolStripMenuItem});
            this.langToolStripMenuItem.Enabled = false;
            this.langToolStripMenuItem.Name = "langToolStripMenuItem";
            this.langToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.langToolStripMenuItem.Text = "Lang";
            // 
            // eNToolStripMenuItem
            // 
            this.eNToolStripMenuItem.Name = "eNToolStripMenuItem";
            this.eNToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.eNToolStripMenuItem.Text = "EN";
            this.eNToolStripMenuItem.Click += new System.EventHandler(this.eNToolStripMenuItem_Click);
            // 
            // pLToolStripMenuItem
            // 
            this.pLToolStripMenuItem.Name = "pLToolStripMenuItem";
            this.pLToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pLToolStripMenuItem.Text = "PL";
            this.pLToolStripMenuItem.Click += new System.EventHandler(this.pLToolStripMenuItem_Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 228);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(768, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // btnExecutes
            // 
            this.btnExecutes.Location = new System.Drawing.Point(6, 19);
            this.btnExecutes.Name = "btnExecutes";
            this.btnExecutes.Size = new System.Drawing.Size(85, 22);
            this.btnExecutes.TabIndex = 6;
            this.btnExecutes.Text = "Executes";
            this.btnExecutes.UseVisualStyleBackColor = true;
            this.btnExecutes.Click += new System.EventHandler(this.btnExecutes_Click);
            // 
            // btnParses
            // 
            this.btnParses.Location = new System.Drawing.Point(97, 19);
            this.btnParses.Name = "btnParses";
            this.btnParses.Size = new System.Drawing.Size(85, 22);
            this.btnParses.TabIndex = 7;
            this.btnParses.Text = "Parses";
            this.btnParses.UseVisualStyleBackColor = true;
            this.btnParses.Click += new System.EventHandler(this.btnParses_Click);
            // 
            // btnUserCalls
            // 
            this.btnUserCalls.Location = new System.Drawing.Point(188, 19);
            this.btnUserCalls.Name = "btnUserCalls";
            this.btnUserCalls.Size = new System.Drawing.Size(85, 22);
            this.btnUserCalls.TabIndex = 8;
            this.btnUserCalls.Text = "User Calls";
            this.btnUserCalls.UseVisualStyleBackColor = true;
            this.btnUserCalls.Click += new System.EventHandler(this.btnUserCalls_Click);
            // 
            // btnDeadlocks
            // 
            this.btnDeadlocks.Location = new System.Drawing.Point(279, 19);
            this.btnDeadlocks.Name = "btnDeadlocks";
            this.btnDeadlocks.Size = new System.Drawing.Size(85, 22);
            this.btnDeadlocks.TabIndex = 9;
            this.btnDeadlocks.Text = "Daedlocks";
            this.btnDeadlocks.UseVisualStyleBackColor = true;
            this.btnDeadlocks.Click += new System.EventHandler(this.btnDeadlocks_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_GlobalUndo);
            this.groupBox2.Controls.Add(this.btnActiveSessionHistory);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.btnTrans);
            this.groupBox2.Controls.Add(this.btnUndo);
            this.groupBox2.Controls.Add(this.btnRecover);
            this.groupBox2.Controls.Add(this.btnTemp);
            this.groupBox2.Controls.Add(this.btnBytesSend);
            this.groupBox2.Controls.Add(this.btnPhysicalReads);
            this.groupBox2.Controls.Add(this.btnCPUBYTHIS);
            this.groupBox2.Controls.Add(this.btnCommits);
            this.groupBox2.Controls.Add(this.btnDeadlocks);
            this.groupBox2.Controls.Add(this.btnUserCalls);
            this.groupBox2.Controls.Add(this.btnParses);
            this.groupBox2.Controls.Add(this.btnExecutes);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(12, 85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(742, 75);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Monitor buttons ( work work work.... )";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(370, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 22);
            this.button1.TabIndex = 17;
            this.button1.Text = "Open Cur";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnTrans
            // 
            this.btnTrans.Location = new System.Drawing.Point(188, 47);
            this.btnTrans.Name = "btnTrans";
            this.btnTrans.Size = new System.Drawing.Size(85, 22);
            this.btnTrans.TabIndex = 16;
            this.btnTrans.Text = "Trans";
            this.btnTrans.UseVisualStyleBackColor = true;
            this.btnTrans.Click += new System.EventHandler(this.btnTrans_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(97, 47);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(51, 22);
            this.btnUndo.TabIndex = 15;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnRecover
            // 
            this.btnRecover.Location = new System.Drawing.Point(279, 47);
            this.btnRecover.Name = "btnRecover";
            this.btnRecover.Size = new System.Drawing.Size(85, 22);
            this.btnRecover.TabIndex = 14;
            this.btnRecover.Text = "Recover";
            this.btnRecover.UseVisualStyleBackColor = true;
            this.btnRecover.Click += new System.EventHandler(this.btnRecover_Click);
            // 
            // btnTemp
            // 
            this.btnTemp.Location = new System.Drawing.Point(6, 47);
            this.btnTemp.Name = "btnTemp";
            this.btnTemp.Size = new System.Drawing.Size(85, 22);
            this.btnTemp.TabIndex = 7;
            this.btnTemp.Text = "Temp";
            this.btnTemp.UseVisualStyleBackColor = true;
            this.btnTemp.Click += new System.EventHandler(this.btnTemp_Click);
            // 
            // btnBytesSend
            // 
            this.btnBytesSend.Location = new System.Drawing.Point(643, 19);
            this.btnBytesSend.Name = "btnBytesSend";
            this.btnBytesSend.Size = new System.Drawing.Size(85, 22);
            this.btnBytesSend.TabIndex = 13;
            this.btnBytesSend.Text = "BytesSend";
            this.btnBytesSend.UseVisualStyleBackColor = true;
            this.btnBytesSend.Click += new System.EventHandler(this.btnBytesSend_Click);
            // 
            // btnPhysicalReads
            // 
            this.btnPhysicalReads.Location = new System.Drawing.Point(552, 19);
            this.btnPhysicalReads.Name = "btnPhysicalReads";
            this.btnPhysicalReads.Size = new System.Drawing.Size(85, 22);
            this.btnPhysicalReads.TabIndex = 12;
            this.btnPhysicalReads.Text = "PhysReads";
            this.btnPhysicalReads.UseVisualStyleBackColor = true;
            this.btnPhysicalReads.Click += new System.EventHandler(this.btnPhysicalReads_Click);
            // 
            // btnCPUBYTHIS
            // 
            this.btnCPUBYTHIS.Location = new System.Drawing.Point(461, 19);
            this.btnCPUBYTHIS.Name = "btnCPUBYTHIS";
            this.btnCPUBYTHIS.Size = new System.Drawing.Size(85, 22);
            this.btnCPUBYTHIS.TabIndex = 11;
            this.btnCPUBYTHIS.Text = "CPU";
            this.btnCPUBYTHIS.UseVisualStyleBackColor = true;
            this.btnCPUBYTHIS.Click += new System.EventHandler(this.btnCPUBYTHIS_Click);
            // 
            // btnCommits
            // 
            this.btnCommits.Location = new System.Drawing.Point(370, 19);
            this.btnCommits.Name = "btnCommits";
            this.btnCommits.Size = new System.Drawing.Size(85, 22);
            this.btnCommits.TabIndex = 10;
            this.btnCommits.Text = "Commits";
            this.btnCommits.UseVisualStyleBackColor = true;
            this.btnCommits.Click += new System.EventHandler(this.btnCommits_Click);
            // 
            // btnTableStatistics
            // 
            this.btnTableStatistics.Location = new System.Drawing.Point(6, 19);
            this.btnTableStatistics.Name = "btnTableStatistics";
            this.btnTableStatistics.Size = new System.Drawing.Size(85, 22);
            this.btnTableStatistics.TabIndex = 0;
            this.btnTableStatistics.Text = "Stats";
            this.btnTableStatistics.UseVisualStyleBackColor = true;
            this.btnTableStatistics.Click += new System.EventHandler(this.btnTableStatistics_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.buttonRepors);
            this.groupBox3.Controls.Add(this.btn_Session);
            this.groupBox3.Controls.Add(this.btnAWR);
            this.groupBox3.Controls.Add(this.btnMissingInedxes);
            this.groupBox3.Controls.Add(this.btnTableStatistics);
            this.groupBox3.Controls.Add(this.btnGain);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(12, 166);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(742, 56);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Developper";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(461, 19);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(85, 22);
            this.button3.TabIndex = 10;
            this.button3.Text = "Chart";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonRepors
            // 
            this.buttonRepors.Location = new System.Drawing.Point(370, 19);
            this.buttonRepors.Name = "buttonRepors";
            this.buttonRepors.Size = new System.Drawing.Size(85, 22);
            this.buttonRepors.TabIndex = 9;
            this.buttonRepors.Text = "Reports";
            this.buttonRepors.UseVisualStyleBackColor = true;
            this.buttonRepors.Visible = false;
            this.buttonRepors.Click += new System.EventHandler(this.buttonRepors_Click);
            // 
            // btnAWR
            // 
            this.btnAWR.Location = new System.Drawing.Point(279, 19);
            this.btnAWR.Name = "btnAWR";
            this.btnAWR.Size = new System.Drawing.Size(85, 22);
            this.btnAWR.TabIndex = 7;
            this.btnAWR.Text = "AWR";
            this.btnAWR.UseVisualStyleBackColor = true;
            this.btnAWR.Click += new System.EventHandler(this.btnAWR_Click);
            // 
            // btnMissingInedxes
            // 
            this.btnMissingInedxes.Location = new System.Drawing.Point(97, 19);
            this.btnMissingInedxes.Name = "btnMissingInedxes";
            this.btnMissingInedxes.Size = new System.Drawing.Size(176, 22);
            this.btnMissingInedxes.TabIndex = 6;
            this.btnMissingInedxes.Text = "Missing Indexes";
            this.btnMissingInedxes.UseVisualStyleBackColor = true;
            this.btnMissingInedxes.Click += new System.EventHandler(this.btnMissingInedxes_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.btnSqlId);
            this.groupBox1.Controls.Add(this.btnHolds);
            this.groupBox1.Controls.Add(this.btnLongOps);
            this.groupBox1.Controls.Add(this.btnDDL);
            this.groupBox1.Controls.Add(this.btnSerwerror);
            this.groupBox1.Controls.Add(this.btnLocks);
            this.groupBox1.Controls.Add(this.btnSessions);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(742, 52);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sessions";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(643, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 22);
            this.button2.TabIndex = 9;
            this.button2.Text = "TBS";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnSqlId
            // 
            this.btnSqlId.Location = new System.Drawing.Point(552, 19);
            this.btnSqlId.Name = "btnSqlId";
            this.btnSqlId.Size = new System.Drawing.Size(85, 22);
            this.btnSqlId.TabIndex = 8;
            this.btnSqlId.Text = "sql_id";
            this.btnSqlId.UseVisualStyleBackColor = true;
            this.btnSqlId.Click += new System.EventHandler(this.btnSqlId_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(768, 250);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AssecoTools";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnExecutes;
        private System.Windows.Forms.Button btnParses;
        private System.Windows.Forms.Button btnUserCalls;
        private System.Windows.Forms.Button btnDeadlocks;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGain;
        private System.Windows.Forms.Button btnTableStatistics;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSessions;
        private System.Windows.Forms.Button btnLocks;
        private System.Windows.Forms.Button btnSerwerror;
        private System.Windows.Forms.Button btnDDL;
        private System.Windows.Forms.Button btnLongOps;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCommits;
        private System.Windows.Forms.Button btnCPUBYTHIS;
        private System.Windows.Forms.Button btnPhysicalReads;
        private System.Windows.Forms.Button btnBytesSend;
        private System.Windows.Forms.ToolStripMenuItem langToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showTipsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeLogToolStripMenuItem;
        private System.Windows.Forms.Button btnMissingInedxes;
        private System.Windows.Forms.Button btnHolds;
        private System.Windows.Forms.Button btnTemp;
        private System.Windows.Forms.Button btnRecover;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnAWR;
        private System.Windows.Forms.Button btnSqlId;
        private System.Windows.Forms.Button btnTrans;
        private System.Windows.Forms.Button btn_Session;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonRepors;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnActiveSessionHistory;
        private System.Windows.Forms.Button button_GlobalUndo;
        private System.Windows.Forms.Button button3;
    }
}

