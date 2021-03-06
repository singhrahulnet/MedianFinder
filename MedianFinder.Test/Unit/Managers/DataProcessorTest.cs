﻿using MedianFinder.Managers;
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
        Mock<IFileReaderService> moqFileReaderService;

        public DataProcessorTest()
        {
            moqCalcService = new Mock<ICalculationService>();
            moqFileReaderService = new Mock<IFileReaderService>();
        }
        public void Dispose()
        {
            moqCalcService = null;
            moqFileReaderService = null;
        }
        public static IEnumerable<object[]> GetInputParams()
        {
            //THE TEST DATA STRUCTURE
            //IEnumerable<string> dataValues, int elementCount

            //Correct count
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
            var settings = new SourceFolderSettings
            {
                LowerVariancePC = 20,
                UpperVariancePC = 20,
                FileFormat = new FileFormat { Delimiter = "," },
                FileTypes = new Dictionary<string, string>() { { "TOU", "Energy" }, { "LP", "Data Value" } }
            };

            decimal median = 1;
            string filePath = "C:\\TOU_123.csv";

            moqFileReaderService.Setup(m => m.InitFileReader(It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            moqFileReaderService.Setup(m => m.FileName).Returns("TOU_123.csv");
            moqFileReaderService.Setup(m => m.IterateFileOnColumn(It.IsAny<string>())).Returns(dataValues);
            moqCalcService.Setup(m => m.GetMedian(It.IsAny<IEnumerable<string>>())).Returns(median);
            moqCalcService.Setup(m => m.IsValueInMedianRange(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(true);

            var sut = new DataProcessor(moqCalcService.Object, moqFileReaderService.Object);

            //When
            var actual = sut.GetMedianVariance(filePath, settings);

            //Then
            Assert.IsType<MedianVarianceResult>(actual);
            Assert.Equal(elementCount, actual.VarianceData.Count);
        }
    }
}
