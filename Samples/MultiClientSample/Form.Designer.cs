namespace MultiClientSample
{
    partial class MainForm
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
            this.menu = new System.Windows.Forms.MenuStrip();
            this.meshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.split = new System.Windows.Forms.SplitContainer();
            this.buttonSignIn = new System.Windows.Forms.Button();
            this.labelError = new System.Windows.Forms.Label();
            this.comboUsername = new System.Windows.Forms.ComboBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSelect = new System.Windows.Forms.Button();
            this.tbSaveMessagesPath = new System.Windows.Forms.TextBox();
            this.cbSaveMessages = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pbForwardAudio = new System.Windows.Forms.ProgressBar();
            this.lbForwardAudio = new System.Windows.Forms.Label();
            this.cbForwardAudio = new System.Windows.Forms.CheckedListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pbSendProgress = new System.Windows.Forms.ProgressBar();
            this.btnSend = new System.Windows.Forms.Button();
            this.tbSendWavPath = new System.Windows.Forms.TextBox();
            this.btnWavBrowse = new System.Windows.Forms.Button();
            this.cbSendAudioFile = new System.Windows.Forms.CheckedListBox();
            this.lbSendAudio = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cbEnableAI = new System.Windows.Forms.CheckBox();
            this.replayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.meshToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(677, 24);
            this.menu.TabIndex = 2;
            this.menu.Text = "menuStrip1";
            // 
            // meshToolStripMenuItem
            // 
            this.meshToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.signInToolStripMenuItem,
            this.signOutToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem,
            this.replayToolStripMenuItem});
            this.meshToolStripMenuItem.Name = "meshToolStripMenuItem";
            this.meshToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.meshToolStripMenuItem.Text = "Status";
            // 
            // signInToolStripMenuItem
            // 
            this.signInToolStripMenuItem.Name = "signInToolStripMenuItem";
            this.signInToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.signInToolStripMenuItem.Text = "Sign in";
            this.signInToolStripMenuItem.Click += new System.EventHandler(this.signInToolStripMenuItem_Click);
            // 
            // signOutToolStripMenuItem
            // 
            this.signOutToolStripMenuItem.Name = "signOutToolStripMenuItem";
            this.signOutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.signOutToolStripMenuItem.Text = "Sign out";
            this.signOutToolStripMenuItem.Click += new System.EventHandler(this.signOutToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // split
            // 
            this.split.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.split.Location = new System.Drawing.Point(0, 24);
            this.split.Name = "split";
            // 
            // split.Panel1
            // 
            this.split.Panel1.Controls.Add(this.buttonSignIn);
            this.split.Panel1.Controls.Add(this.labelError);
            this.split.Panel1.Controls.Add(this.comboUsername);
            this.split.Panel1.Controls.Add(this.labelPassword);
            this.split.Panel1.Controls.Add(this.textPassword);
            this.split.Panel1.Controls.Add(this.labelUsername);
            // 
            // split.Panel2
            // 
            this.split.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.split.Size = new System.Drawing.Size(677, 614);
            this.split.SplitterDistance = 308;
            this.split.TabIndex = 9;
            // 
            // buttonSignIn
            // 
            this.buttonSignIn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonSignIn.Location = new System.Drawing.Point(167, 127);
            this.buttonSignIn.Name = "buttonSignIn";
            this.buttonSignIn.Size = new System.Drawing.Size(75, 23);
            this.buttonSignIn.TabIndex = 14;
            this.buttonSignIn.Text = "Sign in";
            this.buttonSignIn.UseVisualStyleBackColor = true;
            this.buttonSignIn.Click += new System.EventHandler(this.buttonSignIn_Click);
            // 
            // labelError
            // 
            this.labelError.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelError.Location = new System.Drawing.Point(50, 184);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(204, 38);
            this.labelError.TabIndex = 13;
            this.labelError.Text = "PTT ActiveX is not installed.";
            this.labelError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelError.Visible = false;
            // 
            // comboUsername
            // 
            this.comboUsername.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboUsername.FormattingEnabled = true;
            this.comboUsername.Items.AddRange(new object[] {
            "test1",
            "test2",
            "test3",
            "test4",
            "test5",
            "test6",
            "test7"});
            this.comboUsername.Location = new System.Drawing.Point(62, 56);
            this.comboUsername.MaxDropDownItems = 100;
            this.comboUsername.MaxLength = 128;
            this.comboUsername.Name = "comboUsername";
            this.comboUsername.Size = new System.Drawing.Size(180, 21);
            this.comboUsername.TabIndex = 11;
            this.comboUsername.Text = "test3";
            // 
            // labelPassword
            // 
            this.labelPassword.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(62, 84);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(53, 13);
            this.labelPassword.TabIndex = 10;
            this.labelPassword.Text = "Password";
            // 
            // textPassword
            // 
            this.textPassword.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textPassword.Location = new System.Drawing.Point(62, 100);
            this.textPassword.MaxLength = 128;
            this.textPassword.Name = "textPassword";
            this.textPassword.Size = new System.Drawing.Size(180, 21);
            this.textPassword.TabIndex = 12;
            this.textPassword.Text = "test";
            this.textPassword.UseSystemPasswordChar = true;
            // 
            // labelUsername
            // 
            this.labelUsername.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(62, 40);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(55, 13);
            this.labelUsername.TabIndex = 9;
            this.labelUsername.Text = "Username";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(361, 610);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSelect);
            this.panel1.Controls.Add(this.tbSaveMessagesPath);
            this.panel1.Controls.Add(this.cbSaveMessages);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(355, 54);
            this.panel1.TabIndex = 2;
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Location = new System.Drawing.Point(279, 27);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(69, 23);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "Browse...";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // tbSaveMessagesPath
            // 
            this.tbSaveMessagesPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSaveMessagesPath.Location = new System.Drawing.Point(4, 29);
            this.tbSaveMessagesPath.Name = "tbSaveMessagesPath";
            this.tbSaveMessagesPath.Size = new System.Drawing.Size(269, 21);
            this.tbSaveMessagesPath.TabIndex = 1;
            // 
            // cbSaveMessages
            // 
            this.cbSaveMessages.AutoSize = true;
            this.cbSaveMessages.Location = new System.Drawing.Point(4, 4);
            this.cbSaveMessages.Name = "cbSaveMessages";
            this.cbSaveMessages.Size = new System.Drawing.Size(188, 17);
            this.cbSaveMessages.TabIndex = 0;
            this.cbSaveMessages.Text = "Save incoming messages to folder";
            this.cbSaveMessages.UseVisualStyleBackColor = true;
            this.cbSaveMessages.CheckedChanged += new System.EventHandler(this.cbSaveMessages_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pbForwardAudio);
            this.panel2.Controls.Add(this.lbForwardAudio);
            this.panel2.Controls.Add(this.cbForwardAudio);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 93);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(355, 254);
            this.panel2.TabIndex = 3;
            // 
            // pbForwardAudio
            // 
            this.pbForwardAudio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbForwardAudio.Location = new System.Drawing.Point(63, 7);
            this.pbForwardAudio.Name = "pbForwardAudio";
            this.pbForwardAudio.Size = new System.Drawing.Size(285, 14);
            this.pbForwardAudio.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbForwardAudio.TabIndex = 4;
            this.pbForwardAudio.Visible = false;
            // 
            // lbForwardAudio
            // 
            this.lbForwardAudio.AutoSize = true;
            this.lbForwardAudio.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbForwardAudio.Location = new System.Drawing.Point(1, 7);
            this.lbForwardAudio.Name = "lbForwardAudio";
            this.lbForwardAudio.Size = new System.Drawing.Size(55, 13);
            this.lbForwardAudio.TabIndex = 3;
            this.lbForwardAudio.Text = "Echo back";
            // 
            // cbForwardAudio
            // 
            this.cbForwardAudio.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbForwardAudio.CheckOnClick = true;
            this.cbForwardAudio.FormattingEnabled = true;
            this.cbForwardAudio.Location = new System.Drawing.Point(4, 27);
            this.cbForwardAudio.Name = "cbForwardAudio";
            this.cbForwardAudio.Size = new System.Drawing.Size(344, 228);
            this.cbForwardAudio.Sorted = true;
            this.cbForwardAudio.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pbSendProgress);
            this.panel3.Controls.Add(this.btnSend);
            this.panel3.Controls.Add(this.tbSendWavPath);
            this.panel3.Controls.Add(this.btnWavBrowse);
            this.panel3.Controls.Add(this.cbSendAudioFile);
            this.panel3.Controls.Add(this.lbSendAudio);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 353);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(355, 254);
            this.panel3.TabIndex = 4;
            // 
            // pbSendProgress
            // 
            this.pbSendProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSendProgress.Location = new System.Drawing.Point(83, 229);
            this.pbSendProgress.Maximum = 0;
            this.pbSendProgress.Name = "pbSendProgress";
            this.pbSendProgress.Size = new System.Drawing.Size(265, 18);
            this.pbSendProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbSendProgress.TabIndex = 13;
            this.pbSendProgress.Visible = false;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(8, 227);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(68, 23);
            this.btnSend.TabIndex = 12;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tbSendWavPath
            // 
            this.tbSendWavPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSendWavPath.Location = new System.Drawing.Point(6, 25);
            this.tbSendWavPath.Name = "tbSendWavPath";
            this.tbSendWavPath.Size = new System.Drawing.Size(267, 21);
            this.tbSendWavPath.TabIndex = 11;
            // 
            // btnWavBrowse
            // 
            this.btnWavBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWavBrowse.Location = new System.Drawing.Point(279, 23);
            this.btnWavBrowse.Name = "btnWavBrowse";
            this.btnWavBrowse.Size = new System.Drawing.Size(69, 23);
            this.btnWavBrowse.TabIndex = 10;
            this.btnWavBrowse.Text = "Browse...";
            this.btnWavBrowse.UseVisualStyleBackColor = true;
            this.btnWavBrowse.Click += new System.EventHandler(this.btnWavBrowse_Click);
            // 
            // cbSendAudioFile
            // 
            this.cbSendAudioFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSendAudioFile.CheckOnClick = true;
            this.cbSendAudioFile.FormattingEnabled = true;
            this.cbSendAudioFile.Location = new System.Drawing.Point(7, 59);
            this.cbSendAudioFile.Name = "cbSendAudioFile";
            this.cbSendAudioFile.Size = new System.Drawing.Size(341, 164);
            this.cbSendAudioFile.TabIndex = 9;
            // 
            // lbSendAudio
            // 
            this.lbSendAudio.AutoSize = true;
            this.lbSendAudio.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbSendAudio.Location = new System.Drawing.Point(4, 4);
            this.lbSendAudio.Name = "lbSendAudio";
            this.lbSendAudio.Size = new System.Drawing.Size(77, 13);
            this.lbSendAudio.TabIndex = 7;
            this.lbSendAudio.Text = "Send audio file";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cbEnableAI);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(355, 24);
            this.panel4.TabIndex = 5;
            // 
            // cbEnableAI
            // 
            this.cbEnableAI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.cbEnableAI.Location = new System.Drawing.Point(0, 0);
            this.cbEnableAI.Name = "cbEnableAI";
            this.cbEnableAI.Size = new System.Drawing.Size(148, 24);
            this.cbEnableAI.TabIndex = 2;
            this.cbEnableAI.Text = "Enable audio integration";
            this.cbEnableAI.UseVisualStyleBackColor = true;
            this.cbEnableAI.CheckedChanged += new System.EventHandler(this.cbEnableAI_CheckedChanged);
            // 
            // replayToolStripMenuItem
            // 
            this.replayToolStripMenuItem.Name = "replayToolStripMenuItem";
            this.replayToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.replayToolStripMenuItem.Text = "Replay";
            this.replayToolStripMenuItem.Click += new System.EventHandler(this.replayToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 638);
            this.Controls.Add(this.split);
            this.Controls.Add(this.menu);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MinimumSize = new System.Drawing.Size(212, 358);
            this.Name = "MainForm";
            this.Text = "PTT Multiclient sample";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel1.PerformLayout();
            this.split.Panel2.ResumeLayout(false);
            this.split.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem meshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer split;
        private System.Windows.Forms.Button buttonSignIn;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.ComboBox comboUsername;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.TextBox tbSaveMessagesPath;
        private System.Windows.Forms.CheckBox cbSaveMessages;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckedListBox cbForwardAudio;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox tbSendWavPath;
        private System.Windows.Forms.Button btnWavBrowse;
        private System.Windows.Forms.CheckedListBox cbSendAudioFile;
        private System.Windows.Forms.Label lbSendAudio;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox cbEnableAI;
        private System.Windows.Forms.Label lbForwardAudio;
        private System.Windows.Forms.ProgressBar pbSendProgress;
        private System.Windows.Forms.ProgressBar pbForwardAudio;
        private System.Windows.Forms.ToolStripMenuItem replayToolStripMenuItem;
    }
}

