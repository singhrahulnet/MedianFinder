using System.Collections;
using System.Collections.Generic;

namespace MedianFinder.Test.InputData
{
    public class FileReaderTestData
    {
        public class IterateTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                //THE TEST DATA STRUCTURE
                //string filePath, string fileData, string columnName, int countOfDataElements

                //Column Name "Data Value", Count 2
                yield return new object[] { "z:\\test\\LP_123.csv",
                @"MeterPoint Code,Serial Number,Plant Code,Date/Time,Data Type,Data Value,Units,Status 
                 210095893,210095893,ED031000001,31/08/2015 00:45:00,Import Wh Total,0.000000,kwh,
                 210095893,210095893,ED031000001,31/08/2015 01:00:00,Import Wh Total,0.000000,kwh,",
                "Data Value",
                2
                };

                //Column Name "MeterPoint Code", Count 2
                yield return new object[] { "z:\\test\\LP_123.csv",
                @"MeterPoint Code,Serial Number,Plant Code,Date/Time,Data Type,Data Value,Units,Status 
                 210095893,210095893,ED031000001,31/08/2015 00:45:00,Import Wh Total,0.000000,kwh,
                 210095893,210095893,ED031000001,31/08/2015 01:00:00,Import Wh Total,0.000000,kwh,",
                "MeterPoint Code",
                2
                };

                //Column Name "Serial Number", Count 3
                yield return new object[] { "z:\\test\\LP_123.csv",
                @"MeterPoint Code,Serial Number
                 210095893,210095893
                 210095893,210095893
                 210095893,210095893",
                "Serial Number",
                3
                };

                //Empty file
                yield return new object[] { "z:\\test\\LP_123.csv",
                @"MeterPoint Code,Serial Number",
                "Serial Number",
                0
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class ColumnValueTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                //THE TEST DATA STRUCTURE
                //string filePath, string fileData, string columnName, int countOfDataElements

                //Column Name "Data Value"
                yield return new object[] { "z:\\test\\LP_123.csv",
                @"MeterPoint Code,Serial Number,Plant Code,Date/Time,Data Type,Data Value,Units,Status 
                 210095893,210095893,ED031000001,31/08/2015 00:45:00,Import Wh Total,0.000000,kwh,
                 210095893,210095893,ED031000001,31/08/2015 01:00:00,Import Wh Total,0.000000,kwh,",
                "Data Value",
                "0.000000"
                };

                //Column Name "Plant Code"
                yield return new object[] { "z:\\test\\LP_123.csv",
                @"MeterPoint Code,Serial Number,Plant Code,Date/Time,Data Type,Data Value,Units,Status 
                 210095893,210095893,ED031000001,31/08/2015 00:45:00,Import Wh Total,0.000000,kwh,
                 210095893,210095893,ED031000001,31/08/2015 01:00:00,Import Wh Total,0.000000,kwh,",
                "Plant Code",
                "ED031000001"
                };

                //Column Name "Date/Time"
                yield return new object[] { "z:\\test\\LP_123.csv",
                @"MeterPoint Code,Serial Number,Plant Code,Date/Time,Data Type,Data Value,Units,Status 
                 210095893,210095893,ED031000001,31/08/2015 00:45:00,Import Wh Total,0.000000,kwh,
                 210095893,210095893,ED031000001,31/08/2015 01:00:00,Import Wh Total,0.000000,kwh,",
                "Date/Time",
                "31/08/2015 00:45:00"
                };

                //Column Name "Serial Number"
                yield return new object[] { "z:\\test\\LP_123.csv",
                @"MeterPoint Code,Serial Number,
                 210095893,210095893,
                 210095893,210095893,
                 210095893,210095893",
                "Serial Number",
                "210095893"
                };                
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
