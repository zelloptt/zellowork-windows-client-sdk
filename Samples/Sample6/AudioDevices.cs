using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sample6
{
    public partial class AudioDevices : Form
    {
        PttLib.ISettings2 m_settings;
        System.Collections.Generic.List<String> lstPlaybackDeviceIds;
        System.Collections.Generic.List<String> lstRecordingDeviceIds;
        public AudioDevices(PttLib.ISettings2 _sett)
        {
            m_settings = _sett;
            lstPlaybackDeviceIds = new List<string>();
            lstRecordingDeviceIds = new List<string>();
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            loadValues();
        }

        private void loadAudioDevices(PttLib.IAudioDevices devs, ComboBox cb, String strActiveDevice, ref System.Collections.Generic.List<String> lst)
        {
	        lst.Clear();
            cb.Items.Clear();
	        int nCount = devs.Count;
	        int nSelectedDeviceIdx = 0;
	        for(int i=0;i<nCount;++i) {
		        String strName = devs.get_Name(i);
		        String strId = devs.get_Id(i);
		        lst.Add(strId);
		        if(strActiveDevice.CompareTo(strId)==0) {
			        strName = strName.Insert(0,"*** ");
			        nSelectedDeviceIdx = i;
                }
                cb.Items.Add(strName);
	        }
            cb.SelectedIndex = nSelectedDeviceIdx;
        }

        private void loadValues()
        {
            PttLib.IAudioDevices pPlaybackDevices = m_settings.GetPlaybackDevices();
	        PttLib.IAudioDevices pRecordDevices = m_settings.GetRecordingDevices();
            
            String strPlaybackDeviceId = m_settings.PlaybackDeviceId;
	        String strRecordingDeviceId = m_settings.RecordingDeviceId;

	        loadAudioDevices(pPlaybackDevices, cbPlaybackDevices, strPlaybackDeviceId, ref lstPlaybackDeviceIds);
	        loadAudioDevices(pRecordDevices, cbRecordingDevices, strRecordingDeviceId, ref lstRecordingDeviceIds);
        }

        private void buttonClearRecordingDevice_Click(object sender, EventArgs e)
        {
            m_settings.RecordingDeviceId = String.Empty;
        }

        private void buttonClearPlaybackDevice_Click(object sender, EventArgs e)
        {
            m_settings.PlaybackDeviceId = String.Empty;
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            int nIdx = cbPlaybackDevices.SelectedIndex;
	        if(nIdx>=0 && nIdx < lstPlaybackDeviceIds.Count)
		        m_settings.PlaybackDeviceId = lstPlaybackDeviceIds[nIdx];

            nIdx = cbRecordingDevices.SelectedIndex;
            if(nIdx>=0 && nIdx < lstRecordingDeviceIds.Count)
                m_settings.RecordingDeviceId = lstRecordingDeviceIds[nIdx];

	        loadValues();
        }

        private void AudioDevices_Load(object sender, EventArgs e)
        {
            loadValues();
        }

    }
}