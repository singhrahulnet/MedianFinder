using MedianFinder.Managers;
using MedianFinder.Models;
using MedianFinder.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MedianFinder.Test.Unit.Managers
{
    public class DataProcessorTest : IDisposable
    {
        Mock<ICalculationService> moqCalcService;
        Mock<IFileService> moqFileService;

        public DataProcessorTest()
        {
            moqCalcService = new Mock<ICalculationService>();
            moqFileService = new Mock<IFileService>();
            Startup.ConfigureServices();
        }
        public void Dispose()
        {
            moqCalcService = null;
            moqFileService = null;
        }
        public static IEnumerable<object[]> GetInputParams()
        {
            yield return new object[] { new string[] { "0.0", "1.0", "2.0" }, 3 };

            //Ignore invalid data in stream. 
            yield return new object[] { new string[] { "kkk", "ppp", "0.9", "1.0", "YYY--", "1.2", "2.0" }, 4 };

            //Empty stream
            yield return new object[] { new string[] { }, 0 };
        }

        [Theory]
        [MemberData(nameof(GetInputParams))]
        public void GetMedianVariance_returns_correct_variance_count(IEnumerable<string> dataValues, int elementCount)
        {
            //Given            
            decimal lowerVariancePC = 20, upperVariancePC = 20, median = 1;
            string filePath = "C:\\TOU_123csv", delimiter = ",";

            moqFileService.Setup(m => m.InitFileReader(It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            moqFileService.Setup(m => m.FileName).Returns("TOU_123.csv");
            moqFileService.Setup(m => m.IterateFile(It.IsAny<string>())).Returns(dataValues);
            moqCalcService.Setup(m => m.GetMedian(It.IsAny<IEnumerable<string>>())).Returns(median);
            moqCalcService.Setup(m => m.IsValueInMedianRange(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(true);

            var sut = new DataProcessor(moqCalcService.Object, moqFileService.Object);

            //When
            var actual = sut.GetMedianVariance(filePath, delimiter, lowerVariancePC, upperVariancePC);

            //Then
            Assert.IsType<MedianVarianceResult>(actual);
            Assert.Equal(elementCount, actual.VarianceData.Count);
        }
    }
}
