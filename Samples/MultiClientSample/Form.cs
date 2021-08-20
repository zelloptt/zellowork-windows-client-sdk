using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace MultiClientSample
{
	public partial class MainForm : Form
	{
		private bool bExitOnSignout;
		ZelloPTTLib.ZelloClient ptt;
        ZelloPTTLib.IContacts contactsMesh;
        ZelloPTTLib.IAudioMessageRecord m_AudRecorder;
        AudioMessagePlaybackImpl m_AudPlayback;
        List<String> lstForwardContactIds = new List<string>();
        List<AudioMessageRecording> lstOutMessages = new List<AudioMessageRecording>();
        private Timer tmUpdateOnlineContacts = new Timer();
        private bool bUpdateOnlineContats = false;

        private String sDirAudioSave;
        private String sWavToSend;
        private String sZelloNetworkName;

        public UInt16 ZelloClientId { get; private set; }

		public MainForm(String zelloNetworkName, UInt16 zelloClientId)
		{
            sZelloNetworkName = zelloNetworkName;
            ZelloClientId = zelloClientId;
			bExitOnSignout = false;
			InitializeComponent();
		}

		private void UpdateMenuState()
		{
            if (ptt != null)
			{
				// Disable/enable appropriate menu commands depending on Loudtalks Mesh network status
				ZelloPTTLib.NETWORK_STATUS Status = ptt.NetworkStatus;
                signInToolStripMenuItem.Enabled = Status == ZelloPTTLib.NETWORK_STATUS.NSOFFLINE;
                signOutToolStripMenuItem.Enabled = Status == ZelloPTTLib.NETWORK_STATUS.NSONLINE;
			}
		}

		private void UpdateControlsState()
		{
			if (ptt != null)
			{
				// Query Loudtalks Mesh network status
				ZelloPTTLib.NETWORK_STATUS Status = ptt.NetworkStatus;
				bool bShow = Status == ZelloPTTLib.NETWORK_STATUS.NSOFFLINE || Status == ZelloPTTLib.NETWORK_STATUS.NSSIGNINGIN;
				bool bEnable = Status == ZelloPTTLib.NETWORK_STATUS.NSOFFLINE;

                if (false == bShow)
                {
                    if (split.Panel2Collapsed)
                    {
                        split.Panel1Collapsed = true;
                        split.Panel2Collapsed = false;
                        //ClientSize = new Size(ClientSize.Width + 300, ClientSize.Height);
                        //split.SplitterDistance = ClientSize.Width - 300;
                    }
                }
                else
                {
                    if (false == split.Panel2Collapsed)
                    {
                        //Size sz = new Size(ClientSize.Width - split.Panel2.Width - split.SplitterWidth, ClientSize.Height);
                        split.Panel2Collapsed = true;
                        split.Panel1Collapsed = false;
                        //ClientSize = sz;
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
				if (Status == ZelloPTTLib.NETWORK_STATUS.NSSIGNINGIN)
					buttonSignIn.Text = "Cancel";
				else
					buttonSignIn.Text = "Sign in";
				buttonSignIn.Visible = Status == ZelloPTTLib.NETWORK_STATUS.NSOFFLINE || Status == ZelloPTTLib.NETWORK_STATUS.NSSIGNINGIN;
				// Mesh control location
				Point pt = menu.Location;
				if (Status == ZelloPTTLib.NETWORK_STATUS.NSOFFLINE || Status == ZelloPTTLib.NETWORK_STATUS.NSSIGNINGIN)
					pt.Y = buttonSignIn.Location.Y + buttonSignIn.Size.Height;
				else
					pt.Y = menu.Location.Y + menu.Size.Height;
				//Size sz = ClientRectangle.Size;
				//sz.Height -= pt.Y;
				//ptt.Location = pt;
				//ptt.Size = sz;
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
                ptt = new ZelloPTTLib.ZelloClient();
			}
			catch (System.Runtime.InteropServices.COMException)
			{
				ptt = null;
			}
			catch (Exception)
			{
				ptt = null;
			}
			bExitOnSignout = false;
			if (ptt != null)
			{
				// Wire Loudtalks Mesh control events
                ptt.SignInStarted +=new ZelloPTTLib.IZelloClientEvents_SignInStartedEventHandler(ptt_SignInStarted);
                ptt.SignInSucceeded += new ZelloPTTLib.IZelloClientEvents_SignInSucceededEventHandler(ptt_SignInSucceeded);
                ptt.SignInFailed += new ZelloPTTLib.IZelloClientEvents_SignInFailedEventHandler(ptt_SignInFailed);
                ptt.SignInRequested += new ZelloPTTLib.IZelloClientEvents_SignInRequestedEventHandler(ptt_SignInRequested);
                ptt.SignOutStarted += new ZelloPTTLib.IZelloClientEvents_SignOutStartedEventHandler(ptt_SignOutStarted);
                ptt.SignOutComplete += new ZelloPTTLib.IZelloClientEvents_SignOutCompleteEventHandler(ptt_SignOutComplete);
                ptt.GetCanSignIn += new ZelloPTTLib.IZelloClientEvents_GetCanSignInEventHandler(ptt_GetCanSignIn);
                ptt.MessageInBegin += new ZelloPTTLib.IZelloClientEvents_MessageInBeginEventHandler(ptt_MessageInBegin);
                ptt.MessageInEnd += new ZelloPTTLib.IZelloClientEvents_MessageInEndEventHandler(ptt_MessageInEnd);
                ptt.MessageOutBegin += new ZelloPTTLib.IZelloClientEvents_MessageOutBeginEventHandler(ptt_MessageOutBegin);
                ptt.MessageOutEnd += new ZelloPTTLib.IZelloClientEvents_MessageOutEndEventHandler(ptt_MessageOutEnd);
                ptt.MessageOutError += new ZelloPTTLib.IZelloClientEvents_MessageOutErrorEventHandler(ptt_MessageOutError);
                ptt.AudioMessageInStart += new ZelloPTTLib.IZelloClientEvents_AudioMessageInStartEventHandler(ptt_AudioMessageInStart);
                ptt.AudioMessageInStop += new ZelloPTTLib.IZelloClientEvents_AudioMessageInStopEventHandler(ptt_AudioMessageInStop);
                ptt.PlayerAudioMessageStart += new ZelloPTTLib.IZelloClientEvents_PlayerAudioMessageStartEventHandler(ptt_PlayerAudioMessageStart);
                ptt.PlayerAudioMessageStop += new ZelloPTTLib.IZelloClientEvents_PlayerAudioMessageStopEventHandler(ptt_PlayerAudioMessageStop);
                ptt.PlayerAudioMessageProgress += new ZelloPTTLib.IZelloClientEvents_PlayerAudioMessageProgressEventHandler(ptt_PlayerAudioMessageProgress);
                ptt.ContactListChanged += new ZelloPTTLib.IZelloClientEvents_ContactListChangedEventHandler(ptt_ContactListChanged);
				// Configure Loudtalks Mesh network parameters
                ptt.Network.NetworkName = sZelloNetworkName;
				ptt.Network.LoginServer = sZelloNetworkName + ".loudtalks.net";
                ptt.Network.WebServer = "http://" + sZelloNetworkName + ".zellowork.com";
                ptt.Network.EnableTls("tls.zellowork.com");
				// Customize using embedded oem.config
				// Update UI
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
				// Show error description
                labelError.Parent = this;
                labelError.Dock = DockStyle.Fill;
				//labelError.Top = menu.Location.Y + menu.Size.Height +  10;
                //labelError.AutoSize = true;
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
                ZelloPTTLib.IContacts cnts = ptt.Contacts;
                int idxMax = cnts.Count;
                for (int idx = 0; idx < idxMax; ++idx)
                {
                    ZelloPTTLib.IContact cnt = cnts.get_Item(idx);
                    ZelloPTTLib.ONLINE_STATUS st = cnt.Status;
                    if (st == ZelloPTTLib.ONLINE_STATUS.OSAVAILABLE || st == ZelloPTTLib.ONLINE_STATUS.OSSTANDBY || st == ZelloPTTLib.ONLINE_STATUS.OSHEADPHONES
                        || st == ZelloPTTLib.ONLINE_STATUS.OSAWAY || st == ZelloPTTLib.ONLINE_STATUS.OSBUSY)
                    {
                        ContactInfo cn = new ContactInfo();
                        cn.id = cnt.Id;
                        if (cnt.Type == ZelloPTTLib.CONTACT_TYPE.CTCHANNEL)
                            cn.name = cnt.Name + "(channel)";
                        else if(cnt.Type == ZelloPTTLib.CONTACT_TYPE.CTGROUP)
                            cn.name = cnt.Name + "(group)";
                        else
                            cn.name = cnt.Name;
                        lst.Add(cn);
                        if (st != ZelloPTTLib.ONLINE_STATUS.OSSTANDBY)
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

        void ptt_ContactListChanged()
        {
            bUpdateOnlineContats = true;
        }

        private String MessageToCaption(ZelloPTTLib.IAudioMessage pMessage)
        {
            StringBuilder sb = new StringBuilder("Replaying message ");
            if (pMessage.Incoming)
            {
                ZelloPTTLib.IAudioInMessage msg = pMessage as ZelloPTTLib.IAudioInMessage;
                sb.Append("received from ").Append(msg.Sender.Name);
                if (null != msg.Author && false == String.IsNullOrEmpty(msg.Author.Name))
                    sb.Append(" / ").Append(msg.Author.Name);
            }
            else
            {
                ZelloPTTLib.IAudioOutMessage msg = pMessage as ZelloPTTLib.IAudioOutMessage;
                sb.Append("sent to ").Append(msg.Recipients.get_Item(0).Name);
            }
            return sb.ToString();
        }

        void ptt_PlayerAudioMessageProgress(ZelloPTTLib.IAudioMessage pMessage, int iProgress)
        {
            if (pMessage != null && pMessage.Type == ZelloPTTLib.MESSAGE_TYPE.MTAUDIO)
            {
                System.Diagnostics.Debug.WriteLine("playback progress " + iProgress + "ms");
            }
        }

        void ptt_PlayerAudioMessageStop(ZelloPTTLib.IAudioMessage pMessage)
        {
            if (pMessage != null && pMessage.Type == ZelloPTTLib.MESSAGE_TYPE.MTAUDIO)
            {
                System.Diagnostics.Debug.WriteLine(MessageToCaption(pMessage) + " has stopped");
            }
        }

        void ptt_PlayerAudioMessageStart(ZelloPTTLib.IAudioMessage pMessage)
        {
            if (pMessage != null && pMessage.Type == ZelloPTTLib.MESSAGE_TYPE.MTAUDIO)
            {
                System.Diagnostics.Debug.WriteLine(MessageToCaption(pMessage) + " has started");
            }
        }

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (ptt != null)
			{
				// It takes several seconds to sign out of Loudtalks Mesh network
				// Hide main window and tray icon as if application exited
				// Start signing out and destroy window later when sign out process is over
				Hide();
				ptt.Settings.ShowTrayIcon = false;
				if (ptt.NetworkStatus == ZelloPTTLib.NETWORK_STATUS.NSONLINE ||
					ptt.NetworkStatus == ZelloPTTLib.NETWORK_STATUS.NSSIGNINGIN)
				{
					bExitOnSignout = true;
					ptt.SignOut();
					e.Cancel = true;
				}
			}
            Program.inf.OnNormalZClientClose(ZelloClientId);
		}

		public void ptt_SignInStarted()
		{
			// Sign in process has started
            BeginInvoke((MethodInvoker)delegate
            {
			    UpdateMenuState();
			    UpdateControlsState();
            });
		}

        public void ptt_SignInSucceeded()
		{
			// Sign in process has finished successfully
            BeginInvoke((MethodInvoker)delegate
            {
                this.Text = "Zello client #" + ZelloClientId.ToString() + " : " + comboUsername.Text;
                UpdateMenuState();
                UpdateControlsState();
                tmUpdateOnlineContacts.Start();
                if(null != contactsMesh)
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(contactsMesh);
                contactsMesh = ptt.Contacts;
                UpdateIntegrationControlsState();
            });
		}

        void ptt_SignInFailed(ZelloPTTLib.CLIENT_ERROR ErrorCode)
		{
			// Sign in process failed
            BeginInvoke((MethodInvoker)delegate
            {
			    UpdateMenuState();
			    UpdateControlsState();
            });
		}

		public void ptt_SignInRequested()
		{
			// "Sign in" popup menu command was activated in Loudtalks Mesh control
			//signInToolStripMenuItem_Click(sender, e);
		}

		public void ptt_SignOutStarted()
		{
			// Sign out process has started
            BeginInvoke((MethodInvoker)delegate
            {
                UpdateMenuState();
                UpdateControlsState();
                tmUpdateOnlineContacts.Stop();
                if (null != contactsMesh)
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(contactsMesh);
                contactsMesh = null;
            });
        }

		public void ptt_SignOutComplete()
		{
            BeginInvoke((MethodInvoker)delegate
            {
                this.Text = "Zello client #" + ZelloClientId.ToString();
                tmUpdateOnlineContacts.Stop();
                if (null != contactsMesh)
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(contactsMesh);
                contactsMesh = null;
                // Destroy hidden main window if sign out was initiated earlier
                // when user tried to exit while Loudtalks Mesh control was signed into network.
                if (bExitOnSignout)
                {
                    if (ptt != null)
                        ptt = null;
                    Close();
                }
                else
                {
                    UpdateMenuState();
                    UpdateControlsState();
                }
            });			// Sign out process has finished
		}

        void ptt_GetCanSignIn(ref bool pbVal)
        {
			// Popup menu is activated from Loudtaklks Mesh control
			// and it needs to know if "Online", "Away" etc commands should be available to user
			pbVal = comboUsername.Text.Length > 0 && textPassword.Text.Length > 0;
		}

        void ptt_MessageInBegin(ZelloPTTLib.IMessage pMsg)
		{
            if (pMsg != null)
			{
				ZelloPTTLib.IAudioInMessage pMessage = (ZelloPTTLib.IAudioInMessage)pMsg;
				if (pMessage != null)
				{
					ZelloPTTLib.IContact pContact = pMessage.Sender;
					if (pContact != null)
						Debug.WriteLine("Incoming message " + pMsg.Id + " from " + pContact.Name + " begins");
				}
			}
		}

        void ptt_MessageInEnd(ZelloPTTLib.IMessage pMsg)
		{
			if (pMsg != null)
			{
				ZelloPTTLib.IAudioInMessage pMessage = (ZelloPTTLib.IAudioInMessage) pMsg;
				if (pMessage != null)
				{
					ZelloPTTLib.IContact pContact = pMessage.Sender;
					if (pContact != null)
						Debug.WriteLine("Incoming message " + pMsg.Id + " from " + pContact.Name + " ends, duration " + pMessage.Duration);
				}
			}
		}

        void ptt_MessageOutBegin(ZelloPTTLib.IMessage pMessage, ZelloPTTLib.IContact pContact)
		{
			if (pMessage != null && pContact != null)
			{
				Debug.WriteLine("Outgoing message " + pMessage.Id + " to " + pContact.Name + " begins");
			}
		}

        void ptt_MessageOutEnd(ZelloPTTLib.IMessage pMessage, ZelloPTTLib.IContact pContact)
        {
			if (pMessage != null && pContact != null)
			{
				ZelloPTTLib.IAudioOutMessage pAudioMessage = (ZelloPTTLib.IAudioOutMessage)pMessage;
                if (pAudioMessage != null)
                    Debug.WriteLine("Outgoing message " + pMessage.Id + " to " + pContact.Name + " ends, duration " + pAudioMessage.Duration);
			}
		}

        void ptt_MessageOutError(ZelloPTTLib.IMessage pMessage, ZelloPTTLib.IContact pContact)
		{
			if (pMessage != null && pContact != null)
			{
				Debug.WriteLine("Outgoing message " + pMessage.Id + " to " + pContact.Name + " error");
			}
		}

        void ptt_AudioMessageInStart(ZelloPTTLib.IAudioInMessage pMessage, ref bool pbActivate)
        {
			if (pMessage != null)
			{
				ZelloPTTLib.IContact pContact = pMessage.Sender;
				if (pContact != null)
					Debug.WriteLine("Incoming message " + pMessage.Id + " from " + pContact.Name + " starts");
                // Activate incoming message if possible
                pbActivate = true;
			}
		}

        void ptt_AudioMessageInStop(ZelloPTTLib.IAudioInMessage pMessage)
		{
			if (pMessage != null)
			{
				ZelloPTTLib.IContact pContact = pMessage.Sender;
				if (pContact != null)
					Debug.WriteLine("Incoming message " + pMessage.Id + " from " + pContact.Name + " stops");
			}
		}

		private void signInToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// "Sign in" menu command was activated
			if (ptt != null && ptt.NetworkStatus == ZelloPTTLib.NETWORK_STATUS.NSOFFLINE)
				ptt.SignIn(comboUsername.Text, textPassword.Text, false);
		}

		private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// "Sign out" menu command was activated
			if (ptt != null && ptt.NetworkStatus == ZelloPTTLib.NETWORK_STATUS.NSONLINE)
				ptt.SignOut();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// "Exit" menu command was activated
			Close();
		}

		private void buttonSignIn_Click(object sender, EventArgs e)
		{
			// "Sign in/Cancel" button was pressed
			// Sign into network if control is offline,
			// cancel if control is signing in.
			if (ptt != null)
			{
				ZelloPTTLib.NETWORK_STATUS Status = ptt.NetworkStatus;
				if (ZelloPTTLib.NETWORK_STATUS.NSSIGNINGIN == Status)
					ptt.Cancel();
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

        void SendFile_AllDataWritten(object sender, EventArgs e)
        {
            AudioMessageRecording amr = sender as AudioMessageRecording;
            if (amr != null)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    lstOutMessages.Remove(amr);
                    pbSendProgress.Visible = false;
                });
            }
        }

        void ForwardAudio_AllDataWritten(object sender, EventArgs e)
        {
            AudioMessageRecording amr = sender as AudioMessageRecording;
            if (amr != null)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    lstOutMessages.Remove(amr);
                    pbForwardAudio.Visible = false;
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
                ZelloPTTLib.IContacts cnts = ptt.Contacts;
                ZelloPTTLib.IMessage msg = null;
                List<string> lst = new List<string>();
                try
                {
                    foreach (object chk in cbSendAudioFile.CheckedItems)
                    {
                        ContactInfo ci = chk as ContactInfo;
                        if (ci != null)
                        {
                            lst.Add(ci.id);
                        }
                    }
                }
                catch (System.Exception)
                {}
                if (lst.Count > 0)
                {
                    ZelloPTTLib.IAudioStream strm = m_AudRecorder.MessageOutBeginEx(lst.ToArray(), string.Empty, out msg);
                    if (strm != null && msg != null)
                    {
                        pbSendProgress.Visible = true;
                        AudioMessageRecording rec = new AudioMessageRecording(wf);
                        rec.AllDataWritten += new EventHandler(SendFile_AllDataWritten);
                        lstOutMessages.Add(rec);
                        rec.SetStream(strm);
                    }
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
                    ZelloPTTLib.IMessage msg = null;
                    List<string> lst = new List<string>();
                    foreach (string id in lstForwardContactIds)
                    {
                        lst.Add(id);
                    }
                    if (lst.Count > 0)
                    {
                        ZelloPTTLib.IAudioStream strm = m_AudRecorder.MessageOutBeginEx(lst.ToArray(), string.Empty, out msg);
                        if (strm != null && msg != null)
                        {
                            pbForwardAudio.Visible = true;
                            AudioMessageRecording rec = new AudioMessageRecording(wb as MultiClientSample.IReadWav);
                            rec.AllDataWritten += new EventHandler(ForwardAudio_AllDataWritten);
                            lstOutMessages.Add(rec);
                            rec.SetStream(strm);
                        }
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
                m_AudRecorder = ptt.StartAudioMessageIntegration(m_AudPlayback);
            }
            else
            {
                ptt.StopAudioMessageIntegration();

                m_AudPlayback = null;
                m_AudRecorder = null;
                GC.Collect();
                lstForwardContactIds.Clear();
                foreach (int idx in cbForwardAudio.CheckedIndices)
                    cbForwardAudio.SetItemCheckState(idx, CheckState.Unchecked);
                foreach (int idx2 in this.cbSendAudioFile.CheckedIndices)
                    cbSendAudioFile.SetItemCheckState(idx2, CheckState.Unchecked);
            }
            ptt.Chorus = cbEnableAI.Checked;
            lbSendAudio.ForeColor = cbEnableAI.Checked ? System.Drawing.SystemColors.ControlText : System.Drawing.SystemColors.GrayText;
            lbForwardAudio.ForeColor = cbEnableAI.Checked ? System.Drawing.SystemColors.ControlText : System.Drawing.SystemColors.GrayText; ;
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
