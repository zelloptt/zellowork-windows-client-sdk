using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace SoundSample
{
	public partial class MainForm : Form
	{
		private bool bExitOnSignout;
		AxPttLib.AxPtt axMesh;
        PttLib.IContacts contactsMesh;
        PttLib.IAudioMessageRecord m_AudRecorder;
        AudioMessagePlaybackImpl m_AudPlayback;
        List<String> lstForwardContactIds = new List<string>();
        List<AudioMessageRecording> lstOutMessages = new List<AudioMessageRecording>();
        private Timer tmUpdateOnlineContacts = new Timer();
        private bool bUpdateOnlineContats = false;

        private String sDirAudioSave;
        private String sWavToSend;

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

                if (axMesh.Visible == bShow)
                {
                    axMesh.Dock = DockStyle.None;
                    axMesh.Dock = DockStyle.Fill;
                    axMesh.Visible = !bShow;
                    //split.Invalidate();
                    Invalidate(true);
                    split.Panel1.Refresh();
                }

                if (false == bShow)
                {
                    if (split.Panel2Collapsed)
                    {
                        split.Panel2Collapsed = false;
                        ClientSize = new Size(ClientSize.Width + 300, ClientSize.Height);
                        split.SplitterDistance = ClientSize.Width - 300;
                    }
                }
                else
                {
                    if (false == split.Panel2Collapsed)
                    {
                        Size sz = new Size(ClientSize.Width - split.Panel2.Width - split.SplitterWidth, ClientSize.Height);
                        split.Panel2Collapsed = true;
                        ClientSize = sz;
                    }
                }

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
				//Size sz = ClientRectangle.Size;
				//sz.Height -= pt.Y;
				//axMesh.Location = pt;
				//axMesh.Size = sz;
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
            cbSendAudioFile.ItemCheck += new ItemCheckEventHandler(cbSendAudioFile_ItemCheck);
            cbForwardAudio.ItemCheck += new ItemCheckEventHandler(cbForwardAudio_ItemCheck);
            
            tbSendWavPath.Enter += new EventHandler(tbSendWavPath_Enter);
            tbSendWavPath.Leave += new EventHandler(tbSendWavPath_Leave);
            tbSendWavPath.Text = "Select *.wav file";
            tbSendWavPath.ForeColor = Color.Gray;

            tbSaveMessagesPath.Enter += new EventHandler(tbSaveMessagesPath_Enter);
            tbSaveMessagesPath.Leave += new EventHandler(tbSaveMessagesPath_Leave);
            tbSaveMessagesPath.Text = "Select folder to save wav files";
            tbSaveMessagesPath.ForeColor = Color.Gray;

			try
			{
				// Try to create Loudtalks Mesh control instance
				// We demonstrate how to create control dynamically here
				// This way we can easily handle situation when Loudtalks Mesh ActiveX control is not registered in system
				// Alternative is to add the control to form in design mode
				axMesh = new AxPttLib.AxPtt();
				axMesh.BeginInit();
				//axMesh.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom |
				//	System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
				axMesh.TabIndex = textPassword.TabIndex + 1;
				axMesh.TabStop = false;
				//Controls.Add(axMesh);
                axMesh.Dock = DockStyle.Fill;
                split.Panel1.Controls.Add(axMesh);
				axMesh.EndInit();
                axMesh.Dock = DockStyle.None;
                Invalidate(true);
                axMesh.Dock = DockStyle.Fill;
                axMesh.Visible = false;
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
                axMesh.ContactListChanged += new EventHandler(axMesh_ContactListChanged);
				// Configure Loudtalks Mesh network parameters
				axMesh.Network.NetworkName = "default";
				axMesh.Network.LoginServer = "default.loudtalks.net";
                axMesh.Network.WebServer = "http://default.zellowork.com";
                PttLib.INetwork2 ntw2 = axMesh.Network as PttLib.INetwork2;
                if (ntw2 != null)
                    ntw2.EnableTls("tls.zellowork.com");
				// Customize using embedded oem.config
				System.Array OemConfig = Resource.oem_config;
				axMesh.Customization.set_OemConfigData(ref OemConfig);
				// Install tray icon
				axMesh.Settings.ShowTrayIcon = true;
				// Update UI
				UpdateCompactState();
				UpdateMenuState();
				UpdateControlsState();
                tmUpdateOnlineContacts.Interval = 5000;
                tmUpdateOnlineContacts.Tick += new EventHandler(tmUpdateOnlineContacts_Tick);
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

        private class ContactInfo
        {
            public string id;
            public string name;
            public bool selected = false;
            public override string ToString()
            {
                return name;
            }
        }
        List<ContactInfo> onliners = new List<ContactInfo>(10);

        void tmUpdateOnlineContacts_Tick(object sender, EventArgs e)
        {
            if (bUpdateOnlineContats)
            {
                List<ContactInfo> lst = new List<ContactInfo>(10);
                List<ContactInfo> lstLive = new List<ContactInfo>(10);
                bUpdateOnlineContats = false;
                PttLib.IContacts cnts = axMesh.Contacts;
                int idxMax = cnts.Count;
                for (int idx = 0; idx < idxMax; ++idx)
                {
                    PttLib.IContact cnt = cnts.get_Item(idx);
                    PttLib.ONLINE_STATUS st = cnt.Status;
                    if (st == PttLib.ONLINE_STATUS.OSAVAILABLE || st == PttLib.ONLINE_STATUS.OSSTANDBY || st == PttLib.ONLINE_STATUS.OSHEADPHONES
                        || st == PttLib.ONLINE_STATUS.OSAWAY || st == PttLib.ONLINE_STATUS.OSBUSY)
                    {
                        ContactInfo cn = new ContactInfo();
                        cn.id = cnt.Id;
                        if (cnt.Type == PttLib.CONTACT_TYPE.CTCHANNEL)
                            cn.name = cnt.Name + "(channel)";
                        else if(cnt.Type == PttLib.CONTACT_TYPE.CTGROUP)
                            cn.name = cnt.Name + "(group)";
                        else
                            cn.name = cnt.Name;
                        lst.Add(cn);
                        if (st != PttLib.ONLINE_STATUS.OSSTANDBY)
                            lstLive.Add(cn);
                    }
                }
                reloadList(cbForwardAudio, lstLive);
                reloadList(cbSendAudioFile, lst);
            }
        }

        void reloadList(CheckedListBox clb, List<ContactInfo> lst)
        {
            List<string> lst_checked = new List<string>(clb.CheckedItems.Count + 1);
            foreach (object chk in clb.CheckedItems)
            {
                ContactInfo ci = chk as ContactInfo;
                if (ci != null)
                {
                    lst_checked.Add(ci.id);
                }
            }
            clb.Items.Clear();
            foreach (ContactInfo c1 in lst)
            {
                clb.Items.Add(c1, lst_checked.Contains(c1.id));
            }
        }

        void axMesh_ContactListChanged(object sender, EventArgs e)
        {
            bUpdateOnlineContats = true;
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
            BeginInvoke((MethodInvoker)delegate
            {
                tmUpdateOnlineContacts.Start();
                if(null != contactsMesh)
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(contactsMesh);
                contactsMesh = axMesh.Contacts;
                cbChorus.Checked = (axMesh.GetOcx() as PttLib.IPtt4).Chorus;
                UpdateIntegrationControlsState();
            });
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
            BeginInvoke((MethodInvoker)delegate
            {
                tmUpdateOnlineContacts.Stop();
                if (null != contactsMesh)
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(contactsMesh);
                contactsMesh = null;
            });
        }

		public void axMesh_SignOutComplete(Object sender, EventArgs e)
		{
            BeginInvoke((MethodInvoker)delegate
            {
                tmUpdateOnlineContacts.Stop();
                if (null != contactsMesh)
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(contactsMesh);
                contactsMesh = null;
            });			// Sign out process has finished
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

        void Recording_AllDataWritten(object sender, EventArgs e)
        {
            AudioMessageRecording amr = sender as AudioMessageRecording;
            if (amr != null)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    lstOutMessages.Remove(amr);
                });
            }
        }

        private bool selectFolderToSave() {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            fbd.Description = "Folder to save incoming messages";
            fbd.SelectedPath = @"C:\projects\audio";
            DialogResult dr = fbd.ShowDialog();
            if (DialogResult.OK == dr) {
                sDirAudioSave = fbd.SelectedPath;
                tbSaveMessagesPath.Text = sDirAudioSave;
                tbSaveMessagesPath.ForeColor = Color.Black;
            }
            return DialogResult.OK == dr;
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            selectFolderToSave();
        }

        private void cbSaveMessages_CheckedChanged(object sender, EventArgs e)
        {
            if(cbSaveMessages.Checked) {
                if(String.IsNullOrEmpty(sDirAudioSave))
                    selectFolderToSave();
                if (String.IsNullOrEmpty(sDirAudioSave))
                    cbSaveMessages.Checked = false;
                else if (null != m_AudPlayback)
                    m_AudPlayback.savePath = sDirAudioSave;
            }
            else {
                sDirAudioSave = String.Empty;
                tbSaveMessagesPath.Text = String.Empty;
                if (null != m_AudPlayback)
                    m_AudPlayback.savePath = String.Empty;
            }
            UpdateIntegrationControlsState();
        }

        private void cbChorus_CheckedChanged(object sender, EventArgs e)
        {
            if (cbChorus.Checked)
                (axMesh.GetOcx() as PttLib.IPtt4).Chorus = true;
            else
                (axMesh.GetOcx() as PttLib.IPtt4).Chorus = false;
        }

        void cbForwardAudio_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ContactInfo ci = cbForwardAudio.Items[e.Index] as ContactInfo;
            if (e.NewValue == CheckState.Checked)
                lstForwardContactIds.Add(ci.id);
            else if (e.NewValue == CheckState.Unchecked && e.CurrentValue == CheckState.Checked)
                lstForwardContactIds.Remove(ci.id);
            if (m_AudPlayback != null)
                m_AudPlayback.bForwardAudio = (0 != lstForwardContactIds.Count);
        }

        void cbSendAudioFile_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (String.IsNullOrEmpty(sWavToSend))
                btnSend.Enabled = false;
            else {
                if(cbSendAudioFile.CheckedItems.Count == 0 && e.NewValue != CheckState.Checked)
                    btnSend.Enabled = false;
                else if(cbSendAudioFile.CheckedItems.Count == 1 && e.NewValue == CheckState.Unchecked && e.CurrentValue==CheckState.Checked)
                    btnSend.Enabled = false;
                else
                    btnSend.Enabled = true;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (m_AudRecorder != null && false==String.IsNullOrEmpty(sWavToSend))
            {
                CReadWavFile wf = new CReadWavFile(sWavToSend);
                if (wf.ChannelCount != 1 || wf.BitsPerSecond != 16)
                {
                    MessageBox.Show("Incompatible WAV file format", "Unable to send", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                PttLib.IContacts cnts = axMesh.Contacts;
                PttLib.IMessage msg = null;
                List<string> lst = new List<string>();
                foreach (object chk in cbSendAudioFile.CheckedItems)
                {
                    ContactInfo ci = chk as ContactInfo;
                    if (ci != null)
                    {
                        lst.Add(ci.id);
                    }
                }
                if (lst.Count > 0)
                {
                    PttLib.IAudioStream strm = m_AudRecorder.MessageOutBeginEx(lst.ToArray(), string.Empty, out msg);
                    AudioMessageRecording rec = new AudioMessageRecording(wf);
                    rec.AllDataWritten += new EventHandler(Recording_AllDataWritten);
                    lstOutMessages.Add(rec);
                    rec.SetStream(strm);
                }
            }
        }

        private void btnWavBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "wav";
            ofd.Multiselect = false;
            if(String.IsNullOrEmpty(sDirAudioSave))
                ofd.InitialDirectory = @"C:\projects\audio";
            else
                ofd.InitialDirectory = sDirAudioSave;
            ofd.Filter = "WAV files (*.wav)|*.wav|All files|*.*";
            DialogResult dr = ofd.ShowDialog();
            if (DialogResult.OK == dr) {
                tbSendWavPath.Text = ofd.FileName;
                tbSendWavPath.ForeColor = Color.Black;
                sWavToSend = tbSendWavPath.Text;
            }
            btnSend.Enabled = (false == String.IsNullOrEmpty(sWavToSend)) && (cbSendAudioFile.CheckedItems.Count > 0);
        }

        void tbSendWavPath_Enter(object sender, EventArgs e)
        {
            if (tbSendWavPath.ForeColor == Color.Black)
                return;
            tbSendWavPath.Text = String.Empty;
            tbSendWavPath.ForeColor = Color.Black;

        }
        void tbSendWavPath_Leave(object sender, EventArgs e)
        {
            sWavToSend = tbSendWavPath.Text.Trim();
            if (String.IsNullOrEmpty(sWavToSend))
            {
                tbSendWavPath.Text = "Select *.wav file";
                tbSendWavPath.ForeColor = Color.Gray;
            }

            btnSend.Enabled = (false == String.IsNullOrEmpty(sWavToSend)) && (cbSendAudioFile.CheckedItems.Count > 0 );
        }

        void tbSaveMessagesPath_Enter(object sender, EventArgs e)
        {
            if (tbSaveMessagesPath.ForeColor == Color.Black)
                return;
            tbSaveMessagesPath.Text = String.Empty;
            tbSaveMessagesPath.ForeColor = Color.Black;

        }
        void tbSaveMessagesPath_Leave(object sender, EventArgs e)
        {
            sDirAudioSave = tbSaveMessagesPath.Text.Trim();
            if (String.IsNullOrEmpty(sWavToSend))
            {
                tbSaveMessagesPath.Text = "Select folder to save wav files";
                tbSaveMessagesPath.ForeColor = Color.Gray;
            }
        }


        void startAudioForwarding(WavBuffer wb)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                if (m_AudRecorder != null && wb != null && contactsMesh!=null)
                {
                    PttLib.IMessage msg = null;
                    List<string> lst = new List<string>();
                    foreach (string id in lstForwardContactIds)
                    {
                        lst.Add(id);
                    }
                    if (lst.Count > 0)
                    {
                        PttLib.IAudioStream strm = m_AudRecorder.MessageOutBeginEx(lst.ToArray(), string.Empty, out msg);
                        AudioMessageRecording rec = new AudioMessageRecording(wb as SoundSample.IReadWav);
                        rec.AllDataWritten += new EventHandler(Recording_AllDataWritten);
                        lstOutMessages.Add(rec);
                        rec.SetStream(strm);
                    }
                }

            });
        }

        private void cbEnableAI_CheckedChanged(object sender, EventArgs e)
        {
            UpdateIntegrationControlsState();
            if (cbEnableAI.Checked)
            {
                m_AudPlayback = new AudioMessagePlaybackImpl(this.startAudioForwarding);
                m_AudRecorder = (axMesh.GetOcx() as PttLib.IPtt4).StartAudioMessageIntegration(m_AudPlayback);
            }
            else
            {
                (axMesh.GetOcx() as PttLib.IPtt4).StopAudioMessageIntegration();

                m_AudPlayback = null;
                m_AudRecorder = null;
                GC.Collect();
                lstForwardContactIds.Clear();
                foreach (int idx in cbForwardAudio.CheckedIndices)
                    cbForwardAudio.SetItemCheckState(idx, CheckState.Unchecked);
                foreach (int idx2 in this.cbSendAudioFile.CheckedIndices)
                    cbSendAudioFile.SetItemCheckState(idx2, CheckState.Unchecked);
            }
        }

        private void UpdateIntegrationControlsState()
        {
            cbSaveMessages.Enabled = cbEnableAI.Checked;
            btnSelect.Enabled = cbEnableAI.Checked && cbSaveMessages.Checked;
            cbSendAudioFile.Enabled = cbEnableAI.Checked;
            btnWavBrowse.Enabled = cbEnableAI.Checked;
            tbSendWavPath.Enabled = cbEnableAI.Checked;
            cbForwardAudio.Enabled = cbEnableAI.Checked;// && checkBoxFA.Checked;
        }
    }
}
