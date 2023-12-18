using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionArch.Domain.Entities;

namespace OnionArch.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{

		builder.HasData(
			new Category { Id = 1, Name = "Elektrik", Priorty = 1, ParentId = 0, IsDeleted = false, CreatedDate = DateTime.Now },
			new Category { Id = 2, Name = "Moda", Priorty = 2, ParentId = 0, IsDeleted = false, CreatedDate = DateTime.Now },
			new Category { Id = 3, Name = "Bilgisayar", Priorty = 1, ParentId = 1, IsDeleted = false, CreatedDate = DateTime.Now },
			new Category { Id = 4, Name = "Kadın", Priorty = 1, ParentId = 2, IsDeleted = false, CreatedDate = DateTime.Now });
	}
}
