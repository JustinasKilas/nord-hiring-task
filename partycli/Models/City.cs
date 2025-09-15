namespace partycli.Models
{
	public class City
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public double  Latitude { get; set; }
		public double  Longitude { get; set; }
		public string DnsName { get; set; }
		public int HubScore { get; set; }
	}
}