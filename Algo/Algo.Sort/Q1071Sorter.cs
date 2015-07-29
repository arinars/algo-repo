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
            bubbleSort(false);
        }

        private void bubbleSort(bool aNoChange)
        {
            // 수정된 것이 없으면서, 1번 배열의 값이 가장 작은 경우 종료.
            if (aNoChange)
            {
                return;
            }
            //int temp;
            aNoChange = true;

            Dictionary<int, int> dic = new Dictionary<int, int>();
            for (int i = 0; i < (mValues.Count() - 1); i++)
            {
                for (int j = i + 1; j < (mValues.Count() - 1); j++)
                {
                    if (((mValues[i] + 1) == mValues[j]))
                    {
                        // A[i] + 1 != A[i+1] 인 값을 사전에 넣는다.
                        dic.Add(j, mValues[j]);
                        aNoChange = false;
                    }
                }

                if(dic.Count() > 0)
                {
                    // 현재 위치의 수와 사전에 있는 가장 작은 수과 Swap
                    var temp = dic.OrderBy(x => x.Value).First();
                    mValues[temp.Key] = mValues[i];
                    mValues[i] = temp.Value;
                    dic.Clear();
                }
            }



            bubbleSort(aNoChange);
        }

    }
}
