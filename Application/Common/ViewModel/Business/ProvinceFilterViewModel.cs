using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.ViewModel.Business
{
    public class ProvinceFilterViewModel
    {
        public Guid Id { get; set; }
        public string? Name { set; get; }
        public List<CityFilterViewModel> cities { set; get; }   
    }
}
