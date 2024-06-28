using Domain.Entities.Employment;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Mapping
{
    public class EmploymentRequestMap : IEntityTypeConfiguration<EmploymentRequestEntity>
    {
        public void Configure(EntityTypeBuilder<EmploymentRequestEntity> builder)
        {
            builder.ToTable("EmploymentRequest", "Emp");
            builder.HasOne(o => o.Resume)
                 .WithMany(w => w.Requests)
                 .HasForeignKey(f => f.ResumeId);

            builder.HasOne(o => o.Employment)
                .WithOne(w => w.Request)
                .HasForeignKey<EmploymentRequestEntity>(f => f.EmploymentId);
        }
    }
}
