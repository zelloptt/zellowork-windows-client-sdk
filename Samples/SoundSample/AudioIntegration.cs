using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SoundSample
{
    public partial class AudioIntegration : Form
    {
        AxPttLib.AxPtt axMesh;

        public AudioIntegration()
        {
            try
            {
                InitializeComponent();
            }
            catch(System.Exception e) {
                string s = e.Message;
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AudioIntegration_Load(object sender, EventArgs e)
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
                axMesh.TabIndex = 1;
                axMesh.TabStop = false;
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
            if (axMesh != null)
            {
                // Wire Loudtalks Mesh control events
                //axMesh.SignInStarted += new EventHandler(axMesh_SignInStarted);
                //axMesh.SignInSucceeded += new EventHandler(axMesh_SignInSucceeded);
                //axMesh.SignInFailed += new AxPttLib.IPttEvents_SignInFailedEventHandler(axMesh_SignInFailed);
                //axMesh.SignInRequested += new EventHandler(axMesh_SignInRequested);
                //axMesh.SignOutStarted += new EventHandler(axMesh_SignOutStarted);
                //axMesh.SignOutComplete += new EventHandler(axMesh_SignOutComplete);
                //axMesh.GetCanSignIn += new AxPttLib.IPttEvents_GetCanSignInEventHandler(axMesh_GetCanSignIn);
                //axMesh.MessageInBegin += new AxPttLib.IPttEvents_MessageInBeginEventHandler(axMesh_MessageInBegin);
                //axMesh.MessageInEnd += new AxPttLib.IPttEvents_MessageInEndEventHandler(axMesh_MessageInEnd);
                //axMesh.MessageOutBegin += new AxPttLib.IPttEvents_MessageOutBeginEventHandler(axMesh_MessageOutBegin);
                //axMesh.MessageOutEnd += new AxPttLib.IPttEvents_MessageOutEndEventHandler(axMesh_MessageOutEnd);
                //axMesh.MessageOutError += new AxPttLib.IPttEvents_MessageOutErrorEventHandler(axMesh_MessageOutError);
                //axMesh.AudioMessageInStart += new AxPttLib.IPttEvents_AudioMessageInStartEventHandler(axMesh_AudioMessageInStart);
                //axMesh.AudioMessageInStop += new AxPttLib.IPttEvents_AudioMessageInStopEventHandler(axMesh_AudioMessageInStop);
                //axMesh.PlayerAudioMessageStart += new AxPttLib.IPttEvents_PlayerAudioMessageStartEventHandler(axMesh_PlayerAudioMessageStart);
                //axMesh.PlayerAudioMessageStop += new AxPttLib.IPttEvents_PlayerAudioMessageStopEventHandler(axMesh_PlayerAudioMessageStop);
                //axMesh.PlayerAudioMessageProgress += new AxPttLib.IPttEvents_PlayerAudioMessageProgressEventHandler(axMesh_PlayerAudioMessageProgress);
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
            }
            else
            {
                // Loudtalks Mesh control is unavailable
                // Hide all controls
                //foreach (ToolStripMenuItem Item in menu.Items)
                //    foreach (ToolStripItem SubItem in Item.DropDownItems)
                //        SubItem.Enabled = false;
                //foreach (Control Ctrl in Controls)
                //    Ctrl.Visible = false;
                //menu.Show();
                //// Disable menu commands
                //exitToolStripMenuItem.Enabled = true;
                //helpWebToolStripMenuItem.Enabled = true;
                //// Show error description
                //labelError.Top = menu.Location.Y + menu.Size.Height + 10;
                //labelError.Visible = true;
            }

            //m_AudPlayback = new SoundSample.ZelloToFile(@"C:\Users\Eugene\Documents\");
            //m_AudRecorder = (axMesh.GetOcx() as PttLib.IPtt4).StartAudioMessageIntegration(m_AudPlayback);
            //this.enableToolStripMenuItem.Checked = true;
            //this.stopIntegrationToolStripMenuItem.Checked = false;
            //this.immediateReplyToolStripMenuItem.Checked = false;
        }
    }
}
