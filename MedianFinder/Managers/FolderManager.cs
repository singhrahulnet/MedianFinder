using MedianFinder.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedianFinder.Managers
{
    public interface IFolderManager
    {
        IEnumerable<string> GetAllFiles(string sourceFolderPath, Dictionary<string, string> fileTypes);
    }
    public class FolderManager : IFolderManager
    {
        private readonly IFolderService _folderService;
        private readonly string fileExt = "*.csv";
        public FolderManager(IFolderService folderService)
        {
            _folderService = folderService ?? throw new ArgumentNullException(nameof(folderService));
        }

        public IEnumerable<string> GetAllFiles(string sourceFolderPath, Dictionary<string, string> fileTypes)
        {
            if (string.IsNullOrEmpty(sourceFolderPath)) throw new ArgumentNullException("Source folder path is empty");

            IEnumerable<string> allFiles = null;
            try
            {

                allFiles = _folderService.GetFileNames(sourceFolderPath, fileExt)
                                     .Where(fileName => fileTypes.Any(type => fileName.Contains(type.Key)));
                //Logic can be more refined. it just checks if filetype is contained in file path
            }
            catch (Exception)
            {
                // Yell    Log    Catch  Throw     
            }
            return allFiles;
        }
    }
}
