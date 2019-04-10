using MedianFinder.Services;
using MedianFinder.Test.InputData;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using Xunit;

namespace MedianFinder.Test.Unit.Services
{
    public class FolderParserServiceTest
    {

        [Theory]
        [ClassData(typeof(FolderParserTestData))]
        public void GetFileNamesFromFolder_returns_lis_of_files(string[] fileNames, string sourceFolderPath, string searchPattern, int coutOfMatches)
        {
            //Given
            var mockFileObject = InitFileSystem(fileNames);
            var sut = new FolderParserService(mockFileObject.FileSystem);

            //When
            var actual = sut.GetFileNamesFromFolder(sourceFolderPath, searchPattern);

            //Then
            Assert.IsAssignableFrom<IEnumerable<string>>(actual);
            Assert.Equal(coutOfMatches, actual.Count());
        }

        private MockFileSystem InitFileSystem(string[] fileNames)
        {
            var mockFileSystem = new MockFileSystem();
            foreach (var file in fileNames)
            {
                mockFileSystem.AddFile(file, new MockFileData("junk text content"));
            }
            return mockFileSystem;
        }
    }
}
