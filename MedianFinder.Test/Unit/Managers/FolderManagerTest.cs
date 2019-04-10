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
        }

        public void Dispose()
        {
            moqFolderParserService = null;
        }
        public static IEnumerable<object[]> GetFiles()
        {
            //THE TEST DATA STRUCTURE
            //IEnumerable<string> filesList, int countOfFiles

            //All valid files
            yield return new object[] { new string[] { "C:\\LP_1.csv", "C:\\LP_2.csv", "C:\\TOU_1.csv" }, 3 };
            //All invalid files
            yield return new object[] { new string[] { "C:\\1.csv", "C:\\2.csv", "C:\\Xyz1.csv" }, 0 };
            //Only LP
            yield return new object[] { new string[] { "C:\\LP_e.csv", "C:\\file2.csv", "C:\\file3.csv", "C:\\file4.csv" }, 1 };
            //Only TOU
            yield return new object[] { new string[] { "C:\\file1.csv", "C:\\file2.csv", "C:\\file3.csv", "C:\\TOU_ff.csv" }, 1 };
            //LP, TOU and invalid files
            yield return new object[] { new string[] { "C:\\file1.csv", "C:\\LP_1.csv", "C:\\file3.csv", "C:\\TOU_1_.csv" }, 2 };
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
            var fileTypes = new Dictionary<string, string>() {
                {"TOU","Energy" },
                {"LP","Data Value" }
            };
            var sourceFolderPath = "D:\\Sample files";
            var fileFormat = "*.csv";
            //when
            var actual = sut.GetAllFiles(sourceFolderPath, fileFormat, fileTypes);

            //then
            int countReturned = actual == null ? 0 : actual.Count();

            Assert.Equal(countOfFiles, countReturned);
            moqFolderParserService.Verify(v => v.GetFileNamesFromFolder(It.IsAny<string>(), It.Is<string>(p => p == "*.csv")), Times.Once);
        }
    }
}
