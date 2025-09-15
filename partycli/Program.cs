// Program.cs

using System.Collections.Generic;
using System.CommandLine;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using partycli.Commands;
using partycli.Repositories;
using partycli.Services;

namespace partycli
{
	public class Program
	{
		public static async Task<int> Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();
			var logger = host.Services.GetRequiredService<ILogger<Program>>();

			logger.LogInformation("Command input: '{command}'", string.Join(" ", args));

			var rootCommand = new RootCommand("VPN CLI tool for managing NordVPN servers");

			var serverListCommandHandler = host.Services.GetRequiredService<IServerListCommand>();
			var serverListCommand = serverListCommandHandler.GetCommand();

			var configCommandHandler = host.Services.GetRequiredService<IConfigCommand>();
			var configCommand = configCommandHandler.GetCommand();

			rootCommand.Subcommands.Add(serverListCommand);
			rootCommand.Subcommands.Add(configCommand);
			var parsed = rootCommand.Parse(args);

			logger.LogInformation("Command parsed: {command}", parsed);

			return await parsed.InvokeAsync();
		}

		private static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureAppConfiguration((hostingContext, config) =>
				{
					var vpnApiSettings = new Dictionary<string, string>
					{
						{ "VpnApi:BaseUrl", Properties.Settings.Default.BaseUrl },
						{ "VpnApi:TimeoutSeconds", Properties.Settings.Default.TimeoutSeconds.ToString() }
					};

					config.AddInMemoryCollection(vpnApiSettings);
				})

				.ConfigureServices((context, services) =>
				{
					services.Configure<VpnApiSettings>(context.Configuration.GetSection("VpnApi"));

					services.AddHttpClient<IVpnApiService, VpnApiService>();

					//TODO: Choose one of the storage services by uncommenting the desired line
					services.AddSingleton<IStorageService, ApplicationSettingsStorageService>();
					//services.AddSingleton<IStorageService, AppDataStorageService>();

					services.AddTransient<IServerListCommand, ServerListCommand>();
					services.AddTransient<IConfigCommand, ConfigCommand>();

					services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
					services.AddScoped<ILocalServersRepository, LocalServersRepository>();
				})
				.ConfigureLogging(logging =>
				{
					logging.ClearProviders();
					//TODO: add providers as needed

					logging.AddConsole();

					logging.SetMinimumLevel(LogLevel.Information);
				});
	}
}