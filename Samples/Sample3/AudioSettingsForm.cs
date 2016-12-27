using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PttLib;

namespace Sample3
{
    public partial class AudioSettingsForm : Form
    {
        ISettings4 sett;
        public AudioSettingsForm(ISettings4 _sett)
        {
            sett = _sett;
            InitializeComponent();
        }

        private void UpdateDeviceLabel(Label lb, IAudioDevices iad, String strId)
        {
            for (int idx = 0; idx < iad.Count; ++idx)
            {
                if (iad.get_Id(idx).Equals(strId))
                {
                    lb.Text = iad.get_Name(idx) + " volume";
                    return;
                }
            }
        }

        private void AudioSettingsForm_Load(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Test with ptt control #" + sett.GetValue(SETTING_ID.ST_VERSION).ToString());
                UpdateDeviceLabel(lblPlayback, sett.GetPlaybackDevices(), sett.PlaybackDeviceId);
                UpdateDeviceLabel(lblRecording, sett.GetRecordingDevices(), sett.RecordingDeviceId);

                
                this.chkNoiseSupp.Checked = (bool)sett.GetValue(SETTING_ID.ST_AUD_NOISE_SUPP);
                this.tbPlayback.Value = (int)sett.GetValue(SETTING_ID.ST_AUD_PLAYBACK_VOLUME);
                this.tbRecording.Value = (int)sett.GetValue(SETTING_ID.ST_AUD_RECORDING_VOLUME);
                nudPlayback.Value = (int)sett.GetValue(SETTING_ID.ST_AUD_PLAYBACK_AMPL);
                nudRecording.Value = (int)sett.GetValue(SETTING_ID.ST_AUD_RECORDING_AMPL);
            }
            catch (System.Runtime.InteropServices.COMException cex)
            {
                System.Diagnostics.Debug.WriteLine("Can't read PTT property : " + cex.Message);
            }
        }

        private void nudRecording_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                sett.SetValue(SETTING_ID.ST_AUD_RECORDING_AMPL, nudRecording.Value);
            }
            catch (System.Runtime.InteropServices.COMException cex)
            {
                System.Diagnostics.Debug.WriteLine("Can't set recording amplification : " + cex.Message);
            }
        }

        private void nudPlayback_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                sett.SetValue(SETTING_ID.ST_AUD_PLAYBACK_AMPL, nudPlayback.Value);
            }
            catch (System.Runtime.InteropServices.COMException cex)
            {
                System.Diagnostics.Debug.WriteLine("Can't set playback amplification : " + cex.Message);
            }
        }

        private void chkNoiseSupp_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                sett.SetValue(SETTING_ID.ST_AUD_NOISE_SUPP, chkNoiseSupp.Checked);
            }
            catch (System.Runtime.InteropServices.COMException cex)
            {
                System.Diagnostics.Debug.WriteLine("Can't update noise suppression property : " + cex.Message);
            }
        }

        private void tbPlayback_Scroll(object sender, EventArgs e)
        {
            try
            {
                sett.SetValue(SETTING_ID.ST_AUD_PLAYBACK_VOLUME, tbPlayback.Value);
            }
            catch (System.Runtime.InteropServices.COMException cex)
            {
                System.Diagnostics.Debug.WriteLine("Can't set playback volume : " + cex.Message);
            }
        }

        private void tbRecording_Scroll(object sender, EventArgs e)
        {
            try
            {
                sett.SetValue(SETTING_ID.ST_AUD_RECORDING_VOLUME, tbRecording.Value);
            }
            catch (System.Runtime.InteropServices.COMException cex)
            {
                System.Diagnostics.Debug.WriteLine("Can't set recording volume : " + cex.Message);
            }
        }
    }
}
