namespace partycli.Models
{
	public class Country
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public City City { get; set; }
	}
}