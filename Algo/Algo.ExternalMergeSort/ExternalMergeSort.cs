using Sinbadsoft.Lib.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo.ExternalMergeSort
{
    public class ExternalMergeSort
    {

        /// <summary>
        /// 작은 블록으로 파일을 나눈다.
        /// 블록이 너무 작은 경우에 너무 많은 임시 파일을 생성해야 한다.
        /// 너무 큰 경우에 너무 많은 메모리를 사용한다.
        /// </summary>
        /// <param name="aFileToBeSorted"></param>
        /// <returns></returns>
        public static long EstimateBestSizeOfBlocks(FileStream aFileToBeSorted)
        {
            long lSizeOfFile = aFileToBeSorted.Length;
            
            // 1024 개가 넘는 임시파일이 생성되지 않도록 한다.
            const int MAXTEMPFILES = 1024;
            long lBlockSize = lSizeOfFile / MAXTEMPFILES;

            // 너무 많은 임시 파일도 생성하지 않는다.
            // 사용 가능한 메모리를 계산하여, Block Size를 결정한다.
            Process lProc = Process.GetCurrentProcess();
            long lFreeMemory = lProc.PrivateMemorySize64;
            if (lBlockSize < lFreeMemory / 2)
            {
                lBlockSize = lFreeMemory / 2;
            }
            else
            {
                if (lBlockSize >= lFreeMemory)
                {
                    Console.Error.WriteLine("We expect to run out of memory. ");
                }
            }

            return 1000000; // lBlockSize;
        }
        
        /// <summary>
        /// 원본 파일을 불러와서 정렬한뒤 임시파일로 저장한다.
        /// 저장된 임시파일의 스트림을 리턴한다. (나중에 병합 필요)
        /// </summary>
        /// <param name="aFile"></param>
        /// <param name="aComparer"></param>
        /// <returns></returns>
        public static List<FileStream> SortInBatch(FileStream aFileStream, IComparer<long> aComparer) //throw IOException
        {
            List<FileStream> lFiles = new List<FileStream>();

            
            BufferedStream lBufStream = new BufferedStream(aFileStream);
            StreamReader lBufReader = new StreamReader(lBufStream);

            long lBlockSize = EstimateBestSizeOfBlocks(aFileStream);
            try
            {
                IList<String> lTmpList = new List<string>();
                string lLine = "";
                try
                {
                    while (lLine != null)
                    {
                        long lCurrentBlockSize = 0;// in bytes
                        while ((lCurrentBlockSize < lBlockSize)
                                && ((lLine = lBufReader.ReadLine()) != null))
                        { // as long as you have 2MB
                            lTmpList.Add(lLine);
                            lCurrentBlockSize += lLine.Length; // 2 + 40; // java uses 16 bits per character + 40 bytes of overhead (estimated)
                        }
                        lFiles.Add(SortAndSave(lTmpList, aComparer));
                        lTmpList.Clear();
                    }
                }
                catch (Exception)
                {
                    if (lTmpList.Count > 0)
                    {
                        lFiles.Add(SortAndSave(lTmpList, aComparer));
                        lTmpList.Clear();
                    }
                }
            }
            finally
            {
                lBufReader.Close();
            }
            return lFiles;

        }

        public static FileStream SortAndSave(IList<string> aTmpList, IComparer<long> aComparer)
        {
            IComparer<string> lComp = Comparer<string>.Create(new Comparison<string>((string x, string y) =>
            {
                return aComparer.Compare(Convert.ToInt64(x), Convert.ToInt64(y));
            }));

            List<string> lSortedTmpList = new List<string>(aTmpList);
            lSortedTmpList.Sort(lComp);
            
            FileStream lNewTmpFileStream = File.Create(Path.GetRandomFileName(), 4096, FileOptions.DeleteOnClose);

            BufferedStream lBufStream = new BufferedStream(lNewTmpFileStream);
            StreamWriter lBufWriter = new StreamWriter(lBufStream);


            try {
                foreach (string lLine in lSortedTmpList)
                {
                    lBufWriter.WriteLine(lLine);
                }
            } finally {
                lBufWriter.Flush();
                lNewTmpFileStream.Seek(0, SeekOrigin.Begin);
                //lBufWriter.Close();
            }
            return lNewTmpFileStream;

            
        }
        

        // This merges a bunch of temporary flat files 
        // @param files
        // @param output file
        // @return The number of lines sorted. (P. Beaudoin)

        public static int MergeSortedFiles(List<FileStream> aFiles, FileStream aOutputFile, IComparer<long> aComparer)
        {
            IComparer<BinaryFileBuffer> lComp = Comparer<BinaryFileBuffer>.Create(new Comparison<BinaryFileBuffer>((BinaryFileBuffer x, BinaryFileBuffer y) =>
            {
                long x_peek = Convert.ToInt64(x.Peek());
                long y_peek = Convert.ToInt64(y.Peek());
                int lResult = aComparer.Compare(x_peek, y_peek);
                return x_peek.CompareTo(y_peek);// Result;
            }));
            
            PriorityQueue<BinaryFileBuffer> lPq = new PriorityQueue<BinaryFileBuffer>(lComp, 11);

            foreach (FileStream lFile in aFiles) {
                BinaryFileBuffer lBfb = new BinaryFileBuffer(lFile);
                lPq.Enqueue(lBfb); //add
            }

            StreamWriter lBufWriter = new StreamWriter(new BufferedStream(aOutputFile));
            int lRowCounter = 0;

            try {
                while (lPq.Count > 0) {
                    BinaryFileBuffer lBfb = lPq.Dequeue();
                    string lLine = lBfb.Pop();
                    lBufWriter.WriteLine(lLine);
                    ++lRowCounter;
                    if (lBfb.Empty) {
                        lBfb.BufReader.Close();
                        lBfb.OriginalFile.Close();// we don't need you anymore
                    } else {
                        lPq.Enqueue(lBfb); // add it back
                    }
                }
            } finally {
                lBufWriter.Close();
                //foreach (BinaryFileBuffer lBfb in lPq)
                //{
                //    lBfb.Close();
                //}
            }
            return lRowCounter;
        }
 
    }
}
