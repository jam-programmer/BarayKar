using Domain.Entities.Business;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration.Mapping
{
    internal class BusinessPropertyMap : IEntityTypeConfiguration<BusinessPropertyEntity>
    {
        public void Configure(EntityTypeBuilder<BusinessPropertyEntity> builder)
        {
            builder.ToTable("BusinessProperty", "Bis");
            builder.Property(p=>p.Key).IsRequired().HasMaxLength(900);
            builder.Property(p => p.Value).IsRequired().HasMaxLength(900);

            builder.HasOne(o => o.Business)
              .WithMany(m => m.Properties)
              .HasForeignKey(f => f.BusinessId);
        }
    }
}
