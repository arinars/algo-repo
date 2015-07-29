using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo.Sort
{
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

        public override void Execute()
        {
            // 결과값 정렬 프로세스 실행.
            quickSort(0, mValues.Count() - 1);
        }

        private void quickSort(int left, int right)
        {
            if (left == right) return;

            int pivot, l_hold, r_hold;
            l_hold = left;
            r_hold = right;
            pivot = mValues[left];

            while (left < right)
            {
                while (mValues[right] >= pivot && left < right) right--; //right

                if (left != right)
                {
                    //값 스왑
                    mValues[left] = mValues[right];
                    left++;
                }

                while (mValues[left] <= pivot && left < right) left++; //left

                if (left != right)
                {
                    //값 스왑
                    mValues[right] = mValues[left];
                    right--;
                }
            }

            //피벗 교체
            mValues[left] = pivot;
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
    }
}
