using MedianFinder.Managers;
using MedianFinder.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MedianFinder.Test.Unit.Managers
{
    public class FolderManagerTest : IDisposable
    {
        Mock<IFolderParserService> moqFolderParserService = null;
        public FolderManagerTest()
        {
            moqFolderParserService = new Mock<IFolderParserService>();
            Startup.ConfigureServices();
        }

        public void Dispose()
        {
            moqFolderParserService = null;
        }
        public static IEnumerable<object[]> GetFiles()
        {
            //All valid files
            yield return new object[] { new string[] { "LP_1.csv", "LP2.csv", "TOU1.csv" }, 3 };
            //All invalid files
            yield return new object[] { new string[] { "1.csv", "2.csv", "Xyz1.csv" }, 0 };
            //Only LP
            yield return new object[] { new string[] { "LP.csv", "file2.csv", "file3.csv", "file4.csv" }, 1 };
            //Only TOU
            yield return new object[] { new string[] { "file1.csv", "file2.csv", "file3.csv", "TOU_.csv" }, 1 };
            //LP, TOU and invalid files
            yield return new object[] { new string[] { "file1.csv", "LP_1.csv", "file3.csv", "TOU1_.csv" }, 2 };
            //No files found
            yield return new object[] { null, 0 };
        }

        [Theory]
        [MemberData(nameof(GetFiles))]        
        public void GetAllFiles_returns_all_valid_files_based_on_configured_filetypes(IEnumerable<string> filesList, int countOfFiles)
        {
            //given
            moqFolderParserService.Setup(m => m.GetFileNamesFromFolder(It.IsAny<string>(), It.Is<string>(p => p == "*.csv"))).Returns(filesList);
            var sut = new FolderManager(moqFolderParserService.Object);

            //when
            var actual = sut.GetAllFiles(Startup.Settings.Path, Startup.Settings.FileFormat.Ext, Startup.Settings.FileTypes);

            //then
            int countReturned = actual == null ? 0 : actual.Count();

            Assert.Equal(countOfFiles, countReturned);
            moqFolderParserService.Verify(v => v.GetFileNamesFromFolder(It.IsAny<string>(), It.Is<string>(p => p == "*.csv")), Times.Once);
        }
    }
}
