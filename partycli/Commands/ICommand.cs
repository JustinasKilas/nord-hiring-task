using System.CommandLine;

namespace partycli.Commands
{
	public interface ICommand
	{
		Command GetCommand();
	}
}