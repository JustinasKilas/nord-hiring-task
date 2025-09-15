namespace partycli.Models
{
	public class ServerResponseModel
	{
		public int Id { get; set; }
		public string CreatedAt { get; set; }
		public string UpdatedAt { get; set; }
		public string Name { get; set; }
		public string Station { get; set; }
		public string Ipv6Station { get; set; }
		public string Hostname { get; set; }
		public int Load { get; set; }
		public string Status { get; set; }
		public Location[] Locations { get; set; }
		public Service[] Services { get; set; }
		public Technology[] Technologies { get; set; }
		public Group[] Groups { get; set; }
		public Specification[] Specifications { get; set; }
		public IpItem[] Ips { get; set; }
	}
}