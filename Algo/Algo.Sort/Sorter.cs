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
        protected int[] mValues = null;
        public int[] Values
        {
            get { return mValues; }
            set { mValues = value; }
        }

        public Sorter(int aArraySize)
        {
            mValues = new int[aArraySize];
        }

        public Sorter(int[] aArray)
        {
            mValues = aArray;
        }

        /// <summary>
        /// 값 삽입
        /// </summary>
        /// <param name="aIdx"></param>
        /// <param name="aValue"></param>
        public void Insert(int aIdx, int aValue)
        {
            mValues[aIdx] = aValue;
        }

        /// <summary>
        /// 정렬 실행 메소드
        /// </summary>
        abstract public void Execute();
        /// <summary>
        /// 결과 프린트
        /// </summary>
        public void Print()
        {
            Console.WriteLine(string.Join(" ", mValues));
        }
    }
}
