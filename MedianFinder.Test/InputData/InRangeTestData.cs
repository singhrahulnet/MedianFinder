using System.Collections;
using System.Collections.Generic;

namespace MedianFinder.Test.InputData
{
    public class InRangeTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //THE TEST DATA STRUCTURE
            //decimal median, decimal value, decimal lowerVariancePC, decimal upperVariancePC, bool result

            //not in range - lower bound
            yield return new object[] { 1, 0.5, 20, 20, false };
            //not in range - upper bound
            yield return new object[] { 1, 1.5, 20, 20, false };

            //in range - upper bound
            yield return new object[] { 1, 1.5, 50, 50, true };
            //in range - lower bound
            yield return new object[] { 1, 1.5, 50, 50, true };

            //not in range negative values - upper bound
            yield return new object[] { -2, -1.7, 10, 10, false };
            //not in range negative values - lower bound
            yield return new object[] { -2, -2.3, 10, 10, false };

            //in range negative values - upper bound
            yield return new object[] { -2, -1.8, 10, 10, true };
            //in range negative values - lower bound
            yield return new object[] { -2, -2.2, 10, 10, true };

            //Ignore the median value itself (-ve)
            yield return new object[] { -2.2, -2.2, 10, 10, false };
            //Ignore the median value itself (+ve)
            yield return new object[] { 2, 2, 10, 10, false };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
