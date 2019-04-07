using MedianFinder.Services;
using System.Collections.Generic;
using Xunit;


namespace MedianFinder.Test.Services
{
    public class CalculationServiceTest
    {
        public static IEnumerable<object[]> GetParamsForMedianValue()
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
            ////null string
            //yield return new object[] { null, 0 };
        }
        public static IEnumerable<object[]> GetParamsForMedianInRange()
        {
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
        }

        [Theory]
        [MemberData(nameof(GetParamsForMedianValue))]
        public void GetMedian_returns_correct_median(IEnumerable<string> dataValues, decimal median)
        {
            //Given                        
            var sut = new CalculationService();

            //When
            var actual = sut.GetMedian(dataValues);

            //Then
            Assert.Equal(median, actual);
        }

        [Theory]
        [MemberData(nameof(GetParamsForMedianInRange))]
        public void IsValueInMedianRange_returns_correct_value(decimal median, decimal value, decimal lowerVariancePC, decimal upperVariancePC, bool result)
        {
            //Given                        
            var sut = new CalculationService();

            //When
            var actual = sut.IsValueInMedianRange(median, value, lowerVariancePC, upperVariancePC);

            //Then
            Assert.Equal(result, actual);
        }
    }
}
