using Domain.Entities.Resume;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Mapping
{
    public class ResumeMap : IEntityTypeConfiguration<ResumeEntity>
    {
        public void Configure(EntityTypeBuilder<ResumeEntity> builder)
        {
            builder.ToTable("Resume", "Cv");
            builder.HasOne(o => o.User)
                .WithOne(o => o.Resume)
                .HasForeignKey<ResumeEntity>(f => f.UserId);
        }
    }
}
