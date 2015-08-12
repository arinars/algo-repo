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
        }
    }
}
