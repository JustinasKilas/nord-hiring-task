using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using partycli.Models;
using partycli.Repositories;
using partycli.Services;
using Country = partycli.Services.Country;

namespace partycli.Commands
{
	public class ServerListCommand : IServerListCommand
	{
		private readonly IVpnApiService _vpnApiService;
		private readonly ILocalServersRepository _localServersRepository;
		private readonly ILogger<ServerListCommand> _logger;

		public ServerListCommand(
			IVpnApiService vpnApiService,
			ILocalServersRepository localServersRepository,
			ILogger<ServerListCommand> logger)
		{
			_vpnApiService = vpnApiService ?? throw new ArgumentNullException(nameof(vpnApiService));
			_localServersRepository = localServersRepository ?? throw new ArgumentNullException(nameof(localServersRepository));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public Command GetCommand()
		{
			var serverListCommand = new Command("server_list", "Manage VPN server lists");
			var localOption = new Option<bool>("--local") { Description = "Display local server list" };
			var franceOption = new Option<bool>("--france") { Description = "Filter France servers" };
			var tcpOption = new Option<bool>("--tcp") { Description = "Filter TCP protocol servers" };

			serverListCommand.Add(localOption);
			serverListCommand.Add(franceOption);
			serverListCommand.Add(tcpOption);

			serverListCommand.SetAction(async (result, token) =>
			{
				var local = result.GetValue(localOption);
				var france = result.GetValue(franceOption);
				var tcp = result.GetValue(tcpOption);
				await ExecuteAsync(new ServerListRequestModel(
					local,
					france ? Country.France : default,
					tcp ? Protocol.Tcp : default
				));
			});

			return serverListCommand;
		}

		public async Task ExecuteAsync(ServerListRequestModel options)
		{
			_logger.LogInformation("Service list command invoked with options: {Options}", options);
			try
			{
				if (options.ShowLocal)
				{
					Console.WriteLine("Getting local servers...");
					await ShowLocalServersAsync(options.CountryFilter, options.ProtocolFilter);
					return;
				}

				Console.WriteLine("Getting remote servers...");
				var servers = await _vpnApiService.GetServersAsync(options.CountryFilter, options.ProtocolFilter);

				var serverList = servers.ToList();
				await _localServersRepository.SetLocalServers(serverList);
				Console.WriteLine("Servers saved successfully!");

				DisplayServers(serverList);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to execute server list command");
				Console.WriteLine($"Error: {ex.Message}");
			}
		}

		private async Task ShowLocalServersAsync(Country countryFilter, Protocol protocolFilter)
		{
			var servers = await _localServersRepository.GetServers();

			if (servers == null || !servers.Any())
			{
				Console.WriteLine("No server data found in local storage.");
				Console.WriteLine("Run 'server-list' command first to fetch and save server data.");
				return;
			}
			var filteredServers = servers
				.Where(x => countryFilter == default || x.Locations.Any(xx => xx.Country.Id == (int)countryFilter))
				.Where(x => protocolFilter == default || x.Technologies.Any(xx => xx.Id == (int)protocolFilter))
				.ToList();

			if (filteredServers.Count == 0)
			{
				Console.WriteLine("No servers match the specified filters in local storage.");
				return;
			}

			DisplayServers(filteredServers);
		}

		private void DisplayServers(IEnumerable<ServerResponseModel> servers)
		{
			Console.WriteLine("\nVPN Server List:");
			Console.WriteLine(new string('─', 50));
			var totalServers = 0;
			foreach (var server in servers)
			{
				var status = server.Status.ToLowerInvariant();
				Console.WriteLine($"{status} - {server.Name} (Load: {server.Load}%)");
				totalServers++;
			}

			Console.WriteLine(new string('─', 50));
			Console.WriteLine($"Total servers: {totalServers}");
		}
	}
}