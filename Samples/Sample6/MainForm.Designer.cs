namespace Sample6
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
            this.buttonSignIn = new System.Windows.Forms.Button();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.menu = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.signInToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.signOutToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.normalToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.compactToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.historyToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.helpWebToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.comboUsername = new System.Windows.Forms.ComboBox();
            this.labelError = new System.Windows.Forms.Label();
            this.changeAudioDeviceToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // buttonSignIn
            // 
            this.buttonSignIn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonSignIn.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.buttonSignIn.Location = new System.Drawing.Point(135, 99);
            this.buttonSignIn.Name = "buttonSignIn";
            this.buttonSignIn.Size = new System.Drawing.Size(75, 25);
            this.buttonSignIn.TabIndex = 13;
            this.buttonSignIn.Text = "Sign in";
            this.buttonSignIn.Click += new System.EventHandler(this.buttonSignIn_Click);
            // 
            // labelPassword
            // 
            this.labelPassword.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelPassword.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelPassword.Location = new System.Drawing.Point(30, 56);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(76, 15);
            this.labelPassword.Text = "Password";
            // 
            // textPassword
            // 
            this.textPassword.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textPassword.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textPassword.Location = new System.Drawing.Point(30, 74);
            this.textPassword.MaxLength = 128;
            this.textPassword.Name = "textPassword";
            this.textPassword.PasswordChar = '*';
            this.textPassword.Size = new System.Drawing.Size(180, 19);
            this.textPassword.TabIndex = 12;
            this.textPassword.Text = "test";
            // 
            // labelUsername
            // 
            this.labelUsername.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelUsername.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelUsername.Location = new System.Drawing.Point(30, 12);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(88, 15);
            this.labelUsername.Text = "Username";
            // 
            // menu
            // 
            this.menu.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.signInToolStripMenuItem);
            this.menuItem1.MenuItems.Add(this.signOutToolStripMenuItem);
            this.menuItem1.MenuItems.Add(this.menuItem4);
            this.menuItem1.MenuItems.Add(this.normalToolStripMenuItem);
            this.menuItem1.MenuItems.Add(this.compactToolStripMenuItem);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.historyToolStripMenuItem);
            this.menuItem1.MenuItems.Add(this.changePasswordToolStripMenuItem);
            this.menuItem1.MenuItems.Add(this.settingsToolStripMenuItem);
            this.menuItem1.MenuItems.Add(this.helpWebToolStripMenuItem);
            this.menuItem1.MenuItems.Add(this.aboutToolStripMenuItem);
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.MenuItems.Add(this.exitToolStripMenuItem);
            this.menuItem1.MenuItems.Add(this.changeAudioDeviceToolStripMenuItem);
            this.menuItem1.Text = "Menu";
            // 
            // signInToolStripMenuItem
            // 
            this.signInToolStripMenuItem.Text = "Sign in";
            this.signInToolStripMenuItem.Click += new System.EventHandler(this.signInToolStripMenuItem_Click);
            // 
            // signOutToolStripMenuItem
            // 
            this.signOutToolStripMenuItem.Text = "Sign Out";
            this.signOutToolStripMenuItem.Click += new System.EventHandler(this.signOutToolStripMenuItem_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "-";
            // 
            // normalToolStripMenuItem
            // 
            this.normalToolStripMenuItem.Text = "Normal";
            this.normalToolStripMenuItem.Click += new System.EventHandler(this.normalToolStripMenuItem_Click);
            // 
            // compactToolStripMenuItem
            // 
            this.compactToolStripMenuItem.Text = "Compact";
            this.compactToolStripMenuItem.Click += new System.EventHandler(this.compactToolStripMenuItem_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "-";
            // 
            // historyToolStripMenuItem
            // 
            this.historyToolStripMenuItem.Text = "History...";
            this.historyToolStripMenuItem.Click += new System.EventHandler(this.historyToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Text = "Change password...";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Text = "Settings...";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // helpWebToolStripMenuItem
            // 
            this.helpWebToolStripMenuItem.Text = "Help...";
            this.helpWebToolStripMenuItem.Click += new System.EventHandler(this.helpWebToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click_1);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "-";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // comboUsername
            // 
            this.comboUsername.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.comboUsername.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.comboUsername.Items.Add("test1");
            this.comboUsername.Items.Add("test2");
            this.comboUsername.Items.Add("test3");
            this.comboUsername.Items.Add("test4");
            this.comboUsername.Items.Add("test5");
            this.comboUsername.Items.Add("test6");
            this.comboUsername.Items.Add("test7");
            this.comboUsername.Location = new System.Drawing.Point(30, 29);
            this.comboUsername.Name = "comboUsername";
            this.comboUsername.Size = new System.Drawing.Size(180, 20);
            this.comboUsername.TabIndex = 16;
            this.comboUsername.Text = "test6";
            // 
            // labelError
            // 
            this.labelError.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelError.Location = new System.Drawing.Point(0, 142);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(240, 38);
            this.labelError.Text = "PTT ActiveX is not installed.";
            this.labelError.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelError.Visible = false;
            // 
            // changeAudioDeviceToolStripMenuItem
            // 
            this.changeAudioDeviceToolStripMenuItem.Text = "Change audio...";
            this.changeAudioDeviceToolStripMenuItem.Click += new System.EventHandler(this.menuItemAD_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.comboUsername);
            this.Controls.Add(this.buttonSignIn);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.labelUsername);
            this.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Menu = this.menu;
            this.Name = "MainForm";
            this.Text = "LoudtalksMesh Sample";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonSignIn;
		private System.Windows.Forms.Label labelPassword;
		private System.Windows.Forms.TextBox textPassword;
		private System.Windows.Forms.Label labelUsername;
		private System.Windows.Forms.MainMenu menu;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem signInToolStripMenuItem;
		private System.Windows.Forms.MenuItem signOutToolStripMenuItem;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ComboBox comboUsername;
		private System.Windows.Forms.Label labelError;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem normalToolStripMenuItem;
		private System.Windows.Forms.MenuItem compactToolStripMenuItem;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem changePasswordToolStripMenuItem;
		private System.Windows.Forms.MenuItem helpWebToolStripMenuItem;
		private System.Windows.Forms.MenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.MenuItem historyToolStripMenuItem;
		private System.Windows.Forms.MenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.MenuItem changeAudioDeviceToolStripMenuItem;
	}
}

