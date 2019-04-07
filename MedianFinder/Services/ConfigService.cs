using Microsoft.Extensions.Configuration;
using System;

namespace MedianFinder.Services
{
    public interface IConfigService
    {
        TResult GetSection<TResult>(string sectionName) where TResult : class;
    }
    public class ConfigService : IConfigService
    {
        private readonly IConfiguration _configuration;

        public ConfigService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentException(nameof(configuration));
        }

        public TResult GetSection<TResult>(string sectionName) where TResult : class
        {
            if (string.IsNullOrEmpty(sectionName)) throw new ArgumentNullException("Section name is empty");

            var section = Activator.CreateInstance(typeof(TResult)) as TResult;
            _configuration.GetSection(sectionName).Bind(section);

            return section;
        }
    }
}
