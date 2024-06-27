
using Domain.Entities.Business;
using Domain.Entities.Employment;

namespace Domain.Entities.System
{
    public class ProvinceEntity:BaseEntity
    {
        public string? Image { set; get; }
        public string? Name { set; get; }
        public ICollection<CityEntity>? cities { set; get; }
        public ICollection<BusinessEntity>? Businesses { get; set; }
        public ICollection<EmploymentEntity>? Employments { set; get; }
    }
}
