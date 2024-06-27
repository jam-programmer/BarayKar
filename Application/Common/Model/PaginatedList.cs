using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Model
{
    public class PaginatedList<TModel>
    {
        public List<TModel>? List { set; get; }
        public int CurentPage { set; get; }
        public int TotalPage { set; get; }

        public int PageSize { set; get; }
        public string Keyword { set; get; } = string.Empty;
    }
}
