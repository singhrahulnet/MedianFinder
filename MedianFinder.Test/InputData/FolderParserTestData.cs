using System.Collections;
using System.Collections.Generic;

namespace MedianFinder.Test.InputData
{
    public class FolderParserTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //THE TEST DATA STRUCTURE
            //string[] fileNames, string sourceFolderPath, string searchPattern, int coutOfMatches

            //All matching
            yield return new object[] { new string[] { @"D:\\a.csv", @"D:\\b.csv", @"D:\\c.csv" }
                                        , "D:", "*.csv", 3 };
            //None matching - searcing CSV
            yield return new object[] { new string[] { @"D:\\a.TSV", @"D:\\b.TSV", @"D:\\c.TSV" }
                                        , "D:", "*.csv", 0 };
            //None matching - searcing TSV
            yield return new object[] { new string[] { @"D:\\a.csv", @"D:\\b.csv", @"D:\\c.csv" }
                                        , "D:", "*.TSV", 0 };
            //Few matching
            yield return new object[] { new string[] { @"D:\\a.csv", @"D:\\b.csv", @"D:\\c.TSV" }
                                        , "D:", "*.csv", 2 };
            //Other than csv - All matching
            yield return new object[] { new string[] { @"D:\\a.xls", @"D:\\b.xls", @"D:\\c.xls" }
                                        , "D:", "*.xls", 3 };
            //Other than csv - Few matching
            yield return new object[] { new string[] { @"D:\\a.xls", @"D:\\b.csv", @"D:\\c.xls" }
                                        , "D:", "*.xls", 2 };
            //Different Directory - All Matching
            yield return new object[] { new string[] { @"F:\\a.xls", @"F:\\b.csv", @"F:\\c.xls" }
                                        , "F:", "*.xls", 2 };
            //Multiple Directories - Few Matching
            yield return new object[] { new string[] { @"G:\\a.xls", @"H:\\b.csv", @"F:\\c.xls" }
                                        , "F:", "*.xls", 1 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
