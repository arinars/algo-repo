using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo.Sort
{
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
}
