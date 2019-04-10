using MedianFinder.Services;
using MedianFinder.Test.InputData;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using Xunit;

namespace MedianFinder.Test.Unit.Services
{
    public class FileReaderServiceTest
    {
        public static IEnumerable<object[]> GetFiles()
        {
            //THE TEST DATA STRUCTURE
            //string filePath, string fileData, string expectedFileName

            yield return new object[] { "z:\\test\\LP_123.csv", "test data,is good", "LP_123.csv" };
            yield return new object[] { "z:\\test\\TOU_123.csv", "test data,is good", "TOU_123.csv" };
            yield return new object[] { "T:\\test\\LP_123.TSV", "test data,is good", "LP_123.TSV" };
            yield return new object[] { "T:\\test\\TOU_123.XLS", "test data,is good", "TOU_123.XLS" };
        }
       
        [Theory]
        [MemberData(nameof(GetFiles))]
        public void FileName_returns_correct_name(string filePath, string fileData, string expectedFileName)
        {
            //Given
            var mockFileObject = InitFileSystem(filePath, fileData);
            var sut = new FileReaderService(mockFileObject.FileSystem);
            sut.InitFileReader(filePath, ",");

            //When
            var actual = sut.FileName;

            //Then
            Assert.IsType<string>(actual);
            Assert.Equal(expectedFileName, actual);
        }

        [Theory]
        [ClassData(typeof(FileReaderTestData.IterateTestData))]
        public void IterateFileOnColumn_returns_correct_count(string filePath, string fileData, string columnName, int countOfDataElements)
        {
            //given
            var mockFileObject = InitFileSystem(filePath, fileData);
            var sut = new FileReaderService(mockFileObject.FileSystem);
            sut.InitFileReader(filePath, ",");

            //when
            var actual = sut.IterateFileOnColumn(columnName);

            //then
            Assert.IsAssignableFrom<IEnumerable<string>>(actual);
            Assert.Equal(countOfDataElements, actual.Count());
        }


        [Theory]
        [ClassData(typeof(FileReaderTestData.ColumnValueTestData))]
        public void GetColumnValueFromCurrentLine_returns_correct_value(string filePath, string fileData, string columnName, string columnValue)
        {
            //given
            var mockFileObject = InitFileSystem(filePath, fileData);
            var sut = new FileReaderService(mockFileObject.FileSystem);
            sut.InitFileReader(filePath, ",");
            var data = sut.IterateFileOnColumn(columnName).ElementAtOrDefault(0);            

            //when
            var actual = sut.GetColumnValueFromCurrentLine(columnName);

            //then
            Assert.IsType<string>(actual);            
            Assert.Equal(columnValue, actual);
        }

        private MockFileSystem InitFileSystem(string filePath, string fileData)
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(filePath, new MockFileData(fileData));

            return mockFileSystem;
        }
    }
}
