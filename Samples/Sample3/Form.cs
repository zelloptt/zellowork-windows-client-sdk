using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Sample3
{
	public partial class MainForm : Form
	{
		private bool bExitOnSignout;
		AxPttLib.AxPtt axMesh;
		public MainForm()
		{
			bExitOnSignout = false;
			InitializeComponent();
		}

		private void UpdateCompactState()
		{
			if (axMesh != null)
			{
				// Check/uncheck appropriate menu commands depending on Loudtalks Mesh contacts mode
				bool bCompact = axMesh.Settings.CompactContactList;
				normalToolStripMenuItem.Checked = !bCompact;
				compactToolStripMenuItem.Checked = bCompact;
			}
		}

		private void UpdateMenuState()
		{
			if (axMesh != null)
			{
				// Disable/enable appropriate menu commands depending on Loudtalks Mesh network status
				PttLib.NETWORK_STATUS Status = axMesh.NetworkStatus;
				signInToolStripMenuItem.Enabled = Status == PttLib.NETWORK_STATUS.NSOFFLINE;
				signOutToolStripMenuItem.Enabled = Status == PttLib.NETWORK_STATUS.NSONLINE;
                historyToolStripMenuItem.Enabled = Status == PttLib.NETWORK_STATUS.NSONLINE;
				changePasswordToolStripMenuItem.Enabled = Status == PttLib.NETWORK_STATUS.NSONLINE;
			}
		}

		private void UpdateControlsState()
		{
			if (axMesh != null)
			{
				// Query Loudtalks Mesh network status
				PttLib.NETWORK_STATUS Status = axMesh.NetworkStatus;
				bool bShow = Status == PttLib.NETWORK_STATUS.NSOFFLINE || Status == PttLib.NETWORK_STATUS.NSSIGNINGIN;
				bool bEnable = Status == PttLib.NETWORK_STATUS.NSOFFLINE;
				// Controls' state
				labelUsername.Visible = bShow;
				comboUsername.Visible = bShow;
				labelPassword.Visible = bShow;
				textPassword.Visible = bShow;
				comboUsername.Enabled = bEnable;
				textPassword.Enabled = bEnable;
				// Switch between "Sign in" and "Cancel" button modes
				if (Status == PttLib.NETWORK_STATUS.NSSIGNINGIN)
					buttonSignIn.Text = "Cancel";
				else
					buttonSignIn.Text = "Sign in";
				buttonSignIn.Visible = Status == PttLib.NETWORK_STATUS.NSOFFLINE || Status == PttLib.NETWORK_STATUS.NSSIGNINGIN;
				// Mesh control location
				Point pt = menu.Location;
				if (Status == PttLib.NETWORK_STATUS.NSOFFLINE || Status == PttLib.NETWORK_STATUS.NSSIGNINGIN)
					pt.Y = buttonSignIn.Location.Y + buttonSignIn.Size.Height;
				else
					pt.Y = menu.Location.Y + menu.Size.Height;
				Size sz = ClientRectangle.Size;
				sz.Height -= pt.Y;
				axMesh.Location = pt;
				axMesh.Size = sz;
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			try
			{
				// Try to create Loudtalks Mesh control instance
				// We demonstrate how to create control dynamically here
				// This way we can easily handle situation when Loudtalks Mesh ActiveX control is not registered in system
				// Alternative is to add the control to form in design mode
				axMesh = new AxPttLib.AxPtt();
				axMesh.BeginInit();
				axMesh.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom |
					System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
				axMesh.TabIndex = textPassword.TabIndex + 1;
				axMesh.TabStop = false;
				Controls.Add(axMesh);
				axMesh.EndInit();
			}
			catch (System.Runtime.InteropServices.COMException)
			{
				axMesh = null;
			}
			catch (Exception)
			{
				axMesh = null;
			}
			bExitOnSignout = false;
			if (axMesh != null)
			{
				// Wire Loudtalks Mesh control events
				axMesh.SignInStarted += new EventHandler(axMesh_SignInStarted);
				axMesh.SignInSucceeded += new EventHandler(axMesh_SignInSucceeded);
				axMesh.SignInFailed += new AxPttLib.IPttEvents_SignInFailedEventHandler(axMesh_SignInFailed);
				axMesh.SignInRequested+= new EventHandler(axMesh_SignInRequested);
				axMesh.SignOutStarted += new EventHandler(axMesh_SignOutStarted);
				axMesh.SignOutComplete += new EventHandler(axMesh_SignOutComplete);
				axMesh.GetCanSignIn += new AxPttLib.IPttEvents_GetCanSignInEventHandler(axMesh_GetCanSignIn);
				axMesh.MessageInBegin += new AxPttLib.IPttEvents_MessageInBeginEventHandler(axMesh_MessageInBegin);
				axMesh.MessageInEnd += new AxPttLib.IPttEvents_MessageInEndEventHandler(axMesh_MessageInEnd);
				axMesh.MessageOutBegin += new AxPttLib.IPttEvents_MessageOutBeginEventHandler(axMesh_MessageOutBegin);
				axMesh.MessageOutEnd += new AxPttLib.IPttEvents_MessageOutEndEventHandler(axMesh_MessageOutEnd);
				axMesh.MessageOutError += new AxPttLib.IPttEvents_MessageOutErrorEventHandler(axMesh_MessageOutError);
				axMesh.AudioMessageInStart += new AxPttLib.IPttEvents_AudioMessageInStartEventHandler(axMesh_AudioMessageInStart);
				axMesh.AudioMessageInStop += new AxPttLib.IPttEvents_AudioMessageInStopEventHandler(axMesh_AudioMessageInStop);
                axMesh.PlayerAudioMessageStart += new AxPttLib.IPttEvents_PlayerAudioMessageStartEventHandler(axMesh_PlayerAudioMessageStart);
                axMesh.PlayerAudioMessageStop += new AxPttLib.IPttEvents_PlayerAudioMessageStopEventHandler(axMesh_PlayerAudioMessageStop);
                axMesh.PlayerAudioMessageProgress += new AxPttLib.IPttEvents_PlayerAudioMessageProgressEventHandler(axMesh_PlayerAudioMessageProgress);
				// Configure Loudtalks Mesh network parameters
                axMesh.Network.NetworkName = "taximaster";//"zukabra";
                axMesh.Network.LoginServer = "taximaster.loudtalks.net";
                axMesh.Network.WebServer = "http://taximaster.zellowork.com";
                //PttLib.INetwork2 ntw2 = axMesh.Network as PttLib.INetwork2;
                //if (ntw2 != null)
                //    ntw2.EnableTls("tls.zellowork.com");
				// Customize using embedded oem.config
				System.Array OemConfig = Resource.oem_config;
				axMesh.Customization.set_OemConfigData(ref OemConfig);
				// Install tray icon
				axMesh.Settings.ShowTrayIcon = true;
				// Update UI
				UpdateCompactState();
				UpdateMenuState();
				UpdateControlsState();
			}
			else
			{
				// Loudtalks Mesh control is unavailable
				// Hide all controls
				foreach (ToolStripMenuItem Item in menu.Items)
					foreach (ToolStripItem SubItem in Item.DropDownItems)
						SubItem.Enabled = false;
				foreach (Control Ctrl in Controls)
					Ctrl.Visible = false;
				menu.Show();
				// Disable menu commands
				exitToolStripMenuItem.Enabled = true;
				helpWebToolStripMenuItem.Enabled = true;
				// Show error description
				labelError.Top = menu.Location.Y + menu.Size.Height +  10;
				labelError.Visible = true;
			}
		}

        private String MessageToCaption(PttLib.IAudioMessage pMessage)
        {
            StringBuilder sb = new StringBuilder("Replaying message ");
            if (pMessage.Incoming)
            {
                PttLib.IAudioInMessage msg = pMessage as PttLib.IAudioInMessage;
                sb.Append("received from ").Append(msg.Sender.Name);
                if (null != msg.Author && false == String.IsNullOrEmpty(msg.Author.Name))
                    sb.Append(" / ").Append(msg.Author.Name);
            }
            else
            {
                PttLib.IAudioOutMessage msg = pMessage as PttLib.IAudioOutMessage;
                sb.Append("sent to ").Append(msg.Recipients.get_Item(0).Name);
            }
            return sb.ToString();
        }

        void axMesh_PlayerAudioMessageProgress(object sender, AxPttLib.IPttEvents_PlayerAudioMessageProgressEvent e)
        {
            if (e.pMessage != null && e.pMessage.Type == PttLib.MESSAGE_TYPE.MTAUDIO)
            {
                System.Diagnostics.Debug.WriteLine("playback progress " + e.iProgress +"ms");
            }
        }

        void axMesh_PlayerAudioMessageStop(object sender, AxPttLib.IPttEvents_PlayerAudioMessageStopEvent e)
        {
            if (e.pMessage != null && e.pMessage.Type == PttLib.MESSAGE_TYPE.MTAUDIO)
            {
                System.Diagnostics.Debug.WriteLine(MessageToCaption(e.pMessage) + " has stopped");
            }
        }

        void axMesh_PlayerAudioMessageStart(object sender, AxPttLib.IPttEvents_PlayerAudioMessageStartEvent e)
        {
            if (e.pMessage != null && e.pMessage.Type == PttLib.MESSAGE_TYPE.MTAUDIO)
            {
                System.Diagnostics.Debug.WriteLine(MessageToCaption(e.pMessage) + " has started");
            }
        }

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (axMesh != null)
			{
				// It takes several seconds to sign out of Loudtalks Mesh network
				// Hide main window and tray icon as if application exited
				// Start signing out and destroy window later when sign out process is over
				Hide();
				axMesh.Settings.ShowTrayIcon = false;
				if (axMesh.NetworkStatus == PttLib.NETWORK_STATUS.NSONLINE ||
					axMesh.NetworkStatus == PttLib.NETWORK_STATUS.NSSIGNINGIN)
				{
					bExitOnSignout = true;
					axMesh.SignOut();
					e.Cancel = true;
				}
			}
		}

		public void axMesh_SignInStarted(Object sender, EventArgs e)
		{
			// Sign in process has started
			UpdateMenuState();
			UpdateControlsState();
		}

		public void axMesh_SignInSucceeded(Object sender, EventArgs e)
		{
			// Sign in process has finished successfully
			UpdateMenuState();
			UpdateControlsState();
			if (axMesh.Contacts.Count > 0)
				axMesh.SelectContact(axMesh.Contacts.get_Item(0));
		}

		public void axMesh_SignInFailed(Object sender, AxPttLib.IPttEvents_SignInFailedEvent e)
		{
			// Sign in process failed
			UpdateMenuState();
			UpdateControlsState();
		}

		public void axMesh_SignInRequested(Object sender, EventArgs e)
		{
			// "Sign in" popup menu command was activated in Loudtalks Mesh control
			signInToolStripMenuItem_Click(sender, e);
		}

		public void axMesh_SignOutStarted(Object sender, EventArgs e)
		{
			// Sign out process has started
			UpdateMenuState();
			UpdateControlsState();
		}

		public void axMesh_SignOutComplete(Object sender, EventArgs e)
		{
			// Sign out process has finished
			// Destroy hidden main window if sign out was initiated earlier
			// when user tried to exit while Loudtalks Mesh control was signed into network.
			if (bExitOnSignout)
			{
				if (axMesh != null)
					axMesh = null;
				Close();
			}
			else
			{
				UpdateMenuState();
				UpdateControlsState();
			}
		}

		public void axMesh_GetCanSignIn(object sender, AxPttLib.IPttEvents_GetCanSignInEvent e)
		{
			// Popup menu is activated from Loudtaklks Mesh control
			// and it needs to know if "Online", "Away" etc commands should be available to user
			e.pbVal = comboUsername.Text.Length > 0 && textPassword.Text.Length > 0;
		}

		public void axMesh_MessageInBegin(object sender, AxPttLib.IPttEvents_MessageInBeginEvent e)
		{
			if(e.pMessage != null)
			{
				PttLib.IAudioInMessage pMessage = (PttLib.IAudioInMessage)e.pMessage;
				if (pMessage != null)
				{
					PttLib.IContact pContact = pMessage.Sender;
					if (pContact != null)
						Debug.WriteLine("Incoming message " + e.pMessage.Id + " from " + pContact.Name + " begins");
				}
			}
		}

		public void axMesh_MessageInEnd(object sender, AxPttLib.IPttEvents_MessageInEndEvent e)
		{
			if (e.pMessage != null)
			{
				PttLib.IAudioInMessage pMessage = (PttLib.IAudioInMessage) e.pMessage;
				if (pMessage != null)
				{
					PttLib.IContact pContact = pMessage.Sender;
					if (pContact != null)
						Debug.WriteLine("Incoming message " + e.pMessage.Id + " from " + pContact.Name + " ends, duration " + pMessage.Duration);
				}
			}
		}

		public void axMesh_MessageOutBegin(object sender, AxPttLib.IPttEvents_MessageOutBeginEvent e)
		{
			if (e.pMessage != null && e.pContact != null)
			{
				Debug.WriteLine("Outgoing message " + e.pMessage.Id + " to " + e.pContact.Name + " begins");
			}
		}

		public void axMesh_MessageOutEnd(object sender, AxPttLib.IPttEvents_MessageOutEndEvent e)
		{
			if (e.pMessage != null && e.pContact != null)
			{
				PttLib.IAudioOutMessage pMessage = (PttLib.IAudioOutMessage)e.pMessage;
				if (pMessage != null)
					Debug.WriteLine("Outgoing message " + e.pMessage.Id + " to " + e.pContact.Name + " ends, duration " + pMessage.Duration);
			}
		}

		public void axMesh_MessageOutError(object sender, AxPttLib.IPttEvents_MessageOutErrorEvent e)
		{
			if (e.pMessage != null && e.pContact != null)
			{
				Debug.WriteLine("Outgoing message " + e.pMessage.Id + " to " + e.pContact.Name + " error");
			}
		}

		public void axMesh_AudioMessageInStart(object sender, AxPttLib.IPttEvents_AudioMessageInStartEvent e)
		{
			if (e.pMessage != null)
			{
				PttLib.IContact pContact = e.pMessage.Sender;
				if (pContact != null)
					Debug.WriteLine("Incoming message " + e.pMessage.Id + " from " + pContact.Name + " starts");
                // Activate incoming message if possible
                e.pbActivate = true;
			}
		}

		public void axMesh_AudioMessageInStop(object sender, AxPttLib.IPttEvents_AudioMessageInStopEvent e)
		{
			if (e.pMessage != null)
			{
				PttLib.IContact pContact = e.pMessage.Sender;
				if (pContact != null)
					Debug.WriteLine("Incoming message " + e.pMessage.Id + " from " + pContact.Name + " stops");
			}
		}

		private void signInToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// "Sign in" menu command was activated
			if (axMesh != null && axMesh.NetworkStatus == PttLib.NETWORK_STATUS.NSOFFLINE)
				axMesh.SignIn(comboUsername.Text, textPassword.Text, false);
		}

		private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// "Sign out" menu command was activated
			if (axMesh != null && axMesh.NetworkStatus == PttLib.NETWORK_STATUS.NSONLINE)
				axMesh.SignOut();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// "Exit" menu command was activated
			Close();
		}

		private void historyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// "History..." menu command was activated
			if (axMesh != null)
				axMesh.OpenHistory(null);
		}

		private void normalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// "Normal contasts" menu command was activated
			if (axMesh != null)
				axMesh.Settings.CompactContactList = false;
			UpdateCompactState();
		}

		private void compactToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// "Compact contasts" menu command was activated
			if (axMesh != null)
				axMesh.Settings.CompactContactList = true;
			UpdateCompactState();
		}

		private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// "Password Wizard" menu command was activated
			if (axMesh != null)
				axMesh.ShowPasswordWizard(0);
		}

		private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// "Options" menu command was activated
			if (axMesh != null)
				axMesh.ShowSettingsDialog(0);
		}

		private void helpWebToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// "Help" menu command was activated
            System.Diagnostics.Process.Start("http://support.zello.com/categories/20031828-Zello-Work", "");
		}

		private void reportAProblemToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// "Report a problem" menu command was activated
			if (axMesh != null)
				axMesh.ShowFeedbackDialog(0);
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// "About" menu command was activated
			if (axMesh != null)
				axMesh.ShowAboutDialog(0);
		}

		private void buttonSignIn_Click(object sender, EventArgs e)
		{
			// "Sign in/Cancel" button was pressed
			// Sign into network if control is offline,
			// cancel if control is signing in.
			if (axMesh != null)
			{
				PttLib.NETWORK_STATUS Status = axMesh.NetworkStatus;
				if (PttLib.NETWORK_STATUS.NSSIGNINGIN == Status)
					axMesh.Cancel();
				else
					signInToolStripMenuItem_Click(sender, e);
			}
		}

		private void comboUsername_KeyDown(object sender, KeyEventArgs e)
		{
			// Sign into network if "Return" key pressed while username combo is focused
			if (e.KeyCode == Keys.Return)
				signInToolStripMenuItem_Click(sender, e);
		}

		private void textPassword_KeyDown(object sender, KeyEventArgs e)
		{
			// Sign into network if "Return" key pressed while password edit is focused
			if (e.KeyCode == Keys.Return)
				signInToolStripMenuItem_Click(sender, e);
		}

	}
}
