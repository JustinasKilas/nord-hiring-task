namespace partycli.Models
{
	public class Location
	{
		public int Id { get; set; }
		public string CreatedAt { get; set; }
		public string UpdatedAt { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public Country Country { get; set; }
	}
}