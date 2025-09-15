namespace partycli.Models
{
	public class Group
	{
		public int Id { get; set; }
		public string CreatedAt { get; set; }
		public string UpdatedAt { get; set; }
		public string Title { get; set; }
		public string Identifier { get; set; }
		public Type Type { get; set; }
	}
}