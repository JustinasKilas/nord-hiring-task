namespace partycli.Services
{
	public class VpnApiSettings
	{
		public string BaseUrl { get; set; } = "https://api.nordvpn.com/v1/";
		public int TimeoutSeconds { get; set; } = 30;
	}

	//TODO: Fill others
	public enum Country
	{
		NotSet = default,
		Albania = 2,
		Argentina = 10,
		France = 74,
	}

	//TODO: Fill others
	public enum Protocol
	{
		NotSet = default,
		Udp = 3,
		Tcp = 5,
		NordLynx = 35,
	}
}