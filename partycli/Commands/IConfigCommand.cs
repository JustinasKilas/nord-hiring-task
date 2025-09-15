using System.Threading.Tasks;

namespace partycli.Commands
{
	public interface IConfigCommand : ICommand
	{
		Task ExecuteAsync(string key, string value);
	}
}