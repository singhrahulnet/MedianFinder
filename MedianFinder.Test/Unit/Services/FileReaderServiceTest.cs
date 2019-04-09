using MedianFinder.Models;
using MedianFinder.Services;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace MedianFinder.Test.Unit.Services
{
    public class FileReaderServiceTest
    {
        public static IEnumerable<object[]> GetFilePaths()
        {
            yield return new object[] { "z:\\test\\LP_123.csv", "test data,is good" };
        }
        [Theory]
        [MemberData(nameof(GetFilePaths))]
        public void FileName_returns_correct_name(string filePath, string fileData)
        {
            //Given
            dynamic mockobject = Initialization(filePath, fileData);
            var sut = new FileReaderService(mockobject.FileSystem);

            sut.InitFileReader(filePath, ",");


            //When
            var actual = sut.FileName;

            //Then
            Assert.IsType<string>(actual);
            Assert.Equal(mockobject.FileName, actual);
        }

        [Theory]
        [MemberData(nameof(GetFilePaths))]
        public void IterateFileOnColumn(string filePath, string fileData)
        {
            //given
            dynamic mockobject = Initialization(filePath, fileData);
            var sut = new FileReaderService(mockobject.FileSystem);
            sut.InitFileReader(filePath, ",");
            var fileTypes = new Dictionary<string, string>() {
                {"TOU","Energy" },
                {"LP","Data Value" }
            };
            var model = new MedianVarianceResult(fileTypes)
            {
                FileName = sut.FileName,
                VarianceData = new List<VarianceData>()
            };
            //when
            var actual = sut.IterateFileOnColumn(model.DataColumnName);

            //then

        }

        dynamic Initialization(string filePath, string fileData)
        {
            var mockFileSystem = new MockFileSystem();
            var mockInputFile = new MockFileData(fileData);
            mockFileSystem.AddFile(filePath, mockInputFile);
            var fileName = mockFileSystem.Path.GetFileName(filePath);
            return new
            {
                FileSystem = mockFileSystem,
                FileName = fileName
            };
        }
    }
}
