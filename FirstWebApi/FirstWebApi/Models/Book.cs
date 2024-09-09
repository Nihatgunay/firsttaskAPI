namespace FirstWebApi.Models
{
	public class Book : BaseEntity
	{
		public int GenreId { get; set; }
		public string Name { get; set; }
		public double CostPrice { get; set; }
		public double SalePrice { get; set; }

		public Genre Genre { get; set; }
	}
}
