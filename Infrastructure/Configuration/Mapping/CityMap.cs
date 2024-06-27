using Domain.Entities.System;

namespace Infrastructure.Configuration.Mapping
{
    public class CityMap : IEntityTypeConfiguration<CityEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CityEntity> builder)
        {
            builder.Property(p => p.Name).IsRequired();
         
        }
    }
}
