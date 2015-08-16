using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo.ExternalMergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            // 참고 URL : http://www.ashishsharma.me/2011/08/external-merge-sort.html
            //183292 없는 수
            #region >> 샘플 파일 생성
            /* 샘플 파일 생성
            using (StreamWriter w = File.AppendText("data.txt"))
            {
                for (long i = 1; i < 400001; i++)
                {
                    w.WriteLine(400001 - i);
                }
            }*/
            #endregion

            /* data.txt 파일을 외부 병합 정렬로 정렬한 뒤에 없는 수를 찾아 낸다.
             */

            /* 외부 병합 정렬
             * external merge sort는 대상 데이터가 테이프나 디스크에 저장되어있고
             * 데이터가 너무 커서 메모리에 담을 수 없는 경우에 실용적인 방법이다.
             * 예를 들어, 900MB의 데이터를 100MB의 RAM을 사용하여 정렬을 해야 한다고
             * 해보자.
             * 1. 100MB 데이터를 주메모리에 읽어들이고, quicksort와 같이 일반적인 
             *    알고리즘을 사용하여 정렬한다.
             * 2. 정렬된 데이터를 디스크에 쓴다.
             * 3. 1,2 번 과정을 9번 반복한다. 그러면 100MB짜리 파일이 9개 생긴다.
             * 4. 9개의 파일에서 각각 처음부터 10MB 씩을 메모리(입력버퍼)에 로딩한다.
             *    10MB의 출력을 위한 버퍼도 만들어둔다.
             * 5. 9way merge를 수행하고 결과를 출력버퍼에 쓴다. 출력버퍼가 차면 파일에
             *    쓰고, 출력 버퍼를 비운다. 9개의 입력 버퍼가 비워지면, 다음 10MB를 읽는다.
            */

            //if (args.Count() < 2)
            //{
            //    Console.WriteLine("please provide input and output file names");
            //    return;
            //}
            string inputfile = "data.txt";// args[0];
            string outputfile = "dataresult.txt";// args[1];
            IComparer<long> lComp = Comparer<long>.Create(new Comparison<long>((long x, long y) =>
            {
                if (x < y)
                {
                    return -1;
                }
                else if (x > y)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
                //return x.CompareTo(y);
            }));

            FileStream lInputStream = File.Open(inputfile, FileMode.Open);
            FileStream lOutputStream = File.Open(outputfile, FileMode.OpenOrCreate);
            List<FileStream> lList = ExternalMergeSort.SortInBatch(lInputStream, lComp);
            ExternalMergeSort.MergeSortedFiles(lList, lOutputStream, lComp);
        }
    }
}
