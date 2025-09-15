using partycli.Services;

namespace partycli.Models
{
	public class ServerListRequestModel
	{
		public ServerListRequestModel(bool showLocal, Services.Country countryFilter, Protocol protocolFilter)
		{
			ShowLocal = showLocal;
			CountryFilter = countryFilter;
			ProtocolFilter = protocolFilter;
		}

		public bool ShowLocal { get; private set; }
		public Services.Country CountryFilter { get; private set; }
		public Protocol ProtocolFilter { get; private set; }

		public override string ToString()
		{
			return
				$"{nameof(ShowLocal)}: {ShowLocal}, {nameof(CountryFilter)}: {CountryFilter}, {nameof(ProtocolFilter)}: {ProtocolFilter}";
		}
	}
}