using Domain.Entities.Business;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Configuration.Mapping
{
    internal class BusinessTimeMap : IEntityTypeConfiguration<BusinessTimeEntity>
    {
        public void Configure(EntityTypeBuilder<BusinessTimeEntity> builder)
        {
            builder.ToTable("BusinessTime", "Bis");
            builder.Property(p => p.Day).IsRequired();
            builder.Property(p => p.Time).IsRequired();
            builder.HasOne(o => o.Business)
              .WithMany(m => m.Times)
              .HasForeignKey(f => f.BusinessId);
        }
    }
}
