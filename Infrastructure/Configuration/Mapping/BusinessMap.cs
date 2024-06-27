
using Domain.Entities.Business;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Mapping
{
    internal class BusinessMap : IEntityTypeConfiguration<BusinessEntity>
    {
        public void Configure(EntityTypeBuilder<BusinessEntity> builder)
        {
            builder.Property(p => p.AccountName).IsRequired().HasMaxLength(500);
            builder.HasIndex(p => p.AccountName);
            builder.Property(p => p.IsActive).HasDefaultValue(false);
            builder.Property(p => p.IsTime).HasDefaultValue(false);

            builder.Property(p => p.Code).
                HasDefaultValueSql("NEXT VALUE FOR CodeGenerator");


            builder.HasOne(o => o.User)
                .WithMany(m => m.Businesses)
                  .HasForeignKey(f => f.UserId);

            builder.HasOne(o => o.Category)
                 .WithMany(m => m.Businesses)
                    .HasForeignKey(f => f.CategoryId);

            builder.HasOne(o => o.City)
                  .WithMany(m => m.Businesses)
                      .HasForeignKey(f => f.CityId);

            builder.HasOne(o => o.Province)
             .WithMany(m => m.Businesses)
                    .HasForeignKey(f => f.ProvinceId);

            builder.ToTable("Business", "Bis");
        }
    }
}
