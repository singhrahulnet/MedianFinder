using MedianFinder.Services;
using System;

namespace MedianFinder.Managers
{
    public interface IMedianManager
    {
        int StartProcess();
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

        public int StartProcess()
        {
            int numberofFilesProcessed = 0;
            //Get all valid files paths from the source folder
            var filePaths = _folderManager.GetAllFiles(Startup.Settings.Path, 
                                                       Startup.Settings.FileFormat.Ext, 
                                                       Startup.Settings.FileTypes);

            try
            {
                foreach (string filePath in filePaths)
                {
                    var response = _dataProcessor.GetMedianVariance(filePath,
                                                        Startup.Settings.FileFormat.Delimiter,
                                                        Startup.Settings.LowerVariancePC,
                                                        Startup.Settings.UpperVariancePC);
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
