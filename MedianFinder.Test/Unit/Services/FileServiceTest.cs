using MedianFinder.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MedianFinder.Test.Unit.Services
{
    public class FileServiceTest : IDisposable
    {

        public FileServiceTest()
        {

        }
        public void Dispose()
        {
            
        }
        public static IEnumerable<object[]> GetFilePaths()
        {            
            yield return new object[] { "C:\\DEV\\LP.csv", "LP.csv" };           
        }
        public static IEnumerable<object[]> GetFileData()
        {
            
            yield return new object[] { new string[] { "0.0", "1.0", "2.0" }, 3 };
        }

        //[Theory]
        //[MemberData(nameof(GetFilePaths))]
        //public void FileName_returns_correct_name(string filePath, string fileName)
        //{
        //    //Given
        //    var sut = new FileService();
            
        //    sut.InitFileReader(filePath, ",");


        //    //When
        //    var actual = sut.FileName;

        //    //Then
        //    Assert.IsType<string>(actual);
        //    Assert.Equal(fileName, actual);
        //}

       
    }
}
