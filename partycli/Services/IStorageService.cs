using System.Threading.Tasks;

namespace partycli.Services
{
	public interface IStorageService
	{
		Task<T> GetAsync<T>(string key) where T : class;

		Task SetAsync<T>(string key, T value) where T : class;
	}
}