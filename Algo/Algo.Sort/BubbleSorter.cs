using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo.Sort
{
    /// <summary>
    /// 버블 정렬 클래스
    /// </summary>
    public class BubbleSorter : Sorter
    {
        public BubbleSorter(int aArraySize) : base(aArraySize) { }
        public BubbleSorter(int[] aArray) : base(aArray) { }

        public override void Execute()
        {
            bubbleSort(false);
        }

        private void bubbleSort(bool aNoChange)
        {
            if (aNoChange)
            {
                return;
            }
            int temp;
            aNoChange = true;
            for (int i = 0; i < (mValues.Count() - 1); i++)
            {
                if (mValues[i] > mValues[i + 1])
                {
                    //Swap
                    temp = mValues[i];
                    mValues[i] = mValues[i + 1];
                    mValues[i + 1] = temp;
                    aNoChange = false;
                }
            }

            bubbleSort(aNoChange);
        }
    }
}
