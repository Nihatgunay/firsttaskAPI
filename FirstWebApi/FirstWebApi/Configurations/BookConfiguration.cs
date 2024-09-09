using FirstWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstWebApi.Configurations
{
	public class BookConfiguration : IEntityTypeConfiguration<Book>
	{
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			builder.Property(x => x.Name).IsRequired().HasMaxLength(60);

			builder.Property(x => x.SalePrice).IsRequired();

			builder.Property(x => x.CostPrice).IsRequired();

			builder.HasOne(x => x.Genre).WithMany(x => x.Books).HasForeignKey(x => x.GenreId);
		}
	}
}
