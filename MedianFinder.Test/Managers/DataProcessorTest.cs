using MedianFinder.Managers;
using MedianFinder.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;


namespace MedianFinder.Test.Managers
{
    public class DataProcessorTest : IDisposable
    {
        Mock<ICalculationService> moqCalcService;
        Mock<IFileService> moqFileService;

        public DataProcessorTest()
        {
            moqCalcService = new Mock<ICalculationService>();
            moqFileService = new Mock<IFileService>();
        }
        public void Dispose()
        {
            moqCalcService = null;
            moqFileService = null;
        }
        public static IEnumerable<object[]> GetInputParams()
        {
            yield return new object[] { new string[] { "0.0", "1.0", "2.0" }, 1, "C:\\123.csv", "," };

        }

        //[Theory]
        //[MemberData(nameof(GetInputParams))]
        //public void GetMedianVariance_returns_correct_variance_count(IEnumerable<string> dataValues, decimal median, string filePath, string delimiter)
        //{
        //    //Given            
        //    decimal lowerVariancePC = 20, upperVariancePC = 20;

        //    moqFileService.Setup(m => m.InitFileReader(filePath, It.IsAny<string>())).Verifiable();
        //    moqFileService.Setup(m => m.FileName).Returns(It.IsAny<string>());
        //    moqFileService.Setup(m => m.IterateFile(It.IsAny<string>())).Returns(dataValues);
        //    moqCalcService.Setup(m => m.GetMedian(It.IsAny<IEnumerable<string>>())).Returns(median);
        //    moqCalcService.Setup(m => m.IsValueInMedianRange(median, It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(true);
        //    var sut = new DataProcessor(moqCalcService.Object, moqFileService.Object);

        //    //When
        //    var actual = sut.GetMedianVariance(filePath, delimiter, lowerVariancePC, upperVariancePC);

        //    //Then
        //}
    }
}
