using System;
using System.Collections.Generic;
using System.Text;

namespace SoundSample
{
    class AudioMessagePlaybackImpl : PttLib.IAudioMessagePlayback
    {
        private int cntMessages = 1;
        List<AudioStreamImpl> lstActiveStreams = new List<AudioStreamImpl>();
        public AudioMessagePlaybackImpl(WavBuffer.dlgtAudioRcvStarted _dlgt)
        {
            savePath = string.Empty;
            dlgt = _dlgt;
        }

        #region IAudioMessagePlayback Members

        PttLib.IAudioStream PttLib.IAudioMessagePlayback.MessageInBegin(PttLib.IMessage pMessage)
        {
            AudioStreamImpl rv = new AudioStreamImpl(cntMessages);
            if(false==String.IsNullOrEmpty(savePath)) {
                WriteWav ww = new WriteWav(GetSaveFileName(pMessage));
                if (ww.Valid)
                    rv.AddSink(ww);
            }
            if (bForwardAudio)
            {
                WavBuffer wb = new WavBuffer();
                wb.AudioRcvStarted += dlgt;
                rv.AddSink(wb);
            }
            cntMessages++;
            lstActiveStreams.Add(rv);
            rv.OnEndOfStream +=new AudioStreamImpl.dlgtFinished(OnEndOfStream);
            return rv as PttLib.IAudioStream;
        }

        int PttLib.IAudioMessagePlayback.NativeSampleRate
        {
            get { return 0; }
        }

        #endregion

        public String savePath { get; set; }
        public bool bForwardAudio = false;
        WavBuffer.dlgtAudioRcvStarted dlgt;
        ///public List<PttLib.IContact> lstForwardAudio = new List<PttLib.IContact>();

        String GetSaveFileName(PttLib.IMessage pMessage)
        {
            StringBuilder sb = new StringBuilder(savePath);
            sb.Append(@"\msg");
            sb.Append((cntMessages).ToString("D4"));
            PttLib.IContact cnt = null;
            if (pMessage.Incoming)
            {
                PttLib.IAudioInMessage pAIM = pMessage as PttLib.IAudioInMessage;
                if (pAIM != null)
                {
                    sb.Append("(");
                    cnt = pAIM.Sender;
                    if (cnt != null)
                        sb.Append(cnt.Name);
                    if (pAIM.Author != null)
                    {
                        sb.Append("__");
                        sb.Append(pAIM.Author.Name);
                    }
                    sb.Append(")");
                }
            }
            sb.Append(".wav");
            return sb.ToString();
        }

        void OnEndOfStream(AudioStreamImpl obj)
        {
            Program.mf.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
            {
                lstActiveStreams.Remove(obj);
            });
        }
    }
}
