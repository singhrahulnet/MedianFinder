using MedianFinder.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedianFinder.Managers
{
    public interface IFolderManager
    {
        IEnumerable<string> GetAllFiles(string sourceFolderPath, string fileExt, Dictionary<string, string> fileTypes);
    }
    class FolderManager : IFolderManager
    {
        private readonly IFolderParserService _folderParserService;

        public FolderManager(IFolderParserService folderParserService)
        {
            _folderParserService = folderParserService ?? throw new ArgumentNullException(nameof(folderParserService));
        }

        public IEnumerable<string> GetAllFiles(string sourceFolderPath, string fileExt, Dictionary<string, string> fileTypes)
        {
            //Always good to validate the input parameter in public methods
            if (string.IsNullOrEmpty(sourceFolderPath)) return null;

            IEnumerable<string> allFiles = null;
            try
            {

                allFiles = _folderParserService.GetFileNamesFromFolder(sourceFolderPath, fileExt)
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
