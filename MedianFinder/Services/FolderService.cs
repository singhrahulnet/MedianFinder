using System;
using System.Collections.Generic;
using System.IO;

namespace MedianFinder.Services
{
    public interface IFolderService
    {
        IEnumerable<string> GetFileNames(string sourceFolderPath, string searchPattern);
    }
    public class FolderService : IFolderService
    {
        public IEnumerable<string> GetFileNames(string sourceFolderPath, string searchPattern)
        {
            if (string.IsNullOrEmpty(sourceFolderPath)) throw new ArgumentNullException("Source folder path is empty");
            IEnumerable<string> fileNames = null;
            try
            {
                fileNames = Directory.EnumerateFiles(sourceFolderPath, searchPattern);
            }
            catch (Exception)
            {
                // Yell    Log    Catch  Throw     
            }
            return fileNames;
        }
    }
}

