namespace partycli.Models
{
	public class Specification
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Identifier { get; set; }
		public SpecificationValue[] Values { get; set; }
	}
}