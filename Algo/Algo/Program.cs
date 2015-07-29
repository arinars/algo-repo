using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                        Sorter lSorter = new QuickSorter(lCnt_TestCase);

                        //입력값 세팅
                        lSorter.InputValues = lParams;
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

        /// <summary>
        /// 여러 종류의 정렬을 구현하기 위한 추상 클래스
        /// </summary>
        public abstract class Sorter
        {
            protected int[] mInputValues = null;
            public int[] InputValues
            {
                get { return mInputValues; }
                set { mInputValues = value; }
            }

            protected int[] mResultValues = null;
            public int[] ResultValues
            {
                get { return mResultValues; }
            }

            public Sorter(int aArraySize)
            {
                mInputValues = new int[aArraySize];
                mResultValues = new int[aArraySize];
            }

            public Sorter(int[] aArray)
            {
                mInputValues = aArray;
                mResultValues = aArray.ToArray();
            }

            /// <summary>
            /// 값 삽입
            /// </summary>
            /// <param name="aIdx"></param>
            /// <param name="aValue"></param>
            abstract public void Insert(int aIdx, int aValue);

            /// <summary>
            /// 정렬 실행 메소드
            /// </summary>
            abstract public void Execute();
            /// <summary>
            /// 결과 프린트
            /// </summary>
            abstract public void Print();
        }

        // 이진검색트리 클래스
        public class QuickSorter : Sorter
        {
            public QuickSorter(int aArraySize)
                : base(aArraySize)
            {
            }

            public QuickSorter(int[] aArray)
                : base(aArray)
            {
            }

            public override void Insert(int aIdx, int aValue)
            {
                mInputValues[aIdx] = aValue;
            }

            public override void Execute()
            {
                mResultValues = mInputValues.ToArray(); // 인풋값을 결과값으로 이동.

                if (mInputValues != null && mInputValues.Count() > 0)
                {
                    // 결과값 정렬 프로세스 실행.
                    quickSort(0, mInputValues.Count() - 1);
                }
            }

            private void quickSort(int left, int right)
            {
                if (left == right) return;

                int pivot, l_hold, r_hold;
                l_hold = left;
                r_hold = right;
                pivot = mResultValues[left];

                while (left < right)
                {
                    while (mResultValues[right] >= pivot && left < right) right--; //right

                    if (left != right)
                    {
                        //값 스왑
                        mResultValues[left] = mResultValues[right];
                        left++;
                    }

                    while (mResultValues[left] <= pivot && left < right) left++; //left

                    if (left != right)
                    {
                        //값 스왑
                        mResultValues[right] = mResultValues[left];
                        right--;
                    }
                }

                //피벗 교체
                mResultValues[left] = pivot;
                pivot = left; //다음 피벗 값은 LEFT가 된다.
                left = l_hold;
                right = r_hold;

                //Swap ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
                if (left < pivot)
                {
                    quickSort(left, pivot - 1); // 가운데가 된 pivot을 중심으로 왼쪽 영역 재귀
                }

                if (right > pivot)
                {
                    quickSort(pivot + 1, right);// 가운데가 된 pivot을 중심으로 오른쪽 영역 재귀
                }
            }


            //public class MergeSorter : Sorter
            //{

            //}




            public override void Print()
            {
                Console.WriteLine(string.Join(" ", mResultValues));
            }
        }
    }
}
