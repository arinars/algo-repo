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
            pivot = mValues[left]; //LEFT 값을 피벗으로 선정.

            while (left < right) //left 와 right가 만나면 루프를 빠져나온다.
            {
                //피벗보다 작은 right 값을 찾는다. (큰 경우 다음 인덱스로 이동)
                while (mValues[right] >= pivot && left < right) right--; //right

                //left와 right 가 같지 않으면
                if (left != right)
                {
                    //값 스왑
                    mValues[left] = mValues[right];
                    left++; //left 한칸 이동
                }

                //피벗보다 큰 left 값을 찾는다. (작은 경우 다음 인덱스로 이동)
                while (mValues[left] <= pivot && left < right) left++; //left

                //left와 right가 같지 않으면
                if (left != right)
                {
                    //값 스왑
                    mValues[right] = mValues[left];
                    right--; //right 한칸 이동
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
