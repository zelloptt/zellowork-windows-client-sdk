using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace SoundSample
{
    interface IReadWav
    {
        bool HasMoreData { get; }
        bool WaitForData { get; }
        int SampleRate { get; }
        Array GetNextAudioChunkArray(int nMaxSampleCount);
        System.Runtime.InteropServices.ComTypes.IStream GetNextAudioChunk(int nMaxSampleCount);
    }

    class CReadWavFile : IReadWav
    {
        [DllImport("ole32.dll")]
        static extern int CreateStreamOnHGlobal(IntPtr hGlobal, bool fDeleteOnRelease, out System.Runtime.InteropServices.ComTypes.IStream ppstm);

        int m_totalsamplecount = 0;
        byte[] m_samples;
        int m_StartSampleIndex = 0;

        public ushort ChannelCount
        {
            get;
            private set;
        }

        public ushort BitsPerSecond
        {
            get;
            private set;
        }

        public int SampleRate
        {
            get;
            private set;
        }

        public CReadWavFile(string strFileName)
        {
            try
            {
                if (System.IO.File.Exists(strFileName))
                {
                    using (System.IO.BinaryReader br = new System.IO.BinaryReader(System.IO.File.OpenRead(strFileName)))
                    {
                        byte[] riffID = br.ReadBytes(4);
                        uint size = br.ReadUInt32();
                        byte[] wavID = br.ReadBytes(4);
                        byte[] fmtID = br.ReadBytes(4);//"fmt "
                        uint fmtSize = br.ReadUInt32();
                        ushort format = br.ReadUInt16();
                        ChannelCount = br.ReadUInt16();
                        SampleRate = br.ReadInt32();
                        uint bytePerSec = br.ReadUInt32();
                        ushort blockSize = br.ReadUInt16();
                        BitsPerSecond = br.ReadUInt16();
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

        public bool WaitForData
        {
            get
            {
                return false;
            }
        }

        public Array GetNextAudioChunkArray(int nMaxSampleCount)
        {
            int nSampleCount = System.Math.Min(nMaxSampleCount, m_totalsamplecount - m_StartSampleIndex);
            if (0 == nSampleCount)
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

}
