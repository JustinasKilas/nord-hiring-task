using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace partycli.Services
{
	public class AppDataStorageService : IStorageService
	{
		private readonly ILogger<AppDataStorageService> _logger;
		private static readonly string StorageDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "partycli");

		public AppDataStorageService(ILogger<AppDataStorageService> logger)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));

			Directory.CreateDirectory(StorageDirectory);
		}

		public Task<T> GetAsync<T>(string key) where T : class
		{
			try
			{
				var filePath = GetFilePath(key);
				if (!File.Exists(filePath))
				{
					_logger.LogDebug("Storage file not found for key: {Key}", key);
					return Task.FromResult((T)null);
				}

				var json = File.ReadAllText(filePath);
				var result = JsonConvert.DeserializeObject<T>(json);

				_logger.LogDebug("Successfully retrieved data for key: {Key}", key);
				return Task.FromResult(result);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to retrieve data for key: {Key}", key);
				return Task.FromResult((T)null);
			}
		}

		public Task SetAsync<T>(string key, T value) where T : class
		{
			string json = "**value serialization failed**";
			try
			{
				json = JsonConvert.SerializeObject(value);
				var filePath = GetFilePath(key);
				File.WriteAllText(filePath, json);

				_logger.LogDebug("Successfully stored data for key {Key}: {Value}", key, json);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to store data for key {Key}: {Value}", key, json);
				throw;
			}

			return Task.CompletedTask;
		}

		private static string GetFilePath(string key) => Path.Combine(StorageDirectory, $"{key}.json");
	}
}