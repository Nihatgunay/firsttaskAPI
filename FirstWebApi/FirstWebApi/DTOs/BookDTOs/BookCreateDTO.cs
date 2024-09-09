namespace FirstWebApi.DTOs.BookDTOs
{
	public record BookCreateDTO(string name, double costprice, double saleprice, int genreId);
}
