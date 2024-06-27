using Domain.Entities.Resume;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Mapping
{
    public class WorkExperienceMap : IEntityTypeConfiguration<WorkExperienceEntity>
    {
        public void Configure(EntityTypeBuilder<WorkExperienceEntity> builder)
        {
            builder.ToTable("WorkExperience", "Cv");
            builder.HasOne(o => o.Resume)
                .WithMany(m => m.Experiences)
                .HasForeignKey(f => f.ResumeId);
        }
    }
}
