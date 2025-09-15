using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using partycli.Models;

namespace partycli.Services
{
	public class VpnApiService : IVpnApiService
	{
		private readonly HttpClient _httpClient;
		private readonly ILogger<VpnApiService> _logger;

		public VpnApiService(
			HttpClient httpClient,
			ILogger<VpnApiService> logger,
			IOptions<VpnApiSettings> settings)
		{
			_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			var apiSettings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));

			_httpClient.BaseAddress = new Uri(apiSettings.BaseUrl);
			_httpClient.Timeout = TimeSpan.FromSeconds(apiSettings.TimeoutSeconds);
		}

		public async Task<IEnumerable<ServerResponseModel>> GetServersAsync(Country country = default, Protocol protocol = default, CancellationToken cancellationToken = default)
		{
			_logger.LogInformation("Fetching VPN servers");

			try
			{
				var query = "?";
				if (country != default)
				{
					query += $"filters[country_id]={(int)country}&";
				}
				if (protocol != default)
				{
					query += $"filters[servers_technologies][id]={(int)protocol}&";
				}

				query = query == "?" ? null : query;
				var path = $"servers{query}";

				_logger.LogInformation("Requesting path: {Path}", path);

				var response = await _httpClient.GetAsync(path, cancellationToken);
				response.EnsureSuccessStatusCode();

				var content = await response.Content.ReadAsStringAsync();
				var servers = JsonConvert.DeserializeObject<List<ServerResponseModel>>(content) ?? new List<ServerResponseModel>();

				_logger.LogInformation("Successfully fetched {Count} servers", servers.Count);
				return servers;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to fetch servers");
				throw;
			}
		}
	}
}