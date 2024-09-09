namespace FirstWebApi.DTOs.BookDTOs
{
	public record BookUpdateDTO(string name, double costprice, double saleprice, int genreId);
}
