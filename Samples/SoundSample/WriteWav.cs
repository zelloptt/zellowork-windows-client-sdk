using System;
using System.Collections.Generic;
using System.Text;

namespace SoundSample
{
    class WriteWav : IAudioStreamSink, IDisposable
    {
        public bool Valid { get; private set; }
        System.IO.BinaryWriter bw;
        int m_totalsamplecount = 0;

        public event EventHandler AllDataWritten;

        private void WriteTotal()
        {
            if (Valid)
            {
                Valid = false;
                bw.Seek(4, System.IO.SeekOrigin.Begin);
                bw.Write(BitConverter.GetBytes(m_totalsamplecount * 2 + 36), 0, 4);
                bw.Seek(32, System.IO.SeekOrigin.Current);
                bw.Write(BitConverter.GetBytes(m_totalsamplecount * 2), 0, 4);
                bw.Flush();
                bw.Close();
                if (AllDataWritten != null)
                {
                    System.Diagnostics.Debug.WriteLine("Audio msg saved to file in thread " + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());

                    AllDataWritten.Invoke(this, null);
                }
            }
        }


        static public byte[] ConvertToBytes(Array myArray)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            System.IO.StreamWriter sw = new System.IO.StreamWriter(ms);
            foreach (object obj in myArray)
            {
                sw.Write(Convert.ToInt16(obj));
            }
            sw.Flush();
            return ms.GetBuffer();
        }

        public WriteWav(string strFileName)
        {
            Valid = false;
            try
            {
                if (System.IO.File.Exists(strFileName))
                    bw = new System.IO.BinaryWriter(System.IO.File.OpenWrite(strFileName));
                else
                    bw = new System.IO.BinaryWriter(System.IO.File.Create(strFileName));
                Valid = true;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception in WriteWav(" + strFileName + "): " + ex.Message);
                return;
            }
        }

        #region IAudioStreamSink Members

        public void onAudioStart(int iSampleRate)
        {
            if (Valid)
            {
                bw.Write(Encoding.ASCII.GetBytes("RIFF"), 0, 4);
                // Chunk size.
                byte[] arrSize = BitConverter.GetBytes(36);
                bw.Write(arrSize, 0, 4);
                // Format.
                bw.Write(Encoding.ASCII.GetBytes("WAVE"), 0, 4);
                // Sub-chunk 1.
                // Sub-chunk 1 ID.
                bw.Write(Encoding.ASCII.GetBytes("fmt "), 0, 4);
                // Sub-chunk 1 size.
                bw.Write(BitConverter.GetBytes(16), 0, 4);
                // Audio format (floating point (3) or PCM (1)). Any other format indicates compression.
                bw.Write(BitConverter.GetBytes((ushort)1), 0, 2);
                // Channels.
                bw.Write(BitConverter.GetBytes((ushort)1), 0, 2);
                // Sample rate.
                bw.Write(BitConverter.GetBytes(iSampleRate), 0, 4);
                // Bytes rate.
                bw.Write(BitConverter.GetBytes(iSampleRate * 2), 0, 4);
                // Block align.
                bw.Write(BitConverter.GetBytes((ushort)2), 0, 2);
                // Bits per sample.
                bw.Write(BitConverter.GetBytes(16), 0, 2);

                // Start data subchunk 
                bw.Write(Encoding.ASCII.GetBytes("data"), 0, 4);
                // Unknown size, will overwrite this later
                bw.Write(BitConverter.GetBytes(0), 0, 4);
            }
        }

        public void onAudioStop()
        {
            WriteTotal();
        }

        public void onAudioData(byte[] arr)
        {
            if (Valid)
            {
                bw.Write(arr);
                m_totalsamplecount += arr.Length / 2;
            }
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            WriteTotal();
        }

        #endregion
    }
}
