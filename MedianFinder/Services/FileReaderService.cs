using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.IO.Abstractions;


namespace MedianFinder.Services
{
    public interface IFileReaderService
    {
        void InitFileReader(string filePath, string fileDelimiter);
        string FileName { get; }
        IEnumerable<string> IterateFileOnColumn(string columnName);
        string GetColumnValueFromCurrentLine(string columnName);
    }
    class FileReaderService : IFileReaderService
    {
        private readonly IFileSystem _fileSystem;
        private List<string> _headers;
        private string _currentLine, _filePath, _fileDelimiter;
        private StreamReader FileReader { get; set; }

        public FileReaderService(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void InitFileReader(string filePath, string fileDelimiter)
        {
            //Always good to validate the input parameter in public methods
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(fileDelimiter)) return;

            _filePath = filePath; _fileDelimiter = fileDelimiter;

            string headerString = IterateFileByLine().Take(1).FirstOrDefault();

            //Save the header values for later usage
            _headers = !string.IsNullOrEmpty(headerString) ? headerString.Split(_fileDelimiter).ToList() : throw new InvalidOperationException("Header row is missing");
        }

        public string FileName
        {
            get => _fileSystem.Path.GetFileName(_filePath);
        }

        public IEnumerable<string> IterateFileOnColumn(string columnName)
        {
            //Always good to validate the input parameter in public methods
            if (string.IsNullOrEmpty(columnName)) yield return null;

            int columnIndex = _headers.FindIndex(x => x == columnName);

            foreach (var row in IterateFileByLine().Skip(1))
            {
                yield return row.Split(_fileDelimiter)[columnIndex];
            }
        }

        public string GetColumnValueFromCurrentLine(string columnName)
        {
            //Always good to validate the input parameter in public methods
            if (string.IsNullOrEmpty(columnName)) return null;

            //Find the column index from our header
            int columnIndex = _headers.FindIndex(x => x == columnName);

            //We already have the current line as a string. Let's grab our value
            return _currentLine.Split(_fileDelimiter)[columnIndex];
        }

        private IEnumerable<string> IterateFileByLine()
        {
            using (FileReader = _fileSystem.File.OpenText(_filePath))
            {
                //Save the current line being read in local variable "_currentLine"
                while ((_currentLine = FileReader.ReadLine()) != null)
                {
                    yield return _currentLine;
                }
            }
        }
    }
}
