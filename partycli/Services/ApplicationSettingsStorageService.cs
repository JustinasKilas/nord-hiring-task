using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using partycli.Services;

public class ApplicationSettingsStorageService : IStorageService
{
	private readonly ILogger<ApplicationSettingsStorageService> _logger;

	public ApplicationSettingsStorageService(ILogger<ApplicationSettingsStorageService> logger)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	public Task<T> GetAsync<T>(string key) where T : class
	{
		try
		{
			var settings = partycli.Properties.Settings.Default;
			if (string.IsNullOrEmpty((string)settings[key]))
			{
				_logger.LogWarning("Error: Couldn't get '{Key}'. Check if command was input correctly.", key);
				return Task.FromResult((T)null);
			}
			var t = JsonConvert.DeserializeObject<T>((string)settings[key]);
			return Task.FromResult(t);
		}
		catch (Exception ex)
		{
			_logger.LogError("Error: Couldn't get '{Key}'.", key);
			return Task.FromResult((T)null);
		}
	}

	public Task SetAsync<T>(string key, T value) where T : class
	{
		try
		{
			var settings = partycli.Properties.Settings.Default;
			var json = JsonConvert.SerializeObject(value);
			settings[key] = json;
			settings.Save();
		}
		catch (Exception ex)
		{
			_logger.LogError("Error: Couldn't save '{Key}'. Check if command was input correctly.", key);
		}
		return Task.CompletedTask;
	}
}