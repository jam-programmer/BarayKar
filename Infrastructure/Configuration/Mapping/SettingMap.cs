using Domain.Entities.System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Mapping
{
    internal class SettingMap : IEntityTypeConfiguration<SettingEntity>
    {
        public void Configure(EntityTypeBuilder<SettingEntity> builder)
        {
            builder.HasData(new SettingEntity());
        }
    }
}
