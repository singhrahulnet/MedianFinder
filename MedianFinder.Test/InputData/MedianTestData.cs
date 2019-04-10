using System.Collections;
using System.Collections.Generic;

namespace MedianFinder.Test.InputData
{
    public class MedianTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //THE TEST DATA STRUCTURE
            //IEnumerable<string> dataValues, decimal median

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
            //just two elements
            yield return new object[] { new string[] { "0.1", "0.2" }, 0.15 };            
            //negetive value and odd count
            yield return new object[] { new string[] { "-2.0", "0.0", "-3.0", "-1.0", "-9" }, -2.0 };
            //negetive value and even count
            yield return new object[] { new string[] { "-2.0", "0.0", "-3.0", "-1.0", "-9", "2" }, -1.5 };
            //precision test
            yield return new object[] { new string[] { "0.1111", "0.2222" }, 0.16665 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
