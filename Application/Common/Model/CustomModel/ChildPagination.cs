using Application.Common.Interfaces;

namespace Application.Common.Model.CustomModel
{
    public class ChildPagination : IPagination
    {
        public Guid ParentId { set; get; }
        public int curentPage { set; get; } = 1;
        public int pageSize { set; get; } = 10;
        public string keyword { set; get; } = string.Empty;
    }
}
