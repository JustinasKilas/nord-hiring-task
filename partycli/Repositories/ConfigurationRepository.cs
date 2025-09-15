using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using partycli.Services;

namespace partycli.Repositories
{
	public class ConfigurationRepository : IConfigurationRepository
	{
		private readonly IStorageService _storageService;
		private readonly ILogger<ConfigurationRepository> _logger;

		public ConfigurationRepository(IStorageService storageService, ILogger<ConfigurationRepository> logger)
		{
			_storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task SetValueAsync(string key, string value)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				_logger.LogWarning("Configuration not updated - invalid key value: '{Key}'", key ?? "NULL");
			}
			try
			{
				await _storageService.SetAsync(key, value);
				_logger.LogInformation("Config value set to: '{Key}' = '{Value}'", key, value);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to set config value to '{Key}' = '{Value}'", key, value);
			}
		}

		public Task<string> GetValueAsync(string key)
		{
			return _storageService.GetAsync<string>(key);
		}
	}
}