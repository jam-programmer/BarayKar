using Application.Common.Enum;

namespace Application.Common.Record.UserPanel
{
    public class UpdateEmploymentRequestRecord
    {
        public string? Comment { set; get; }
        public Guid Id { set; get; }

        public string StatusId { set; get; }
        
    }
}
