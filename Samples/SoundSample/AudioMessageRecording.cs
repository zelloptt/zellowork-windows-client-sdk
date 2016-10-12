using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;

namespace SoundSample
{
    class AudioMessageRecording : IDisposable
    {
        public event EventHandler AllDataWritten;
        IReadWav wavDataProvider;
        PttLib.IAudioStream m_stream;
        Thread trdFeed;
        public AudioMessageRecording(IReadWav _wavDataProvider)
        {
            wavDataProvider = _wavDataProvider;
        }
        public void SetStream(PttLib.IAudioStream _stream)
        {
            if (null != trdFeed)
                return;
            m_stream = _stream;
            m_stream.Start(Convert.ToInt32(wavDataProvider.SampleRate));
            trdFeed = new Thread(new ThreadStart(this.ThreadRun));
            trdFeed.SetApartmentState(ApartmentState.MTA);
            trdFeed.Start();
        }
        bool PushSamples(Object samples)
        {
            try
            {
                m_stream.WriteSamples(samples);
            }
            catch (OutOfMemoryException)//not enough memory to save samples
            {
                return false;
            }
            catch (ArgumentException)//data format unsupported
            {
                //System.Diagnostics.Debugger.Log(0,"","");
            }
            return true;
        }

        public void ThreadRun()
        {
            try
            {
                m_stream.Start(Convert.ToInt32(wavDataProvider.SampleRate));
                int nSamples = Convert.ToInt32(wavDataProvider.SampleRate / 50);
                do
                {
                    while (wavDataProvider.WaitForData)
                    {
                        Thread.Sleep(20);
                    }
                    System.Runtime.InteropServices.ComTypes.IStream strm = wavDataProvider.GetNextAudioChunk(nSamples);
                    if (null == strm)
                        break;
                    while (false == PushSamples(strm))
                        Thread.Sleep(20);

                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(strm);
                    strm = null;

                    //if (m_Reader.HasMoreData)
                    //{
                    //    Array arr = m_Reader.GetNextAudioChunkArray(nSamples);
                    //    while (false == PushSamples(arr))
                    //        Thread.Sleep(20);
                    //}
                } while (wavDataProvider.HasMoreData);
            }
            catch (COMException)
            {
            }
            m_stream.Stop();
            AllDataWritten.Invoke(this, null);
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            trdFeed.Join();
        }

        #endregion
    }

}
