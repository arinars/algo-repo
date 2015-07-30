using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo.Sort
{
    /// <summary>
    /// https://www.acmicpc.net/problem/1071
    /// 백준 온라인 저지 사이트의 1071번 문제.
    /// </summary>
    public class Q1071Sorter :Sorter
    {
        public Q1071Sorter(int aArraySize) : base(aArraySize) { }
        public Q1071Sorter(int[] aArray) : base(aArray) { }

        public override void Execute()
        {
            //가장 작은 수를 구해서 정렬 시작.
            int min = mValues.Min();
            for (int i = 0; i < mValues.Count(); i++)
            {
                if (mValues[i] == min)
                {
                    //스왑 대상자.
                    mValues[i] = mValues[0];
                    mValues[0] = min;
                    break;
                }
            }
            
            sort(0);
        }

        private void sort(int aIdx)
        {
            if (aIdx >= mValues.Count() - 1)
            {
                return;
            }

            int temp;
            Dictionary<int, int> lDic = new Dictionary<int, int>();
            
            for (int i = aIdx; i < (mValues.Count() - 1); i++)
            {
                // A[i] + 1 != A[i+1] 인 값을 사전에 넣는다.
                // 다음에 올수 있는 값을 구해서 사전에 넣는다.
                if ((mValues[aIdx] + 1) != mValues[i + 1])
                {
                    //스왑 대상자.
                    lDic.Add(i + 1, mValues[i + 1]);
                }
            }

            // 다음에 올수 있는 값들이 있는 경우
            if (lDic.Count > 0)
            {
                //다음에 올수 있는 값 중에서 가장 작은 값을 구한다.
                KeyValuePair<int, int> lMinValue = lDic.OrderBy(x => x.Value).First();
                //다음에 오는 값을 조건에 맞는 값중 가장 작은 값으로 교체한다.
                temp = mValues[aIdx + 1];
                mValues[aIdx + 1] = mValues[lMinValue.Key];
                mValues[lMinValue.Key] = temp;

                //포인터값을 증가 시켜서 재귀.
                sort(aIdx + 1);
            }
            else
            {
                // 다음에 올수 있는 값이 없는 경우
                //이전값과 비교하면서 조건(A[i] + 1 != A[i+1]) 이 맞을때까지 이동 시킨다.
                for (int i = aIdx; i >= 0; i--)
                {
                    //이전 값 과 스왑후
                    temp = mValues[i + 1];
                    mValues[i + 1] = mValues[i];
                    mValues[i] = temp;

                    //값 비교
                    if (i == 0 || mValues[i - 1] + 1 != mValues[i])
                    {
                        sort(i);
                        break; //소트 재개
                    }
                }
            }
            
        }
    }
}
