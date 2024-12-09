using Domain.Entities.Business;
using Domain.Entities.Employment;
using Domain.Entities.Resume;
using Domain.Entities.System;
using Domain.Entities.System.Identity;
using System.Reflection;

namespace Infrastructure.Configuration.Contexts
{
    public class SqlServerContext : IdentityDbContext
   <UserEntity,
       RoleEntity,
       Guid,
       UserClaimEntity,
       IdentityUserRole<Guid>,
       IdentityUserLogin<Guid>,
       RoleClaimEntity, IdentityUserToken<Guid>>
    {
        public SqlServerContext(DbContextOptions options) : base(options)
        {

        }
        #region Entities
        public virtual DbSet<LogEntity> Log { get; set; }   
        public virtual DbSet<CategoryEntity> Category { set; get; }
        public virtual DbSet<ProvinceEntity> Province { set; get; }
        public virtual DbSet<CityEntity> City { set; get; }
        public virtual DbSet<SettingEntity> Setting { set; get; }
        public virtual DbSet<BusinessEntity> Business { set; get; }
        public virtual DbSet<BusinessPictureEntity> BusinessPicture { set; get; }
        public virtual DbSet<BusinessPropertyEntity> BusinessProperty { set; get; }
        public virtual DbSet<BusinessTimeEntity> BusinessTime { set; get; }
        public virtual DbSet<EmploymentEntity> Employment { set; get; }
        public virtual DbSet<WorkExperienceEntity> Experience { set; get; }
        public virtual DbSet<EducationalRecordEntity> EducationalRecord { set; get; }
        public virtual DbSet<ResumeEntity> Resume { set; get; }
        public virtual DbSet<EmploymentRequestEntity> EmploymentRequest { set; get; }
        public virtual DbSet<ContactEntity> Contact { set; get; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasSequence<long>("CodeGenerator")
               .StartsAt(100).IncrementsBy(2).HasMax(long.MaxValue);

            foreach(var relation in builder.Model.GetEntityTypes().
                SelectMany(s=>s.GetForeignKeys()))
            {
                relation.DeleteBehavior = DeleteBehavior.NoAction;
            }


            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
