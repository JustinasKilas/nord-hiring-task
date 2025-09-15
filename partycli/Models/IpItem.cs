namespace partycli.Models
{
	public class IpItem
	{
		public int Id { get; set; }
		public string CreatedAt { get; set; }
		public string UpdatedAt { get; set; }
		public int ServerId { get; set; }
		public int IpId { get; set; }
		public string Type { get; set; }
		public IpDetails Ip { get; set; }
	}
}