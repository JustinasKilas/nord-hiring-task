using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using partycli.Models;

namespace partycli.Services
{
	public interface IVpnApiService
	{
		Task<IEnumerable<ServerResponseModel>> GetServersAsync(
			Country country = default,
			Protocol protocol = default,
			CancellationToken cancellationToken = default
		);
	}
}