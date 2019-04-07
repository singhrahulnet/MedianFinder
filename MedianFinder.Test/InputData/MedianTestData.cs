using System.Collections;
using System.Collections.Generic;

namespace MedianFinder.Test.InputData
{
    public class MedianTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //sorted and odd count
            yield return new object[] { new string[] { "0.0", "1.0", "2.0" }, 1 };
            //sorted and even count
            yield return new object[] { new string[] { "0.0", "1.0", "2.0", "3.0" }, 1.5 };
            //un-sorted and odd count
            yield return new object[] { new string[] { "1.0", "0.0", "2.0" }, 1 };
            //un-sorted and even count
            yield return new object[] { new string[] { "2.0", "0.0", "3.0", "1.0" }, 1.5 };
            //single value
            yield return new object[] { new string[] { "0.1" }, 0.1 };
            //negetive value and odd count
            yield return new object[] { new string[] { "-2.0", "0.0", "-3.0", "-1.0", "-9" }, -2.0 };
            //negetive value and even count
            yield return new object[] { new string[] { "-2.0", "0.0", "-3.0", "-1.0", "-9", "2" }, -1.5 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
