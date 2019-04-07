using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MedianFinder.Services
{
    public interface IFileService
    {
        void InitFileReader(string filePath, string fileDelimiter);
        string FileName { get; }
        IEnumerable<string> IterateFile(string columnName);
        string GetValueFromCurrentLine(string columnName);
    }
    class FileService : IFileService
    {
        private List<string> _headers;
        private string _currentLine;
        private string _filePath;
        private string _fileDelimiter;
        private StreamReader FileReader { get; set; }


        public void InitFileReader(string filePath, string fileDelimiter)
        {
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(fileDelimiter)) throw new ArgumentNullException("File path is empty");

            _filePath = filePath; _fileDelimiter = fileDelimiter;

            string headerString = IterateFile().Take(1).FirstOrDefault();

            _headers = !string.IsNullOrEmpty(headerString) ? headerString.Split(_fileDelimiter).ToList() : throw new InvalidOperationException("Header row is missing");
        }
        public string FileName
        {
            get => Path.GetFileName(_filePath);
        }
        public IEnumerable<string> IterateFile(string columnName)
        {
            if (string.IsNullOrEmpty(columnName)) throw new ArgumentNullException("Column name is empty");

            int columnIndex = _headers.FindIndex(x => x == columnName);

            foreach (var row in IterateFile().Skip(1))
            {
                yield return row.Split(_fileDelimiter)[columnIndex];
            }
        }
        public string GetValueFromCurrentLine(string columnName)
        {
            if (string.IsNullOrEmpty(columnName)) throw new ArgumentNullException("Column name is empty");

            int columnIndex = _headers.FindIndex(x => x == columnName);

            return _currentLine.Split(_fileDelimiter)[columnIndex];
        }
        private IEnumerable<string> IterateFile()
        {
            using (FileReader = File.OpenText(_filePath))
            {
                while ((_currentLine = FileReader.ReadLine()) != null)
                {
                    yield return _currentLine;
                }
            }
        }
    }
}
