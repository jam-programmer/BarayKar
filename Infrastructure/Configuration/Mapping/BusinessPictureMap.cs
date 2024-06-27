using Domain.Entities.Business;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Mapping
{
    internal class BusinessPictureMap : IEntityTypeConfiguration<BusinessPictureEntity>
    {
        public void Configure(EntityTypeBuilder<BusinessPictureEntity> builder)
        {
            builder.Property(p => p.Image).IsRequired();
            builder.HasOne(o => o.Business)
                .WithMany(m => m.Pictures)
                .HasForeignKey(f => f.BusinessId);
            builder.ToTable("BusinessPicture", "Bis");
        }
    }
}
