using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algo.Sort;

namespace Algo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    /* 예제 입력
                    3
                    1 2 3
                    9
                    1 1 1 1 2 2 2 2 2 
                    */
                    /* 예제 출력
                    1 3 2
                    2 2 2 2 2 1 1 1 1
                    */
                    string lFirstLine = Console.ReadLine();

                    //테스트 케이스 수
                    int lCnt_TestCase = Convert.ToInt32(lFirstLine);
                    if (lCnt_TestCase <= 50)
                    {

                        if (lCnt_TestCase < 1)
                        {
                            Console.WriteLine("프로그램을 종료합니다.");
                            break;
                        }

                        string lSecondLine = Console.ReadLine();

                        int[] lParams = lSecondLine.Split(new char[] { ' ' })
                                          .Select(x => Convert.ToInt32(x))
                                          .ToArray();

                        Sorter lSorter = new Q1071Sorter(lParams);

                        //정렬 실행
                        lSorter.Execute();
                        //프린트
                        lSorter.Print();

                        // 1. 입력받은 순서대로 데이터를 처리.

                        // 1. 입력받은 값을 이진 탐색 트리에 넣는다.                
                        // 2. 연속된 숫자가 아니면서 가작 작은 수를
                        //    차례대로 결과 배열에 넣는다.
                    }
                    else
                    {
                        Console.WriteLine("첫번째 라인의 숫자는 50보다 작거나 같아야 합니다.");
                    }
                }
            }
            catch (Exception lEx)
            {
                Console.WriteLine(lEx.Message);
            }
        }
    }
}
