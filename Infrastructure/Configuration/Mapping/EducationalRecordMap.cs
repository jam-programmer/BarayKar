using Domain.Entities.Resume;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Mapping
{
    public class EducationalRecordMap : IEntityTypeConfiguration<EducationalRecordEntity>
    {
        public void Configure(EntityTypeBuilder<EducationalRecordEntity> builder)
        {
            builder.ToTable("EducationalRecord", "Cv");
            builder.HasOne(o => o.Resume)
                .WithMany(m => m.Educationals)
                .HasForeignKey(f => f.ResumeId);
        }
    }
}
