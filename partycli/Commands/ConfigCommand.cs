using System;
using System.CommandLine;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using partycli.Repositories;

namespace partycli.Commands
{
	internal class ConfigCommand : IConfigCommand
	{
		private readonly IConfigurationRepository _configurationRepository;
		private readonly ILogger<ConfigCommand> _logger;

		public ConfigCommand(
			IConfigurationRepository configurationRepository,
			ILogger<ConfigCommand> logger)
		{
			_configurationRepository = configurationRepository ?? throw new ArgumentNullException(nameof(configurationRepository));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public Command GetCommand()
		{
			var configCommand = new Command("config", "Manage configuration");
			var nameArgument = new Argument<string>("name") { Description = "Configuration name" };
			var valueArgument = new Argument<string>("value") { Description = "Configuration value" };

			configCommand.Add(nameArgument);
			configCommand.Add(valueArgument);

			configCommand.SetAction(async (result, token) =>
			{
				var name = result.GetValue(nameArgument);
				var value = result.GetValue(valueArgument);
				await ExecuteAsync(name, value);
			});

			return configCommand;
		}

		public async Task ExecuteAsync(string key, string value)
		{
			_logger.LogInformation("Service list command invoked with key: '{Key}', value: '{Value}'", key, value);
			try
			{
				var normalizedKey = NormalizeKey(key);

				if (string.IsNullOrWhiteSpace(normalizedKey))
				{
					_logger.LogWarning("Configuration name cannot be empty.");
					Console.WriteLine("Configuration name cannot be empty.");
					return;
				}

				await _configurationRepository.SetValueAsync(normalizedKey, value);

				Console.WriteLine($"Configuration updated: {normalizedKey} = {value}");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to update configuration: {Name}", key);
				Console.WriteLine($"Error updating configuration: {ex.Message}");
			}
		}

		private static string NormalizeKey(string name)
		{
			return name?.Replace("-", string.Empty).ToLowerInvariant();
		}
	}
}