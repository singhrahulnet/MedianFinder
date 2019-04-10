using MedianFinder.Models;
using MedianFinder.Services;
using System;

namespace MedianFinder.Managers
{
    public interface IMedianManager
    {
        int StartProcess(SourceFolderSettings settings);
    }
    class MedianManager : IMedianManager
    {
        private readonly IFolderManager _folderManager;
        private readonly IDataProcessor _dataProcessor;
        private readonly IOutputService _outputService;

        public MedianManager(IFolderManager folderManager, IDataProcessor dataProcessor, IOutputService outputService)
        {
            _folderManager = folderManager ?? throw new ArgumentNullException(nameof(folderManager));
            _dataProcessor = dataProcessor ?? throw new ArgumentNullException(nameof(dataProcessor));
            _outputService = outputService ?? throw new ArgumentNullException(nameof(outputService));
        }

        public int StartProcess(SourceFolderSettings settings)
        {
            int numberofFilesProcessed = 0;
            //Get all valid files paths from the source folder
            var filePaths = _folderManager.GetAllFiles(settings.Path,
                                                       settings.FileFormat.Ext,
                                                       settings.FileTypes);

            try
            {
                foreach (string filePath in filePaths)
                {
                    var response = _dataProcessor.GetMedianVariance(filePath, settings);

                    if (response != null) numberofFilesProcessed++;
                    //print now and move to next file.
                    _outputService.OutputResult(response);
                }
            }
            catch (Exception)
            {
                // Yell    Log    Catch  Throw     
            }
            return numberofFilesProcessed;
        }
    }
}
