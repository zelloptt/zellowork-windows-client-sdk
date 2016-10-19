using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace SoundSample
{
    class CWAVReader
    {
        [DllImport("ole32.dll")]
        static extern int CreateStreamOnHGlobal(IntPtr hGlobal, bool fDeleteOnRelease, out System.Runtime.InteropServices.ComTypes.IStream ppstm);

        int m_totalsamplecount = 0;
        byte[] m_samples;
        int m_StartSampleIndex = 0;
        public uint sampleRate;

        public CWAVReader(string strFileName)
        {
            try
            {
                if (System.IO.File.Exists(strFileName)) {
                    using (System.IO.BinaryReader br = new System.IO.BinaryReader(System.IO.File.OpenRead(strFileName)))
                    {
                        byte[] riffID = br.ReadBytes(4);
                        uint size = br.ReadUInt32();
                        byte[] wavID = br.ReadBytes(4);
                        byte[] fmtID = br.ReadBytes(4);//"fmt "
                        uint fmtSize = br.ReadUInt32();
                        ushort format = br.ReadUInt16();
                        ushort channels = br.ReadUInt16();
                        sampleRate = br.ReadUInt32();
                        uint bytePerSec = br.ReadUInt32();
                        ushort blockSize = br.ReadUInt16();
                        ushort bit = br.ReadUInt16();
                        if (fmtSize == 18)
                        {
                            int fmtExtraSize = br.ReadInt16();
                            br.ReadBytes(fmtExtraSize);
                        }

                        do
                        {
                            byte[] dataID = br.ReadBytes(4);
                            int dataSize = br.ReadInt32();
                            if ((dataID[0] == 100 && dataID[1] == 97 && dataID[2] == 116 && dataID[3] == 97))
                            {
                                m_totalsamplecount = Convert.ToInt32(dataSize / 2);
                                m_samples = br.ReadBytes(dataSize);
                                break;
                            }
                            byte[] tmp = br.ReadBytes(dataSize);
                        } while (true);
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception in CWAVReader(" + strFileName + ") :" + ex.Message);
                return;
            }
        }

        public bool HasMoreData
        {
            get
            {
                return m_StartSampleIndex < m_totalsamplecount;
            }
        }

        public Array GetNextAudioChunkArray(int nMaxSampleCount)
        {
            int nSampleCount = System.Math.Min( nMaxSampleCount, m_totalsamplecount - m_StartSampleIndex);
            if(0==nSampleCount)
                return null;

            short[] arr = new short[nSampleCount];
            int idx = 0;
            do
            {
                arr[idx++] = BitConverter.ToInt16(m_samples, m_StartSampleIndex++ * 2);
            } while (idx < nSampleCount);
            return (Array)arr;
        }

        public System.Runtime.InteropServices.ComTypes.IStream GetNextAudioChunk(int nMaxSampleCount)
        {
            System.Runtime.InteropServices.ComTypes.IStream comStream;
            int nChunkBytes = nMaxSampleCount * 2;
            if (m_totalsamplecount - m_StartSampleIndex < nMaxSampleCount)
                nChunkBytes = (m_totalsamplecount - m_StartSampleIndex) * 2;
            if (nChunkBytes <= 0)
                return null;
            // allocate space on the native heap
            IntPtr nativePtr = System.Runtime.InteropServices.Marshal.AllocHGlobal(nChunkBytes);
            // copy byte array to native heap
            System.Runtime.InteropServices.Marshal.Copy(m_samples, m_StartSampleIndex * 2, nativePtr, nChunkBytes);
            m_StartSampleIndex += nChunkBytes / 2;

            // Create a UCOMIStream from the allocated memory
            CreateStreamOnHGlobal(nativePtr, true, out comStream);
            return comStream;
        }
    }

    class ZelloFromFile : IDisposable
    {
        public event EventHandler AllDataWritten;
        IReadWav wavDataProvider;
        PttLib.IAudioStream m_stream;
        Thread trdFeed;
        public ZelloFromFile(IReadWav _wavDataProvider)
        {
            wavDataProvider = _wavDataProvider;
        }
        public void SetStream(PttLib.IAudioStream _stream) {
            if (null != trdFeed)
                return;
            m_stream = _stream;
            m_stream.Start(Convert.ToInt32(wavDataProvider.SampleRate));
            trdFeed = new Thread(new ThreadStart(this.ThreadRun));
            trdFeed.SetApartmentState(ApartmentState.MTA);
            trdFeed.Start();
        }
        bool PushSamples(Object samples )
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
                    System.Runtime.InteropServices.ComTypes.IStream strm = wavDataProvider.GetNextAudioChunk(nSamples);
                    if (null == strm)
                        break;
                    while (false == PushSamples(strm))
                        Thread.Sleep(20);

                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(strm);
                    strm = null;

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
