

using Domain.Entities.System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Mapping
{
    public class CategoryMap : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.HasOne(c => c.ParentCategory)
                 .WithMany(p => p.ChildCategories)
                         .HasForeignKey(c => c.ParentCategoryId);
            builder.Property(p => p.Name).HasMaxLength(200);
        }
    }
}
