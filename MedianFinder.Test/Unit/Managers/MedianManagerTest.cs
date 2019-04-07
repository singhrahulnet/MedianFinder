using MedianFinder.Managers;
using MedianFinder.Services;
using Moq;
using System;
using Xunit;
using System.Collections.Generic;
using MedianFinder.Models;
using System.Linq;

namespace MedianFinder.Test.Unit.Managers
{
    public class MedianManagerTest : IDisposable
    {
        public static IEnumerable<object[]> GetFiles()
        {
            yield return new object[] { new string[] { "file1.csv", "file2.csv", "file3.csv" } };
            yield return new object[] { new string[] { "file1.csv", "file2.csv", "file3.csv", "file4.csv" } };
        }
        Mock<IFolderManager> moqFolderManager = null;
        Mock<IDataProcessor> moqDataProcessor = null;
        Mock<IOutputService> moqOutPutService = null;
        public MedianManagerTest()
        {
            moqFolderManager = new Mock<IFolderManager>();
            moqDataProcessor = new Mock<IDataProcessor>();
            moqOutPutService = new Mock<IOutputService>();
            Startup.ConfigureServices();
        }

        public void Dispose()
        {
            moqFolderManager = null;
            moqDataProcessor = null;
            moqOutPutService = null;
        }

        [Theory]
        [MemberData(nameof(GetFiles))]
        //[InlineData(new object[] { new string[] { "file1.csv", "file2.csv", "file3.csv" } })]
        public void StartProcess_processes_the_files_from_configured_file_location(IEnumerable<string> filesList)
        {
            //given
            moqFolderManager.Setup(m => m.GetAllFiles(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(filesList);
            var medianVarianceResult = new MedianVarianceResult();
            moqDataProcessor.Setup(m => m.GetMedianVariance(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(medianVarianceResult);
            moqOutPutService.Setup(m => m.OutputResult(It.IsAny<MedianVarianceResult>())).Verifiable("called");
            var sut = new MedianManager(moqFolderManager.Object, moqDataProcessor.Object, moqOutPutService.Object);

            //when
            sut.StartProcess();

            //then
            moqFolderManager.Verify(v => v.GetAllFiles(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
            moqDataProcessor.Verify(v => v.GetMedianVariance(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<decimal>()), Times.Exactly(filesList.Count()));
            moqOutPutService.Verify(v => v.OutputResult(It.IsAny<MedianVarianceResult>()), Times.Exactly(filesList.Count()));
        }
    }
}
