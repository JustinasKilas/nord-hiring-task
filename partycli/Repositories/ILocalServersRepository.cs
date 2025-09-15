using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using partycli.Models;

namespace partycli.Repositories
{
	public interface ILocalServersRepository
	{
		Task SetLocalServers(IEnumerable<ServerResponseModel> servers);

		Task<IQueryable<ServerResponseModel>> GetServers();
	}
}