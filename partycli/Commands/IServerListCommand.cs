using System.Threading.Tasks;
using partycli.Models;

namespace partycli.Commands
{
	public interface IServerListCommand : ICommand
	{
		Task ExecuteAsync(ServerListRequestModel options);
	}
}