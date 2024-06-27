
using Domain.Entities.Employment;
using Domain.Entities.System;
using Domain.Entities.System.Identity;
using Domain.Enum;

namespace Domain.Entities.Business
{
    public class BusinessEntity:BaseEntity
    {
        public long? Code { set; get; }
        public string? Link { set; get; }
        public string? AccountName { set; get; }
        public string? About { set; get; }
        public string? Address { set; get; }
        public string? Instagram { set; get; }
        public string? WhatsApp { set; get; }
        public string? Linkdin { set; get; }
        public string? FaceBook { set; get; }
        public string? PhoneNumber { set; get; }
        public string? Email { set; get; }
        public string? Latitude { set; get; }
        public string? Longitude { set; get; }
        public bool IsActive { set; get; }
        public bool IsTime { set; get; }
        public StatusEnum status { set; get; }
        public int Visist { set; get; }
        public DateTime CreateTime { set;get; }=DateTime.Now;
        #region Relation
        public Guid? UserId { set; get; }
        public UserEntity? User { set; get; }

        public Guid? ProvinceId { set; get; }
        public ProvinceEntity? Province { set; get; }

        public Guid? CityId { set; get; }
        public CityEntity? City { set; get; }   

        public Guid? CategoryId { set; get; }
        public CategoryEntity? Category { set; get; }   

        public ICollection<BusinessPictureEntity>? Pictures { set; get; }
        public ICollection<BusinessPropertyEntity>? Properties { set; get; }  
        public ICollection<BusinessTimeEntity>? Times { set; get; }

        public ICollection<EmploymentEntity>? Employments { set; get; }
        #endregion
    }
}
    