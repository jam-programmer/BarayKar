using Domain.Entities.Business;
using Domain.Entities.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.System
{
    public class CityEntity:BaseEntity
    {
        public string? Name { set; get; }
        public Guid? ProvinceId { set; get; }
        public ProvinceEntity? province { set; get; }
        public ICollection<BusinessEntity>? Businesses { get; set; }

        public ICollection<EmploymentEntity>? Employments { set; get; }
    }
}
