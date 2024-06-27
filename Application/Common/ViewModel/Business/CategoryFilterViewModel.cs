using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.ViewModel.Business
{
    public class CategoryFilterViewModel
    {
        public Guid? ParentCategoryId { get; set; }
        public Guid? Id { set; get; }
        public string? Name { set; get; }

    }
}
