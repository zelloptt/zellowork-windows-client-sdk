using System;
using System.Collections.Generic;
using System.Text;

namespace SoundSample
{
    class ZelloToFile : PttLib.IAudioMessagePlayback
    {
        private String strPath;
        private int mTotalFiles = 0;
        List<CFileWriteBack> lst = new List<CFileWriteBack>();
        //SoundSample.MainForm.dlgtCreateAudioStream dlgtCreate;
        //public ZelloToFile(String _strPath, SoundSample.MainForm.dlgtCreateAudioStream _dlgtCreate)
        //{
        //    strPath = _strPath;
        //    dlgtCreate = _dlgtCreate;
        //}
        public ZelloToFile(String _strPath)
        {
            strPath = _strPath;
        }
        public delegate void dlgtFinished(PttLib.IContact cnt, string strFile);
        public dlgtFinished OnFinished = null;

        #region IAudioMessagePlayback Members

        public PttLib.IAudioStream MessageInBegin(PttLib.IMessage pMessage)
        {
            StringBuilder sb = new StringBuilder(strPath);
            sb.Append(@"\msg");
            sb.Append((++mTotalFiles).ToString("D4"));
            PttLib.IContact cnt = null;
            if (pMessage.Incoming)
            {
                PttLib.IAudioInMessage pAIM = pMessage as PttLib.IAudioInMessage;
                if (pAIM != null)
                {
                    sb.Append("(");
                    cnt = pAIM.Sender;
                    if(cnt!=null)
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
            CFileWriterBufferImpl rv = new CFileWriterBufferImpl(sb.ToString());
            //if (Program.mf.InvokeRequired)
            //{
            //    IAsyncResult ar = Program.mf.BeginInvoke(dlgtCreate, sb.ToString());
            //    ar.AsyncWaitHandle.WaitOne();
            //    rv = Program.mf.EndInvoke(ar) as CFileWriterBufferImpl;
            //}
            //else
            //    rv = dlgtCreate(sb.ToString());

            CFileWriteBack fwb = new CFileWriteBack(rv,sb.ToString(),cnt);
            fwb.m_Finished = this.OnFinished;
            lst.Add(fwb);
            return rv;
        }

        public int NativeSampleRate
        {
            get { 
                return 0;
            }
        }

        #endregion
    }
}
