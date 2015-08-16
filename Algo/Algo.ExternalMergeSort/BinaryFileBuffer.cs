using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo.ExternalMergeSort
{
    public class BinaryFileBuffer
    {
        public static int BUFFERSIZE = 2048;
        public StreamReader BufReader;
        public FileStream OriginalFile;
        private string mCache;
        private bool mEmpty;
        public bool Empty { get { return mEmpty; } }

        public BinaryFileBuffer(FileStream aFileStream)
        {
            OriginalFile = aFileStream;
            BufReader = new StreamReader(new BufferedStream(aFileStream, BUFFERSIZE));
            reload();
        }

        private void reload()
        {
            try
            {
                if ((this.mCache = BufReader.ReadLine()) == null)
                {
                    mEmpty = true;
                    mCache = null;
                }
                else
                {
                    mEmpty = false;
                }
            }
            catch (Exception)
            {
                mEmpty = true;
                mCache = null;
            }
        }

        public void Close()
        {
            BufReader.Close();
        }


        public string Peek()
        {
            if (mEmpty) return null;
            return mCache.ToString();
        }
        public string Pop()
        {
            string lAnswer = Peek();
            reload();
            return lAnswer;
        }
    }
}
