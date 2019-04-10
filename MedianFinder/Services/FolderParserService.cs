using System;
using System.Collections.Generic;
using System.IO.Abstractions;

namespace MedianFinder.Services
{
    public interface IFolderParserService
    {
        IEnumerable<string> GetFileNamesFromFolder(string sourceFolderPath, string searchPattern);
    }
    class FolderParserService : IFolderParserService
    {
        private readonly IFileSystem _fileSystem;
        public FolderParserService(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        public IEnumerable<string> GetFileNamesFromFolder(string sourceFolderPath, string searchPattern)
        {
            //Always good to validate the input parameter in public methods
            if (string.IsNullOrEmpty(sourceFolderPath)) return null;

            IEnumerable<string> fileNames = null;
            try
            {
                fileNames = GetAllFileNames(sourceFolderPath, searchPattern);
            }
            catch (Exception)
            {
                // Yell    Log    Catch  Throw     
            }
            return fileNames;
        }

        private IEnumerable<string> GetAllFileNames(string sourceFolderPath, string searchPattern)
        {
            return _fileSystem.Directory.EnumerateFiles(sourceFolderPath, searchPattern);
        }
    }
}

