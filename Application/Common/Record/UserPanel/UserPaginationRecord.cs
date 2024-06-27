using Application.Common.Model;

namespace Application.Common.Record.UserPanel
{
    public record UserPaginationRecord
    {
        public Pagination pagination { set; get; }
        public Guid UserId { get; set; }
    }
}
