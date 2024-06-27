namespace Application.Common.Record.UserPanel
{
    public class ResumeHistory
    {
        public Guid Id { set; get; }
        public string? Title { set; get; }
        public string? Description { set; get; }
        public string? FromDate { set; get; }
        public string? ToDate { set; get; }
        public bool IsActive { set; get; }
    }
}
