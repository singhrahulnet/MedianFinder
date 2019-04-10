using MedianFinder.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

            //Construct regex pattern based on all valid file types
            string fileNamePattern = Helpers.RegexHelper.FileNamePattern(fileTypes);
            Regex fileSelectionPattern = new Regex(Helpers.RegexHelper.FileSelectionPattern);

            IEnumerable<string> allFilePaths = null;
            try
            {

                allFilePaths = _folderParserService.GetFileNamesFromFolder(sourceFolderPath, fileExt)
                                     //.Where(fileName => fileTypes.Any(type => fileSelectionPattern.IsMatch((fileName))));
                                     .Where(filePath => fileTypes.Any(
                                         fileType =>
                                         {
                                             var fName = fileSelectionPattern.Match(filePath);
                                             return (fName.Success && Regex.IsMatch(fName.Groups[1].Value, fileNamePattern));                                            
                                         }
                                         ));
            }
            catch (Exception)
            {
                // Yell    Log    Catch  Throw     
            }
            return allFilePaths;
        }
    }
}
