using Application.Common.Enum;

namespace Application.Common.Record.UserPanel
{
    public class TimeBusinessRecord
    {
        public string? Time { set; get; }
        public Day Day { set; get; }
        public Guid Id { get; set; }
    }
}
