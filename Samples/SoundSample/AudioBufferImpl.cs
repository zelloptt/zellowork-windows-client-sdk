using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace SoundSample
{
    class CFileWriteBack
    {
        PttLib.IContact cnt;
        CFileWriterBufferImpl writer;
        String path;
        public SoundSample.ZelloToFile.dlgtFinished m_Finished = null;
        public CFileWriteBack(CFileWriterBufferImpl _writer,String _path, PttLib.IContact _cnt)
        {
            cnt = _cnt;
            path = _path;
            writer = _writer;
            writer.AllDataWritten +=new EventHandler(writer_AllDataWritten);
        }

        void  writer_AllDataWritten(object sender, EventArgs e)
        {
            if (m_Finished != null)
                m_Finished(cnt, path);
        }
    };

    public class CFileWriterBufferImpl : PttLib.IAudioStream, IDisposable
    {
        [DllImport("ole32.dll")]
        static extern int CreateStreamOnHGlobal(IntPtr hGlobal, bool fDeleteOnRelease, out System.Runtime.InteropServices.ComTypes.IStream ppstm);
        
        bool m_bOpened = false;
        System.IO.BinaryWriter bw;
        int m_totalsamplecount = 0;

        public event EventHandler AllDataWritten;

        private void WriteTotal()
        {
            if (m_bOpened)
            {
                m_bOpened = false;
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

        public CFileWriterBufferImpl(string strFileName)
        {
            try
            {
                if (System.IO.File.Exists(strFileName))
                    bw = new System.IO.BinaryWriter(System.IO.File.OpenWrite(strFileName));
                else
                    bw = new System.IO.BinaryWriter(System.IO.File.Create(strFileName));
                m_bOpened = true;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception in CFileWriterBufferImpl(" + strFileName + "): " + ex.Message);
                return;
            }
        }
        #region IAudioStream Members

        //void PttLib.IAudioStream.Data(ref Array ppData, int iSize)
        //{
        //    if(ppData==null || ppData.Length==0 || iSize==0)
        //        return;
        //    m_totalsamplecount += ppData.Length;
        //    byte[] arr = ConvertToBytes(ppData);
        //}

        int PttLib.IAudioStream.NativeSampleRate
        {
            get { return 44100; }
        }
        //public void RequestSamples(out object pvData, out int piSize)
        //{
        //    byte[] arr = new byte[2048];
        //    //IntPtr pInt;
        //    var stream = new System.IO.MemoryStream();
        //    // copy stream to byte array
        //    byte[] b = new byte[stream.Length];
        //    stream.Read(b, 0, b.Length);
        //    // allocate space on the native heap
        //    IntPtr nativePtr = System.Runtime.InteropServices.Marshal.AllocHGlobal(arr.Length);
        //    // copy byte array to native heap
        //    System.Runtime.InteropServices.Marshal.Copy(arr, 0, nativePtr, arr.Length);
        //    // Create a UCOMIStream from the allocated memory
        //    System.Runtime.InteropServices.ComTypes.IStream comStream;
        //    CreateStreamOnHGlobal(nativePtr, true, out comStream);
        //    pvData = comStream;
        //    piSize = arr.Length;
        //}

        public void WriteSamples(object vData)
        {
            try
            {
                System.Runtime.InteropServices.ComTypes.IStream comStream = vData as System.Runtime.InteropServices.ComTypes.IStream;
                if (comStream != null)
                {
                    System.Runtime.InteropServices.ComTypes.STATSTG stg;
                    comStream.Stat(out stg, 1/*STATFLAG_NONAME*/);
                    long lSize = stg.cbSize;
                    m_totalsamplecount += Convert.ToInt32(lSize / 2);
                    byte[] arr = new byte[lSize];
                    IntPtr pInt = (IntPtr)0;
                    comStream.Read(arr, Convert.ToInt32(lSize), pInt);
                    bw.Write(arr);

                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(comStream);
                    comStream = null;
                }
            }
            catch (System.Exception _ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception in CFileWriterBufferImpl.WriteSamples : " + _ex.Message);
            }
        }

        void PttLib.IAudioStream.Start(int iSampleRate)
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

        void PttLib.IAudioStream.Stop()
        {
            WriteTotal();
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
