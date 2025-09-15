using System.Threading.Tasks;

namespace partycli.Repositories
{
	public interface IConfigurationRepository
	{
		Task SetValueAsync(string key, string value);

		Task<string> GetValueAsync(string key);
	}
}