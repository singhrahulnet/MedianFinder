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
            return Directory.EnumerateFiles(sourceFolderPath, searchPattern);
        }
    }
}

