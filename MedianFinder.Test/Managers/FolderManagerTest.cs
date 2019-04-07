using MedianFinder.Managers;
using MedianFinder.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MedianFinder.Test.Managers
{
    public class FolderManagerTest : IDisposable
    {
        public static IEnumerable<object[]> GetFiles()
        {
            yield return new object[] { new string[] { "LP1.csv", "LP2.csv", "TOU1.csv" } };
            yield return new object[] { new string[] { "file1.csv", "file2.csv", "file3.csv", "file4.csv" } };
        }
        Mock<IFolderService> moqFolderService = null;
        public FolderManagerTest()
        {
            moqFolderService = new Mock<IFolderService>();
        }

        public void Dispose()
        {
        }

        [Theory]
        [MemberData(nameof(GetFiles))]
        //[InlineData(new object[] { new string[] { "file1.csv", "file2.csv", "file3.csv" } })]
        public void GetAllFiles_returns_all_files_with_required_file_name_from_a_file_location(IEnumerable<string> filesList)
        {
            //given

            moqFolderService.Setup(m => m.GetFileNames(It.IsAny<string>(), It.Is<string>(p => p == "*.csv"))).Returns(filesList);
            var sut = new FolderManager(moqFolderService.Object);

            //when
            Startup.ConfigureServices();
            var actual = sut.GetAllFiles(Startup.Settings.Path, Startup.Settings.FileTypes);

            //then
            Assert.IsAssignableFrom<IEnumerable<string>>(actual);
            moqFolderService.Verify(v => v.GetFileNames(It.IsAny<string>(), It.Is<string>(p => p == "*.csv")), Times.Once);
        }
    }
}
