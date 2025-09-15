namespace partycli.Models
{
	public class Technology
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Identifier { get; set; }
		public string CreatedAt { get; set; }
		public string UpdatedAt { get; set; }
		public Metadata[] Metadata { get; set; }
		public Pivot Pivot { get; set; }
	}
}