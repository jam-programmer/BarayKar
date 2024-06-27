

using Application.Common.Interfaces;

namespace Application.Common.Model
{
    public partial class Pagination: IPagination
    {
        public int curentPage { set; get; } = 1;
        public int pageSize { set; get; } = 10;
        public string keyword { set; get; } = string.Empty;

    }
}
