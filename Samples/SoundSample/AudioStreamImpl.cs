using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace SoundSample
{
    interface IAudioStreamSink
    {
        void onAudioStart(int iSampleRate);
        void onAudioStop();
        void onAudioData(byte[] arr);
    }
    //Transform Audio stream callbacks to events, transform unmanaged audio data to byte array
    class AudioStreamImpl : PttLib.IAudioStream
    {
        List<IAudioStreamSink> lstSinks = new List<IAudioStreamSink>();
        public delegate void dlgtFinished(AudioStreamImpl stream);
        public event dlgtFinished OnEndOfStream;
        int totalCount;
        int sampleCount = 0;

        [DllImport("ole32.dll")]
        static extern int CreateStreamOnHGlobal(IntPtr hGlobal, bool fDeleteOnRelease, out System.Runtime.InteropServices.ComTypes.IStream ppstm);

        public AudioStreamImpl(int nCounter)
        {
            totalCount = nCounter;
        }
        public void AddSink(IAudioStreamSink sink)
        {
            lstSinks.Add(sink);
        }
        #region IAudioStream Members

        int PttLib.IAudioStream.NativeSampleRate
        {
            get { return 44100; }
        }

        void PttLib.IAudioStream.Start(int iSampleRate)
        {
            foreach(IAudioStreamSink sink in lstSinks)
                sink.onAudioStart(iSampleRate);
        }

        void PttLib.IAudioStream.Stop()
        {
            foreach (IAudioStreamSink sink in lstSinks)
                sink.onAudioStop();
            OnEndOfStream.Invoke(this);
        }

        void PttLib.IAudioStream.WriteSamples(object vData)
        {
            byte[] arr;
            try
            {
                System.Runtime.InteropServices.ComTypes.IStream comStream = vData as System.Runtime.InteropServices.ComTypes.IStream;
                if (comStream != null)
                {
                    System.Runtime.InteropServices.ComTypes.STATSTG stg;
                    comStream.Stat(out stg, 1/*STATFLAG_NONAME*/);
                    long lSize = stg.cbSize;
                    sampleCount += Convert.ToInt32(lSize / 2);
                    arr = new byte[lSize];
                    IntPtr pInt = (IntPtr)0;
                    comStream.Read(arr, Convert.ToInt32(lSize), pInt);
                    foreach (IAudioStreamSink sink in lstSinks)
                        sink.onAudioData(arr);
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(comStream);
                    comStream = null;
                }
            }
            catch (System.Exception _ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception in AudioStreamImpl.WriteSamples : " + _ex.Message);
            }

        }

        #endregion
    }
}
