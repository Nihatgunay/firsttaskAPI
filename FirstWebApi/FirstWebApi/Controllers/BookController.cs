using FirstWebApi.DAL;
using FirstWebApi.DTOs.BookDTOs;
using FirstWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookController : ControllerBase
	{
		private readonly AppDbContext _context;

		public BookController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAll()
		{
			var datas = await _context.Books.Include(x => x.Genre).ToListAsync();

			List<BookGetDTO> bookdtos = datas.Select(b => new BookGetDTO(b.Id, b.Name, b.SalePrice, b.Genre.Name)).ToList();

			return Ok(bookdtos);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get([FromRoute]int id)
		{
			if (id < 1) return BadRequest();

			var data = await _context.Books.Include(x => x.Genre).FirstOrDefaultAsync(x => x.Id == id);

			if(data is null) return NotFound();

			BookGetDTO dto = new BookGetDTO(data.Id, data.Name, data.SalePrice, data.Genre.Name);

			return Ok(dto);

		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody]BookCreateDTO dto)
		{
			if (dto is null) return BadRequest();

			Book book = new Book()
			{
				Name = dto.name,
				SalePrice = dto.saleprice,
				CostPrice = dto.costprice,
				GenreId = dto.genreId,
				IsDeleted = false,
				Createdtime = DateTime.Now,
				Updatedtime = DateTime.Now
			};

			await _context.Books.AddAsync(book);
			await _context.SaveChangesAsync();

			return Created(new Uri($"/api/book/{book.Id}", UriKind.Relative), book);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody]BookUpdateDTO dto)
		{
			if(id < 1) return BadRequest();

			if (dto is null) return BadRequest();

			var data = await _context.Books.Include(x=>x.Genre).FirstOrDefaultAsync(x => x.Id == id);

			if (data is null) return NotFound();

			data.Name = dto.name;
			data.SalePrice = dto.saleprice;
			data.CostPrice = dto.costprice;
			data.GenreId = dto.genreId;
			data.Updatedtime = DateTime.Now;

			await _context.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromRoute]int id)
		{
			if (id <= 0) return BadRequest();

			var data = await _context.Books.FindAsync(id);

			if (data is null) return NotFound();

			_context.Books.Remove(data);
			await _context.SaveChangesAsync();

			return Ok();
		}
	}
}
