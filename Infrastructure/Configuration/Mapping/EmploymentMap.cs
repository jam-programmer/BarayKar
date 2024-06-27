using Domain.Entities.Employment;
using Domain.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Mapping
{
    public class EmploymentMap : IEntityTypeConfiguration<EmploymentEntity>
    {
        public void Configure(EntityTypeBuilder<EmploymentEntity> builder)
        {
            builder.Property(p => p.IsActive).HasDefaultValue(false);
            builder.Property(p => p.status)
                .HasDefaultValue(StatusEnum.Waiting);
            builder.Property(p => p.Title).IsRequired()
                .HasMaxLength(800);
            builder.Property(p => p.Description).IsRequired()
               .HasMaxLength(5000);

            builder.Property(p => p.Age)
             .HasMaxLength(100);
            builder.Property(p => p.StartTime)
           .HasMaxLength(150);
            builder.Property(p => p.EndTime)
           .HasMaxLength(150);
            builder.Property(p => p.TypeOfCooperation)
           .HasMaxLength(300);
            builder.Property(p => p.WorkExperience)
           .HasMaxLength(300);
            builder.Property(p => p.Salary)
           .HasMaxLength(600);

           

            builder.Property(p => p.Code).
              HasDefaultValueSql("NEXT VALUE FOR CodeGenerator");

            builder.ToTable("Employment", "Emp");


            builder.HasOne(o => o.Province)
                 .WithMany(m => m.Employments)
                 .HasForeignKey(f => f.ProvinceId);

            builder.HasOne(o => o.Business)
                 .WithMany(m => m.Employments)
                 .HasForeignKey(f => f.BusinessId);

            builder.HasOne(o => o.City)
                 .WithMany(m => m.Employments)
                 .HasForeignKey(f => f.CityId);

            builder.HasOne(o => o.User)
                .WithMany(m => m.Employments)
                .HasForeignKey(f => f.UserId);
        }
    }
}
