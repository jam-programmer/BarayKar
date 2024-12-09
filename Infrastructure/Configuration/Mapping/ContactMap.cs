using Domain.Entities.System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Mapping
{
    internal class ContactMap : IEntityTypeConfiguration<ContactEntity>
    {
        public void Configure(EntityTypeBuilder<ContactEntity> builder)
        {
            builder.Property(p => p.FullName).HasMaxLength(700);

        }
    }
}
