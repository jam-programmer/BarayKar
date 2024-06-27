using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.ViewModel.Business
{
    public class CityFilterViewModel
    {
        public Guid Id { get; set; }    
        public string? Name { set; get; }
        public Guid? ProvinceId { set; get; }
    }
}
