using FirstWebApi.DAL;
using FirstWebApi.DTOs.GenreDTOs;
using FirstWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GenreController : ControllerBase
	{
		private readonly AppDbContext _context;

		public GenreController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAll()
		{
			var datas = await _context.Genres.ToListAsync();

			List<GenreGetDTO> genredto = datas.Select(g => new GenreGetDTO(g.Id, g.Name)).ToList();

			return Ok(genredto);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get([FromRoute]int id)
		{
			if (id <= 0) return BadRequest();

			var data = await _context.Genres.FindAsync(id);

			if(data == null) return NotFound();

			GenreGetDTO genredto = new GenreGetDTO(data.Id, data.Name);

			return Ok(genredto);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody]GenreCreateDTO dto)
		{
			if (dto is null) return BadRequest();

			Genre genre = new Genre()
			{
				Name = dto.name,
				IsDeleted = false,
				Createdtime = DateTime.Now,
				Updatedtime = DateTime.Now
			};

			await _context.Genres.AddAsync(genre);
			await _context.SaveChangesAsync();

			return Created(new Uri($"/api/genre/{genre.Id}", UriKind.Relative), genre);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] GenreUpdateDTO dto)
		{
			if (id <= 0) return BadRequest();

			if (dto is null) return BadRequest();

			var data = await _context.Genres.FindAsync(id);

			if (data is null) return NotFound();

			data.Name = dto.name;
			data.Updatedtime = DateTime.Now;

			await _context.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			if (id <= 0) return BadRequest();

			var data = await _context.Genres.FindAsync(id);

			if (data is null) return NotFound();

			_context.Genres.Remove(data);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
