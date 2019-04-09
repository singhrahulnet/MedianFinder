using MedianFinder.Models;
using MedianFinder.Services;

namespace MedianFinder.Managers
{
    public interface IMainManager
    {
        void StartProcess();
    }
    class MainManager : IMainManager
    {
        IConfigService _configService;
        IMedianManager _medianManager;
        public MainManager(IConfigService configService, IMedianManager medianManager)
        {
            _configService = configService;
            _medianManager = medianManager;
        }
        private SourceFolderSettings _settings => _configService.GetSection<SourceFolderSettings>(nameof(SourceFolderSettings));

        public void StartProcess()
        {
            _medianManager.StartProcess(_settings);
        }
    }
}
