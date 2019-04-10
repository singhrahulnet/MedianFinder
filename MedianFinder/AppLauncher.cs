using MedianFinder.Managers;
using MedianFinder.Models;
using MedianFinder.Services;
using System;

namespace MedianFinder
{
    public interface IAppLauncher
    {
        int Launch();
    }
    class AppLauncher : IAppLauncher
    {
        IConfigService _configService;
        IMedianManager _medianManager;
        public AppLauncher(IConfigService configService, IMedianManager medianManager)
        {
            _configService = configService;
            _medianManager = medianManager;
        }
        private SourceFolderSettings _settings => _configService.GetSection<SourceFolderSettings>(nameof(SourceFolderSettings));

        public int Launch()
        {
            int processedFileCount = 0;
            try
            {
                processedFileCount = _medianManager.StartProcess(_settings);
            }
            catch (Exception)
            {
                // Yell    Log    Catch  Throw 
            }
            return processedFileCount;
        }
    }
}
