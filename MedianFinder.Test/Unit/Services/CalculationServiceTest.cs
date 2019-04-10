using MedianFinder.Services;
using MedianFinder.Test.InputData;
using System.Collections.Generic;
using Xunit;

namespace MedianFinder.Test.Unit.Services
{
    public class CalculationServiceTest
    {

        [Theory]
        [ClassData(typeof(MedianTestData))]
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
        [ClassData(typeof(InRangeTestData))]
        public void IsValueInMedianRange_returns_correct_value(decimal median, decimal dataValue, decimal lowerVariancePC, decimal upperVariancePC, bool result)
        {
            //Given                        
            var sut = new CalculationService();

            //When
            var actual = sut.IsValueInMedianRange(median, dataValue, lowerVariancePC, upperVariancePC);

            //Then
            Assert.Equal(result, actual);
        }
    }
}
