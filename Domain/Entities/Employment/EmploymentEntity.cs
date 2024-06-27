
using Domain.Entities.System.Identity;
using Domain.Entities.System;
using Domain.Entities.Business;
using Domain.Enum;

namespace Domain.Entities.Employment
{
    public class EmploymentEntity : BaseEntity
    {
        public long Code { set; get; }
        public StatusEnum status { set; get; }
        public bool IsActive { set; get; } = false;
        public string? Title { set; get; }
        public string? Description { set; get; }
        public string? Age { set; get; }
        public GenderEnum? Gender { set; get; }
        public string? StartTime { set; get; }
        public string? EndTime { set; get; }
        public string? TypeOfCooperation { set; get; }
        public string? WorkExperience { set; get; }
        public string? Salary { set; get; }
        public string? BusinessDays{set;get;}
        public DateTime CreateTime { set; get; } = DateTime.Now;
        #region Relation
        public Guid? UserId { set; get; }
        public UserEntity? User { set; get; }

        public Guid? ProvinceId { set; get; }
        public ProvinceEntity? Province { set; get; }

        public Guid? CityId { set; get; }
        public CityEntity? City { set; get; }

        public Guid? BusinessId { set; get; }
        public BusinessEntity? Business { set; get; }
        #endregion
    }
}
