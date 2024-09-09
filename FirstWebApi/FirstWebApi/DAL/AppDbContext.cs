using FirstWebApi.Configurations;
using FirstWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApi.DAL
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Genre> Genres { get; set; }
		public DbSet<Book> Books { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookConfiguration).Assembly);
			base.OnModelCreating(modelBuilder);
		}
	}
}
