using Domain.Entities.System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration.Mapping
{
    public class ProvinceMap : IEntityTypeConfiguration<ProvinceEntity>
    {
        public void Configure(EntityTypeBuilder<ProvinceEntity> builder)
        {
            builder.HasIndex(i => i.Name).IsUnique();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(256);
            builder.HasMany(m => m.cities)
                .WithOne(o => o.province)
                .HasForeignKey(f => f.ProvinceId);
        }
    }
}
