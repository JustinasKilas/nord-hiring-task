using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using partycli.Models;
using partycli.Services;

namespace partycli.Repositories
{
	public class LocalServersRepository : ILocalServersRepository
	{
		private readonly IStorageService _storageService;
		private const string ServerListKey = "serverlist";

		public LocalServersRepository(IStorageService storageService)
		{
			_storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
		}

		public async Task SetLocalServers(IEnumerable<ServerResponseModel> servers)
		{
			await _storageService.SetAsync(ServerListKey, servers.ToList());
		}

		public async Task<IQueryable<ServerResponseModel>> GetServers()
		{
			var servers = await _storageService.GetAsync<List<ServerResponseModel>>(ServerListKey);
			return servers?.AsQueryable() ?? Enumerable.Empty<ServerResponseModel>().AsQueryable();
		}
	}
}