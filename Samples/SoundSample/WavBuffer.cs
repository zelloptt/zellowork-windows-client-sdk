using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace SoundSample
{
    class WavBuffer : IAudioStreamSink, IReadWav
    {
        [DllImport("ole32.dll")]
        static extern int CreateStreamOnHGlobal(IntPtr hGlobal, bool fDeleteOnRelease, out System.Runtime.InteropServices.ComTypes.IStream ppstm);

        public delegate void dlgtAudioRcvStarted(WavBuffer wb);
        public event dlgtAudioRcvStarted AudioRcvStarted;

        bool rcvStarted = false;
        bool rcvFinished = false;
        Queue<byte[]> queData = new Queue<byte[]>();
        #region IAudioStreamSink Members

        public void onAudioStart(int iSampleRate)
        {
            SampleRate = iSampleRate;
        }

        public void onAudioStop()
        {
            rcvFinished = true;
        }

        public void onAudioData(byte[] arr)
        {
            lock (queData)
            {
                queData.Enqueue(arr);
            }
            if (false == rcvStarted)
            {
                rcvStarted = true;
                AudioRcvStarted.Invoke(this);
            }
        }

        #endregion

        #region IReadWav Members
        
        public bool WaitForData
        {
            get
            {
                int count = 0;
                lock (queData)
                {
                    count = queData.Count;
                }
                if(count == 0 && false == rcvFinished)
                    return true;
                return false;
            }
        }

        public bool HasMoreData
        {
            get
            {
                bool bRetVal = true;
                if (rcvFinished)
                {
                    lock (queData)
                    {
                        if (queData.Count == 0)
                            bRetVal = false;
                    }
                }
                return bRetVal;
            }
        }

        public int SampleRate
        {
            get;
            private set;
        }

        public Array GetNextAudioChunkArray(int nMaxSampleCount)
        {
            byte[] arr = null;
            lock (queData)
            {
                try
                {
                    arr = queData.Dequeue();
                }
                catch (InvalidOperationException)
                { }
            }
            if (null == arr)
                return null;
            return (Array)arr;
        }

        public System.Runtime.InteropServices.ComTypes.IStream GetNextAudioChunk(int nMaxSampleCount)
        {
            byte[] arr = null;
            lock (queData)
            {
                try
                {
                    arr = queData.Dequeue();
                }
                catch (InvalidOperationException)
                { }
            }
            if (null == arr)
                return null;
            System.Runtime.InteropServices.ComTypes.IStream comStream;
            int nChunkBytes = arr.Length;
            if (nChunkBytes <= 0)
                return null;
            // allocate space on the native heap
            IntPtr nativePtr = System.Runtime.InteropServices.Marshal.AllocHGlobal(nChunkBytes);
            // copy byte array to native heap
            System.Runtime.InteropServices.Marshal.Copy(arr, 0, nativePtr, nChunkBytes);
            // Create a UCOMIStream from the allocated memory
            CreateStreamOnHGlobal(nativePtr, true, out comStream);
            return comStream;
        }

        #endregion
    }
}
