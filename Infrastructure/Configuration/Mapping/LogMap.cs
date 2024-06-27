using Domain.Entities.System;

namespace Infrastructure.Configuration.Mapping
{
    public class LogMap : IEntityTypeConfiguration<LogEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.
            EntityTypeBuilder<LogEntity> builder)
        {
            builder.Property(p => p.Level).HasMaxLength(15);
          
            builder.Property(p => p.Logger).HasMaxLength(255);
            builder.Property(p => p.RequestType).HasMaxLength(100);
            builder.Property(p => p.RequestUrl).HasMaxLength(300);
        }
    }
}
